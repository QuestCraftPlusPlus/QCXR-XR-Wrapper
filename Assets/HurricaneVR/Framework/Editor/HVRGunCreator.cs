using System.Collections.Generic;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Weapons;
using HurricaneVR.Framework.Weapons.Guns;
using HurricaneVR.Framework.Weapons.Guns.PartFinders;
using UnityEditor;
using UnityEngine;
using HVRPistol = HurricaneVR.Framework.Weapons.Guns.HVRPistol;
using Object = UnityEngine.Object;

namespace HurricaneVR.Editor
{
    public class HVRGunCreator : EditorWindow
    {
        private const string RecoilSMG = "RecoilSettings/HVR_Recoil_SMG";
        private const string RecoilAR = "RecoilSettings/HVR_Recoil_AR";
        private const string RecoilPistol = "RecoilSettings/HVR_Recoil_Pistol";
        private const string RecoilPump = "RecoilSettings/HVR_Recoil_PumpShotgun";

        //private const string TwoHandPistol = "HandSettings/HVR_Pistol_TwoHand";
        //private const string TwoHandLarge = "HandSettings/HVR_LargeWeapon_TwoHanded";
        private const string TwoHandLargeStabilizer = "HandSettings/HVR_Stabilizer_TwoHanded_Strength";
        private const string TwoHandPistolStabilizer = "HandSettings/HVR_Pistol_Stabilizer_TwoHanded_Strength";

        //private const string OneHandPistol = "HandSettings/HVR_Pistol_OneHand";
        //private const string OneHandDefault = "HandSettings/HVR_DefaultHandSettings";

        private static HVRGunCreator _window;

        private GameObject _gunPrefab;
        private GameObject _casingPrefab;
        private GameObject _ejectedBulletPrefab;
        private GameObject _bulletPrefab;
        private HVRGunType _gunType;
        private LayerMask _hitLayerMask = ~0;

        private HVRTriggerCollider _socketCollider = HVRTriggerCollider.Box;
        private HVRTriggerCollider _stabilizerCollider = HVRTriggerCollider.Box;
        private HVRTriggerCollider _cockingHandleCollider = HVRTriggerCollider.Capsule;
        private float _ammoTimeout = 2f;
        private float _gunMass = 1.5f;
        private float _bulletSpeed = 80f;
        private int _rpm = 900;
        private GunFireType _firingType;

        private HVRHandPose _gripPose;
        private HVRHandPose _stabilizerPose;
        private HVRHandPose _cockingHandlePose;


        //mags
        private bool _animateMagazine;
        private bool _animateMagazineEject;
        private bool _magazineHeld = true;
        private bool _magazineHoverSwap = true;
        //private SocketHoldType _magazineHoldType = SocketHoldType.RemoveRigidbody;
        private bool _addMagGrab = true;
        private float _magGrabDelay = .1f;
        private bool _stabilizerGrabsGrip;
        private bool _stabilizerRequiresGripGrabbed = true;
        private bool _stabilizerDropsOnGripReleased = true;
        private float _cycleTime = .05f;
        private PDStrength _gripOneHand;
        private PDStrength _gripTwoHand;
        private PDStrength _stabilizerTwoHand;
        private bool _ejectedBulletPooled = true;
        private HVRRecoilSettings _recoilSettings;
        private Vector2 _scrollPosition;

        public AudioClip DryFireSFX;
        public AudioClip FiredSFX;
        public AudioClip CockedBackSFX;
        public AudioClip CockedForwardSFX;



        [MenuItem("Tools/HurricaneVR/Gun Creator")]
        public static void ShowWindow()
        {
            _window = GetWindow<HVRGunCreator>(false);
            _window.titleContent = new GUIContent("Gun Creator");
        }

        //#if CLOUDWALKER


        //[Shortcut("Refresh Window", KeyCode.F9)]
        public static void Refresh()
        {
            if (_window)
            {
                _window.Close();
            }

            ShowWindow();
        }

        //#endif
        private void OnGUI()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            HVREditorExtensions.Header("Prefabs");
            _gunPrefab = EditorGUILayout.ObjectField("Gun Prefab", _gunPrefab, typeof(GameObject), false) as GameObject;
            _casingPrefab = EditorGUILayout.ObjectField("Casing Prefab", _casingPrefab, typeof(GameObject), false) as GameObject;
            _ejectedBulletPrefab = EditorGUILayout.ObjectField("Ejected Bullet Prefab", _ejectedBulletPrefab, typeof(GameObject), false) as GameObject;
            HVREditorExtensions.Toggle("Pooled Ejected Bullet", ref _ejectedBulletPooled);
            _bulletPrefab = EditorGUILayout.ObjectField("Bullet Prefab", _bulletPrefab, typeof(GameObject), false) as GameObject;

            GUILayout.Space(20f);

            HVREditorExtensions.Header("Animation");

            HVREditorExtensions.FloatField("Cycling Time", ref _cycleTime);


            GUILayout.Space(20f);

            HVREditorExtensions.Header("Gun Settings");

            HVREditorExtensions.IntField("Rounds Per Minute", ref _rpm);
            HVREditorExtensions.FloatField("Bullet Speed", ref _bulletSpeed);
            _firingType = (GunFireType)EditorGUILayout.EnumPopup("Firing Mode", _firingType);
            var currentType = _gunType;
            _gunType = (HVRGunType)EditorGUILayout.EnumPopup("Type", _gunType);
            _hitLayerMask = LayerMaskDrawer.LayerMaskField("Hit Layer Mask", _hitLayerMask);
            _gunMass = EditorGUILayout.FloatField("Gun Mass", _gunMass);
            _cockingHandleCollider = (HVRTriggerCollider)EditorGUILayout.EnumPopup("Cocking Handle Trigger", _cockingHandleCollider);
            HVREditorExtensions.ObjectField("Recoil Settings", ref _recoilSettings, false);

            GUILayout.Space(20f);
            if (_gunType == HVRGunType.PumpShotgun)
            {
                HVREditorExtensions.Header("Pump Grabbable");
                _stabilizerCollider = (HVRTriggerCollider)EditorGUILayout.EnumPopup("Pump Trigger", _stabilizerCollider);
                HVREditorExtensions.Toggle("Grabs Grip On Release", ref _stabilizerGrabsGrip);
            }
            else
            {
                HVREditorExtensions.Header("Stabilizer Grabbable");
                _stabilizerCollider = (HVRTriggerCollider)EditorGUILayout.EnumPopup("Stabilizer Trigger", _stabilizerCollider);
                HVREditorExtensions.Toggle("Grabs Grip On Release", ref _stabilizerGrabsGrip);
                HVREditorExtensions.Toggle("Requires Grip Grabbed", ref _stabilizerRequiresGripGrabbed);
                HVREditorExtensions.Toggle("Drops On Grip Released", ref _stabilizerDropsOnGripReleased);
            }



            GUILayout.Space(10f);
            HVREditorExtensions.Header("Ammo Socket Settings");
            if (_gunType != HVRGunType.PumpShotgun)
            {
                HVREditorExtensions.Toggle("Releases On Hover", ref _magazineHoverSwap);
                HVREditorExtensions.FloatField("Socket Grab Timeout", ref _ammoTimeout);
                HVREditorExtensions.Toggle("Magazine Must Be Held", ref _magazineHeld);
                HVREditorExtensions.Toggle("Animate Magazine", ref _animateMagazine);
                HVREditorExtensions.Toggle("Animate Magazine Eject", ref _animateMagazineEject);
                //HVREditorExtensions.EnumField("Hold Type", ref _magazineHoldType);
                HVREditorExtensions.Toggle("Add Magazine Grab", ref _addMagGrab);
                HVREditorExtensions.FloatField("Mag Grab Delay", ref _magGrabDelay);
            }

            _socketCollider = (HVRTriggerCollider)EditorGUILayout.EnumPopup("Ammo Socket Collider", _socketCollider);


            GUILayout.Space(20f);

            HVREditorExtensions.Header("Grabbable Hand Strength Settings");

            //if (GUILayout.Button("Populate Defaults"))
            //{
            //    PopulateDefaultHandSettings();
            //}

            HVREditorExtensions.StrengthField("Grip One Handed", ref _gripOneHand);
            HVREditorExtensions.StrengthField("Grip Two Handed", ref _gripTwoHand);
            HVREditorExtensions.StrengthField("Stabilizer Two Handed", ref _stabilizerTwoHand);

            GUILayout.Space(20f);
            HVREditorExtensions.Header("SFX");
            HVREditorExtensions.ObjectField("Dry Fire", ref DryFireSFX, false);
            HVREditorExtensions.ObjectField("Cocked Back", ref CockedBackSFX, false);
            HVREditorExtensions.ObjectField("Cocked Forward", ref CockedForwardSFX, false);
            HVREditorExtensions.ObjectField("Fired", ref FiredSFX, false);


            GUILayout.Space(20f);

            HVREditorExtensions.Header("Poses (If You have Already)");

            _gripPose = EditorGUILayout.ObjectField("Gun Pose", _gripPose, typeof(HVRHandPose), false) as HVRHandPose;

            if (_gunType == HVRGunType.PumpShotgun)
            {
                _stabilizerPose = EditorGUILayout.ObjectField("Pump Pose", _stabilizerPose, typeof(HVRHandPose), false) as HVRHandPose;
            }
            else
            {
                _stabilizerPose = EditorGUILayout.ObjectField("Stabilizer Pose", _stabilizerPose, typeof(HVRHandPose), false) as HVRHandPose;
                _cockingHandlePose = EditorGUILayout.ObjectField("Cocking Handle Pose", _cockingHandlePose, typeof(HVRHandPose), false) as HVRHandPose;
            }

            GUI.enabled = _gunPrefab;

            if (GUILayout.Button("Create"))
            {
                GameObject root = null;

                switch (_gunType)
                {
                    case HVRGunType.Pistol:
                        root = CreatePistol();
                        break;
                    case HVRGunType.AssaultRifle:
                    case HVRGunType.SMG:
                        root = CreateAutomatic();
                        break;

                    case HVRGunType.PumpShotgun:
                        root = CreatePumpShotgun();
                        break;
                }

                root.SetExpandedRecursive(true);
            }

            GUI.enabled = true;

            if (currentType != _gunType)
            {
                OnGunTypeChanged();
            }


            //if (!_gripPose)
            //{
            //    _gripPose = LoadAsset<HVRHandPose>("pistol_grip");
            //}

            //if (!_stabilizerPose)
            //{
            //    _stabilizerPose = LoadAsset<HVRHandPose>("pistol_offhand");
            //}

            //if (!_cockingHandlePose)
            //{
            //    _cockingHandlePose = LoadAsset<HVRHandPose>("pistol_slide");
            //}

            //SetDefault(ref _bulletPrefab, "HVR_bullet_trail");
            //SetDefault(ref _casingPrefab, "HVR_bullet_casing");
            //SetDefault(ref _ejectedBulletPrefab, "HVR_smg_bullet");

            //SetDefault(ref _gunPrefab, "HVR_smg");

            PopulateDefaultStabilizerSettings(true);

            if (_recoilSettings == null)
            {
                SetDefaultRecoil();
            }

            GUILayout.EndScrollView();
        }

        private void OnGunTypeChanged()
        {
            SetDefaultRecoil();
            //PopulateDefaultHandSettings();
            PopulateDefaultStabilizerSettings(false);
        }

        private void SetDefaultRecoil()
        {
            string recoil;

            switch (_gunType)
            {
                case HVRGunType.Pistol:
                    recoil = RecoilPistol;
                    break;
                case HVRGunType.AssaultRifle:
                    recoil = RecoilAR;
                    break;
                case HVRGunType.SMG:
                    recoil = RecoilSMG;
                    break;
                case HVRGunType.PumpShotgun:
                    recoil = RecoilPump;
                    break;
                default:
                    return;
            }

            TryLoadAsset(ref _recoilSettings, recoil);
        }

        private void PopulateDefaultStabilizerSettings(bool onlyIfNull)
        {
            var foregrip = TwoHandLargeStabilizer;

            switch (_gunType)
            {
                case HVRGunType.Pistol:
                    foregrip = TwoHandPistolStabilizer;
                    break;
                case HVRGunType.SMG:
                    foregrip = TwoHandLargeStabilizer;
                    break;
                case HVRGunType.PumpShotgun:
                case HVRGunType.AssaultRifle:
                    foregrip = TwoHandLargeStabilizer;
                    break;
            }

            if (onlyIfNull)
            {
                SetDefault(ref _stabilizerTwoHand, foregrip);
            }
            else
            {
                TryLoadAsset(ref _stabilizerTwoHand, foregrip);
            }
        }

        //private void PopulateDefaultHandSettings()
        //{
        //    var grip = OneHandDefault;
        //    var gripTwoHand = TwoHandLarge;

        //    switch (_gunType)
        //    {
        //        case HVRGunType.Pistol:
        //            grip = OneHandPistol;
        //            gripTwoHand = TwoHandPistol;
        //            break;
        //        case HVRGunType.SMG:
        //            gripTwoHand = TwoHandLarge;
        //            break;
        //        case HVRGunType.PumpShotgun:
        //        case HVRGunType.AssaultRifle:
        //            gripTwoHand = TwoHandLarge;
        //            break;
        //    }

        //    TryLoadAsset(ref _gripOneHand, grip);
        //    TryLoadAsset(ref _gripTwoHand, gripTwoHand);
        //}

        private void SetDefault<T>(ref T value, string assetName) where T : Object
        {
            if (!value && !string.IsNullOrWhiteSpace(assetName))
            {
                value = Resources.Load<T>(assetName);
            }
        }

        private void TryLoadAsset<T>(ref T value, string assetName) where T : Object
        {
            if (!string.IsNullOrWhiteSpace(assetName))
            {
                value = Resources.Load<T>(assetName);
            }
        }

        private GameObject CreateGun<T>(out HVRGrabbable gunGrabbable, out GameObject model, out HVRGunBase gun) where T : HVRGunBase
        {
            var root = new GameObject(_gunPrefab.name);

            Undo.RegisterCreatedObjectUndo(root, _gunPrefab.name);

            gunGrabbable = AddGrabbable(root, out var poser, out var gp);
            gunGrabbable.OneHandStrength = _gripOneHand;
            gunGrabbable.TwoHandStrength = _gripTwoHand;
            poser.PrimaryPose.Pose = _gripPose;
            gunGrabbable.HoldType = HVRHoldType.Swap;
            gunGrabbable.RequireOverlapClearance = true;
            gunGrabbable.OverlapTimeout = 2f;
            gunGrabbable.LinkedGrabbables = new List<HVRGrabbable>();

            //after grabpoints
            model = Instantiate(_gunPrefab, Vector3.zero, Quaternion.identity, root.transform) as GameObject;
            model.name = model.name.Replace("(Clone)", "");

            gun = root.AddComponent<T>();

            SetupCommonObjects(gun, model, gunGrabbable);

            return root;
        }

        private GameObject CreatePistol()
        {
            var root = CreateGun<HVRPistol>(out var gunGrabbable, out var model, out var gun);
            return root;
        }

        private GameObject CreateAutomatic()
        {
            var root = CreateGun<HVRAutomaticGun>(out var gunGrabbable, out var model, out var gun);
            return root;
        }

        private GameObject CreatePumpShotgun()
        {
            var root = CreateGun<HVRShotgun>(out var gunGrabbable, out var model, out var gun);
            var mag = root.AddComponent<HVRShotgunMagazine>();
            gun.ChambersAfterFiring = false;
            mag.MaxCount = 5;
            mag.StartingCount = 3;
            mag.DestroyIfEmpty = false;
            gun.AnimateGun = false;
            gun.EjectCasingAfterFiring = false;
            gun.BoltPushedBackAfterEmpty = false;
            var socket = root.GetComponentInChildren<HVRShotgunAmmoSocket>();
            if (socket)
            {
                socket.ShotgunMagazine = mag;
            }
            return root;
        }

        private static T LoadAsset<T>(string name) where T : Object
        {
            var fuzzyMatchesUnityBadSearch = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            foreach (var fuzzyMatch in fuzzyMatchesUnityBadSearch)
            {
                var path = AssetDatabase.GUIDToAssetPath(fuzzyMatch);
                if (path.ToLower().Contains(name.ToLower()))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                    return asset;
                }
            }

            return null;
        }

        private HVRGrabbable SetupStabilizer(HVRGunBase hvrGunBase, Transform parent, HVRGrabbable gunGrabbable)
        {
            var offhand = new GameObject("StabilizerGrabbable");
            offhand.transform.parent = parent;
            offhand.transform.ResetLocalProps();
            var grabbable = AddGrabbable(offhand, out var poser, out var gp);
            poser.PrimaryPose.Pose = _stabilizerPose;

            //grabbable.TrackingType = HVRGrabTracking.None;
            grabbable.HoldType = HVRHoldType.OneHand;
            grabbable.DisableHandCollision = true;
            grabbable.ForceGrabbable = false;
            grabbable.TwoHandStrength = _stabilizerTwoHand;
            grabbable.ExtraIgnoreCollisionParents = new List<Transform>() { gunGrabbable.Rigidbody.transform };

            if (_stabilizerRequiresGripGrabbed)
            {
                var require = offhand.AddComponent<HVRRequireOtherGrabbable>();
                require.Grabbable = gunGrabbable;
                require.DropIfReleased = _stabilizerDropsOnGripReleased;
                require.GrabRequiredIfReleased = _stabilizerGrabsGrip;
            }

            hvrGunBase.StabilizerGrabbable = grabbable;

            var gunRB = hvrGunBase.GetRigidbody();
            grabbable.Rigidbody = gunRB;
            grabbable.MasterGrabbable = gunGrabbable;
            
            gunGrabbable.LinkedGrabbables.Add(grabbable);

            AddTriggerCollider(_stabilizerCollider, offhand);

            var handStrength = offhand.AddComponent<HVRTwoHandStrength>();
            handStrength.Others = new HVRGrabbable[1];
            handStrength.Others[0] = gunGrabbable;

            return grabbable;
        }

        private void SetupCommonObjects(HVRGunBase gun, GameObject model, HVRGrabbable gunGrabbable)
        {
            var rb = gun.gameObject.AddComponent<Rigidbody>();
            rb.mass = _gunMass;
            gunGrabbable.Rigidbody = rb;

            FindObject<HVRChamberedCasingFinder>(model, ref gun.ChamberedCasing);
            FindObject<HVRChamberedRoundFinder>(model, ref gun.ChamberedRound);

            var bolt = FindBolt(model);

            gun.TriggerAnimator = FindTriggerAnimator(model);

            gun.RecoilComponent = gun.gameObject.AddComponent<HVRRecoil>();
            gun.RecoilComponent.Settings = _recoilSettings;
            gun.GunSounds = gun.gameObject.AddComponent<HVRGunSounds>();
            gun.GunSounds.Fired = FiredSFX;
            gun.GunSounds.OutOfAmmo = DryFireSFX;
            gun.GunSounds.SlideBack = CockedBackSFX;
            gun.GunSounds.SlideForward = CockedForwardSFX;
            gun.Bolt = bolt;

            var adjustTransforms = AddAdjustTransform(gun.gameObject);

            if (_gunType != HVRGunType.PumpShotgun)
            {
                SetupStabilizer(gun, adjustTransforms.transform, gunGrabbable);
            }

            var emitters = new GameObject("Emitters (Right Vector)");
            emitters.transform.parent = adjustTransforms.transform;
            emitters.transform.ResetLocalProps();

            SetupCockingHandle(gunGrabbable, model, gun, bolt, adjustTransforms);
            AddRecoilTransforms(adjustTransforms, gun.RecoilComponent);
            AddCasingEmitter(emitters, _casingPrefab, gun);
            AddBulletEmitter(emitters, _ejectedBulletPrefab, gun);
            AddBulletOrigin(adjustTransforms, gun);
            AddAmmoSocket(gunGrabbable, adjustTransforms, gun, model);

            gun.BulletPrefab = _bulletPrefab;
            gun.FireType = _firingType;
            gun.HitLayerMask = _hitLayerMask;
            gun.BulletSpeed = _bulletSpeed;
            gun.CyclingTime = _cycleTime;

            gun.Cooldown = 1f / (_rpm / 60f);


            Selection.activeGameObject = adjustTransforms;
        }

        private void AddRecoilTransforms(GameObject adjustTransforms, HVRRecoil gunRecoilComponent)
        {
            var up = new GameObject("UpRecoil (Up Vector)");
            up.transform.parent = adjustTransforms.transform;
            var back = new GameObject("BackwardRecoil (Forward Vector)");
            back.transform.parent = adjustTransforms.transform;
            up.transform.ResetLocalProps();
            back.transform.ResetLocalProps();
            back.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
            gunRecoilComponent.UpRecoil = up.transform;
            gunRecoilComponent.BackRecoil = back.transform;
        }

        private HVRGunBolt FindBolt(GameObject model)
        {
            var component = model.GetComponentInChildren<HVRGunBolt>();

            if (!component)
            {
                Debug.Log($"HVRGunBolt not located.");
                return null;
            }

            return component;
        }

        private void FindObject<T>(GameObject model, ref GameObject field) where T : MonoBehaviour
        {
            var component = model.GetComponentInChildren<T>(true);
            if (!component)
            {
                Debug.Log($"{typeof(T).Name} not found.");
                return;
            }

            field = component.gameObject;
        }

        private HVRTriggerAnimator FindTriggerAnimator(GameObject model)
        {
            var triggerAnimator = model.GetComponentInChildren<HVRTriggerAnimator>();
            if (!triggerAnimator)
            {
                Debug.Log($"HVRTriggerAnimator not located.");
                return null;
            }

            return triggerAnimator;
        }

        private void SetupCockingHandle(HVRGrabbable gripGrabbable, GameObject model, HVRGunBase gun, HVRGunBolt hvrGunBolt, GameObject adjustTransforms)
        {
            var cockingHandle = model.GetComponentInChildren<HVRCockingHandle>();

            if (!cockingHandle)
            {
                Debug.Log($"HVRCockingHandle not located.");
                return;
            }

            cockingHandle.Bolt = hvrGunBolt;

            gun.CockingHandle = cockingHandle;

            var handleGO = cockingHandle.gameObject;
            AddGrabPoints(handleGO, out var poser, out var grabPoint);

            if (_gunType == HVRGunType.PumpShotgun)
            {
                var stabilizer = SetupStabilizer(gun, adjustTransforms.transform, gripGrabbable);
                stabilizer.gameObject.name = "PumpGrabbable";
                var go = stabilizer.gameObject;
                var gp = stabilizer.GetComponentInChildren<HVRPosableGrabPoint>();
                var poser2 = stabilizer.GetComponentInChildren<HVRHandPoser>();
                Destroy(grabPoint.GetComponent<HVRHandPoser>());
                Destroy(grabPoint.GetComponent<HVRPosableGrabPoint>());
                gp.VisualGrabPoint = grabPoint.transform;
                poser2.PrimaryPose.Pose = _stabilizerPose;
                cockingHandle.Grabbable = stabilizer;
            }
            else
            {
                var grabbableObject = handleGO;
                poser.PrimaryPose.Pose = _cockingHandlePose;
                var handleGrabbable = grabbableObject.AddComponent<HVRGrabbable>();
                handleGrabbable.TrackingType = HVRGrabTracking.None;
                handleGrabbable.HoldType = HVRHoldType.OneHand;
                handleGrabbable.PoseImmediately = true;
                handleGrabbable.DisableHandCollision = true;
                handleGrabbable.Stationary = true;
                handleGrabbable.ForceGrabbable = false;
                AddTriggerCollider(_cockingHandleCollider, handleGO);
                var require = grabbableObject.AddComponent<HVRRequireOtherGrabbable>();
                require.Grabbable = gripGrabbable;
                require.DropIfReleased = true;
                require.GrabRequiredIfReleased = true;
            }

        }

        internal GameObject AddAmmoSocket(HVRGrabbable gunGrabbable, GameObject parent, HVRGunBase gun, GameObject model)
        {
            var go = new GameObject("AmmoSocket");
            go.transform.parent = gun.gameObject.transform;
            go.transform.ResetLocalProps();

            var bagGo = new GameObject("AmmoSocketTrigger");
            bagGo.transform.parent = parent.transform;
            bagGo.transform.ResetLocalProps();
            var bag = bagGo.AddComponent<HVRTriggerGrabbableBag>();

            AddTriggerCollider(_socketCollider, bagGo);

            HVRSocket socket;

            if (_gunType == HVRGunType.PumpShotgun)
            {
                socket = go.AddComponent<HVRShotgunAmmoSocket>();
                socket.GrabsFromHand = true;
                socket.GrabbableMustBeHeld = true;
                socket.CanRemoveGrabbable = false;
                socket.HoldType = SocketHoldType.Kinematic;
            }
            else
            {
                var magazine = model.GetComponentInChildren<HVRMagazineFinder>();

                var magSocket = go.AddComponent<HVRMagazineSocket>();
                magSocket.Animate = _animateMagazine;
                magSocket.AnimateEject = _animateMagazineEject;

                var magGO = new GameObject("AnimationDirection (Forward Axis)");
                magGO.transform.parent = magSocket.gameObject.transform;
                magGO.transform.ResetLocalProps();
                magSocket.MagazineAxis = magGO.transform;

                socket = magSocket;
                socket.GrabsFromHand = true;
                socket.GrabbableMustBeHeld = _magazineHeld;
                socket.ScaleGrabbable = false;
                socket.CanRemoveGrabbable = false;
                socket.GrabTimeout = _ammoTimeout;
                socket.HoldType = SocketHoldType.Kinematic;

                if (_magazineHoverSwap)
                {
                    var magRelease = new GameObject("MagazineHoverEject");
                    magRelease.transform.parent = parent.transform;
                    var autoDrop = magRelease.AddComponent<HVRAutoDropMagazine>();
                    autoDrop.MagazineSocket = socket;
                    var collider = magRelease.AddComponent<BoxCollider>();
                    collider.size = new Vector3(.07f, .08f, .07f);
                    collider.isTrigger = true;
                }

                if (_addMagGrab)
                {
                    var magGrab = new GameObject("MagazineGrabbable");
                    var g = magGrab.AddComponent<HVRGrabbable>();
                    g.ForceGrabbable = false;
                    g.PhysicsPoserFallback = false;
                    var grabMag = magGrab.AddComponent<HVRGrabMagazine>();
                    grabMag.GrabDelay = _magGrabDelay;
                    grabMag.MagSocket = socket;
                    var collider = magGrab.AddComponent<BoxCollider>();
                    collider.size = new Vector3(.05f, .08f, .07f);
                    collider.isTrigger = true;
                    magGrab.transform.parent = parent.transform;
                    magGrab.transform.ResetLocalProps();

                    var require = magGrab.AddComponent<HVRRequireOtherGrabbable>();
                    require.Grabbable = gunGrabbable;
                    require.DropIfReleased = false;
                    require.GrabRequiredIfReleased = false;
                }

                if (magazine)
                {
                    go.transform.localPosition = magazine.transform.localPosition;
                    go.transform.localRotation = magazine.transform.localRotation;
                }

                gun.AmmoSocket = socket;
            }

            socket.GrabBags = new List<HVRGrabbableBag>() { bag };

            return go;
        }

        private static void AddTriggerCollider(HVRTriggerCollider colliderType, GameObject go)
        {
            Collider collider = null;

            switch (colliderType)
            {
                case HVRTriggerCollider.Box:
                    var box = go.AddComponent<BoxCollider>();
                    box.size = new Vector3(.08f, .08f, .08f);
                    collider = box;
                    break;
                case HVRTriggerCollider.Sphere:
                    var sphere = go.AddComponent<SphereCollider>();
                    sphere.radius = .06f;
                    collider = sphere;
                    break;
                case HVRTriggerCollider.Capsule:
                    var capsule = go.AddComponent<CapsuleCollider>();
                    capsule.height = .1f;
                    capsule.radius = .02f;
                    capsule.direction = 2;
                    collider = capsule;
                    break;
            }

            if (collider != null)
            {
                collider.isTrigger = true;
            }
        }

        internal static GameObject AddAdjustTransform(GameObject parent)
        {
            var go = new GameObject("Position These Objects");
            go.transform.parent = parent.transform;
            go.transform.ResetLocalProps();
            return go;
        }

        internal static void AddBulletOrigin(GameObject parent, HVRGunBase hvrGunBase)
        {
            var go = new GameObject("BulletOrigin (Forward Vector)");
            go.transform.parent = parent.transform;
            go.transform.ResetLocalProps();
            hvrGunBase.BulletOrigin = go.transform;
        }

        internal void AddBulletEmitter(GameObject parent, GameObject prefab, HVRGunBase gun)
        {
            var go = new GameObject("BulletEmitter (Right Vector)");
            HVRGunEmitterBase emitter;

            if (_ejectedBulletPooled)
            {
                emitter = go.AddComponent<HVRPooledEmitter>();
            }
            else
            {
                emitter = go.AddComponent<HVRBulletEmitter>();
            }

            emitter.Prefab = prefab;
            go.transform.parent = parent.transform;
            go.transform.ResetLocalProps();
            gun.BulletEmitter = emitter;
        }

        internal static void AddCasingEmitter(GameObject parent, GameObject prefab, HVRGunBase gun)
        {
            var go = new GameObject("CasingEmitter (Right Vector)");
            var emitter = go.AddComponent<HVRPooledEmitter>();
            emitter.Prefab = prefab;
            go.transform.parent = parent.transform;
            go.transform.ResetLocalProps();
            gun.CasingEmitter = emitter;
        }

        internal static HVRGrabbable AddGrabbable(GameObject obj, out HVRHandPoser poser, out HVRPosableGrabPoint grabPoint)
        {
            var grabbable = obj.AddComponent<HVRGrabbable>();

            AddGrabPoints(obj, out poser, out grabPoint);

            obj.SetExpandedRecursive(true);
            return grabbable;
        }

        internal static GameObject AddGrabPoint(GameObject obj, out HVRHandPoser poser, out HVRPosableGrabPoint grabPoint)
        {
            var go = new GameObject("GrabPoint");
            go.transform.parent = obj.transform;
            go.transform.ResetLocalProps();
            poser = go.AddComponent<HVRHandPoser>();
            poser.PrimaryPose = new HVRHandPoseBlend();
            grabPoint = go.AddComponent<HVRPosableGrabPoint>();
            grabPoint.HandPoser = poser;
            return go;
        }

        internal static GameObject AddGrabPoints(GameObject obj, out HVRHandPoser poser, out HVRPosableGrabPoint grabPoint)
        {
            var points = new GameObject("GrabPoints");
            points.AddComponent<HVRGrabPoints>();
            points.transform.parent = obj.transform;
            points.transform.ResetLocalProps();
            obj.SetExpandedRecursive(true);

            AddGrabPoint(points, out poser, out grabPoint);

            return points;
        }

    }

    public enum HVRTriggerCollider
    {
        Box, Sphere, Capsule, AddMyself
    }

    public enum HVRGunType
    {
        Pistol, AssaultRifle, SMG, PumpShotgun
    }
}