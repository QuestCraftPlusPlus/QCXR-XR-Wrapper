using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR;


#if USING_OPENXR
using UnityEngine.XR.OpenXR;
#endif

#if USING_XR_MANAGEMENT
using UnityEngine.XR.Management;
#endif

#if HVR_OCULUS
using HurricaneVR.Framework.Oculus;
#endif

#if HVR_STEAMVR
using HurricaneVR.Framework.SteamVR;
using Valve.VR;
#endif

namespace HurricaneVR.Framework.ControllerInput
{
    [Serializable]
    public class HVRControllerEvent : UnityEvent<HVRController> { }

    public class HVRInputManager : MonoBehaviour
    {
        public const string OpenVRController = "openvr";
        public const string WindowsMR = "windowsmr";
        public const string WindowsMROpenXR = "windows mr controller";
        public const string Vive = "vive";
        public const string Cosmos = "cosmos";
        public const string Oculus = "oculus";
        public const string Knuckles = "knuckles";
        public const string KnucklesOpenXR = "index controller";
        public const string WMRController = "spatial";
        public const string HTC = "htc";
        public const string Reverb = "reverb";
        public const string G2 = "g2";
        public const string OpenXR_G2 = "hp reverb g2 controller";

        public const string LegacyOpenVRName = "OpenVR";
        public const string LegacyOculusName = "Oculus";
        public const string LegacyNone = "None";



        [Header("XR Plugin Detection Names")]
        public string OpenVRLoader = "Open VR Loader";
        public string OculusLoader = "Oculus Loader";
        public string OpenXRLoader = "Open XR Loader";
        public string WMRLoader = "Windows MR Loader";


        private const string ReverbG2 = "0x045E/0x066A";
        private const string OdysseyPlus = "WindowsMR: 0x045E/0x065D"; //OpenVR Controller(WindowsMR: 0x045E/0x065D/0/1) - Left

        public static HVRInputManager Instance { get; private set; }



        [Header("Oculus - Requires Oculus Asset + Integration")]

        [Tooltip("If true ovrinputs will be used")]
        public bool UseOVRInputs;

        [Tooltip("If set to true, OVRManager is required to be in your scene")]
        public bool OVRHaptics;

        [Tooltip("If using OVRInput for Oculus devices without OVRManager in the scene then set this to true.")]
        public bool ForceOVRInputUpdate;

        [Header("SteamVR - Requires SteamVR + Integration")]
        public bool InitializeSteamVR = true;
        public bool InitializeSteamVRActions = true;

        [Header("Input Settings")]

        [Tooltip("If true uses the new input system bindings")]
        public bool UseNewInputSystem;

        public TrackingOriginModeFlags TrackingSpace = TrackingOriginModeFlags.Floor;


        [Tooltip("Finger Curl Settings, defaults created if not supplied")]
        public HVRFingerSettings FingerSettings;

        [Tooltip("Device / SDK controller offsets")]
        public HVRControllerOffsets ControllerOffsets;

        [Tooltip("Haptics Settings")]
        public HVRGrabHaptics GrabHaptics;

        [Header("Device Specific Settings")]
        public HVRInputSettings WMRInputMap;
        [FormerlySerializedAs("WMRWithButtonsInputMap")]
        public HVRInputSettings ReverbG2InputMap;
        public HVRInputSettings OculusInputMap;
        public HVRInputSettings ViveInputMap;
        public HVRInputSettings KnucklesInputMap;
        public HVRInputSettings CosmosInputMap;


        //public bool LeftHasTrackPad;
        //public bool RightHasTrackPad;
        [Header("Deadzones")]
        [Tooltip("WMR device deadzone, if any.")]
        public Vector2 WMRDeadzone = new Vector2(.15f, .15f);

        [Tooltip("Oculus device deadzone, if any.")]
        public Vector2 OculusDeadzone = new Vector2(.15f, .15f);

        [Tooltip("Vive device deadzone, if any.")]
        public Vector2 ViveDeadzone = new Vector2(0f, 0f);

        [Tooltip("Knuckles device deadzone, if any.")]
        public Vector2 KnucklesDeadzone = new Vector2(0f, 0f);

        [Tooltip("Cosmos device deadzone, if any.")]
        public Vector2 CosmosDeadzone = new Vector2(0f, 0f);

        [Tooltip("Master deadzone, useful if you want the user to set.")]
        public Vector2 DeadzoneOverride;

        [Tooltip("Override provider level deadzone.")]
        public bool OverrideDeadzone;


        [Header("Debugging")]
        public InputSDK CurrentSDK = InputSDK.None;

        [SerializeField]
        private string _xrPlugin;

        public string XRPluginLoader
        {
            get { return _xrPlugin; }
            set
            {
                _xrPlugin = value;

                if (IsOpenXR)
                {
#if USING_OPENXR
                    Debug.Log($"OpenXR Runtime: {OpenXRRuntime.name}");
#endif
                }
            }
        }

        public bool LegacyActive;
        public bool XRPluginActive;
        public VRMode VRPlugin;
        public bool IsVRInitialized => LegacyActive || XRPluginActive;

        public bool IsOpenXR => XRPluginLoader != null && XRPluginLoader == OpenXRLoader;

        public bool IsSteamVR { get; private set; }

        public string HMDManufacturer;
        public string HMDName;

        public string LeftManufacturer;
        public string LeftControllerName;

        public string RightManufacturer;
        public string RightControllerName;

        [SerializeField] private HVRControllerType LeftXRInputSystem = HVRControllerType.None;
        [SerializeField] private HVRControllerType RightControllerType = HVRControllerType.None;

        public List<string> LeftFeatures = new List<string>();
        public List<string> RightFeatures = new List<string>();
        public List<string> HMDFeatures = new List<string>();

        public HVRController LeftController;
        public HVRController RightController;

        public HVRController LeftXRInputController;
        public HVRController RightXRInputController;

        public HVRController LeftOculusController;
        public HVRController RightOculusController;

        public HVRController LeftSteamController;
        public HVRController RightSteamController;

        public HVRController LeftInputSystemController;
        public HVRController RightInputSystemController;

        public UnityEvent HMDFirstActivation = new UnityEvent();
        public UnityEvent HMDActivated = new UnityEvent();
        public UnityEvent HMDDeactivated = new UnityEvent();
        public UnityEvent HMDRecentered = new UnityEvent();
        public UnityEvent UserSensed = new UnityEvent();
        public UnityEvent UserNotSensed = new UnityEvent();

        public HVRControllerEvent LeftControllerConnected = new HVRControllerEvent();
        public HVRControllerEvent RightControllerConnected = new HVRControllerEvent();

        private readonly List<XRDisplaySubsystem> _displaySubsystems = new List<XRDisplaySubsystem>();
        private bool _applicationExiting;
        private InputDevice _hmdDevice;

        public InputDevice HMDDevice
        {
            get
            {
                if (_hmdDevice.isValid)
                    return _hmdDevice;
                _hmdDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
                return _hmdDevice;
            }
        }

        public bool PreviousHMDActive { get; private set; }
        public bool PreviousUserPresent { get; private set; }

        public bool HMDActive
        {
            get
            {
                if (HMDDevice.isValid)
                    return true;
                //beware this still returns true if the device is not rendering
                return XRSettings.isDeviceActive;
            }
        }

        public bool UserPresent
        {
            get
            {

                if (!HMDActive)
                    return false;

#if USING_XR_MANAGEMENT

                //https://forum.unity.com/threads/commonusages-userpresence-doesnt-report-correctly.818766/
                //this features requires XR Management
                //might be active but not rendering due to headset not on
                if (HMDDevice.TryGetFeatureValue(CommonUsages.userPresence, out var present))
                {
                    return present;
                }

#elif !UNITY_2020_1_OR_NEWER


                //https://stackoverflow.com/questions/51372771/how-to-check-if-a-hmd-in-unity-2018-is-in-use
#pragma warning disable 0618
                //if xr management wasn't detected use the old API for legacy VR
                if (XRDevice.userPresence == UserPresenceState.Present)
                    return true;
#pragma warning restore 0618
#endif

                //todo, test if this is a xrplugin only thing
                //https://docs.unity3d.com/ScriptReference/XR.XRDevice-isPresent.html

                _displaySubsystems.Clear();
                SubsystemManager.GetInstances<XRDisplaySubsystem>(_displaySubsystems);
                foreach (var xrDisplay in _displaySubsystems)
                {
                    if (xrDisplay.running)
                    {
                        return true;
                    }
                }


                return false;
            }
        }

        private InputDevice _leftDevice;
        public InputDevice LeftDevice
        {
            get
            {
                if (_leftDevice.isValid)
                    return _leftDevice;
                _leftDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                return _leftDevice;
            }
        }

        private InputDevice _rightDevice;
        public InputDevice RightDevice
        {
            get
            {
                if (_rightDevice.isValid)
                    return _rightDevice;
                _rightDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                return _rightDevice;
            }
        }




        private bool _isHMDFirstActivationReported;


        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this);
                return;
            }

            InputDevices.deviceConfigChanged += OnDeviceConfigChanged;
            InputDevices.deviceConnected += OnDeviceConnected;
            InputDevices.deviceDisconnected += OnDeviceDisconnected;

            if (!FingerSettings)
            {
                Debug.LogWarning($"HVRInputManager.FingerSettings not assigned, creating defaults.");

                FingerSettings = ScriptableObject.CreateInstance<HVRFingerSettings>();
                FingerSettings.Reset();
            }

            if (!GrabHaptics)
            {
                Debug.LogWarning($"HVRInputManager.GrabHaptics not assigned.");
                //GrabHaptics = ScriptableObject.CreateInstance<HVRGrabHaptics>();
                //GrabHaptics.Reset();
            }

            CheckXRStatus();
            if (IsVRInitialized)
            {
                Initialize();
            }
        }

        private void OnApplicationQuit()
        {
            _applicationExiting = true;
            StopXR();
        }

        private void OnEnable()
        {
            UpdateDevices();
        }

        private void UpdateDevices()
        {
            UpdateDeviceInformation(LeftDevice);
            UpdateLeftController(LeftDevice);
            UpdateDeviceInformation(RightDevice);
            UpdateRightController(RightDevice);

            UpdateDeviceInformation(HMDDevice);
        }

        private void Update()
        {
            //events aren't firing in build for some headsets?
            if (!HMDDevice.isValid)
            {
                UpdateHMD(HMDDevice);
            }

            CheckHMDEvents();
            CheckUserPresentEvents();

            PreviousHMDActive = HMDActive;
            PreviousUserPresent = UserPresent;

#if HVR_OCULUS

            if (UseOVRInputs && ForceOVRInputUpdate && (LeftXRInputSystem == HVRControllerType.Oculus || RightControllerType == HVRControllerType.Oculus))
            {
                HVROculusController.UpdateOVRInput();
            }
#endif

            //#if USING_XR_MANAGEMENT
            //            if (Input.GetKeyDown(KeyCode.X))
            //            {
            //                StartXR();
            //            }


            //#elif !UNITY_2020_1_OR_NEWER
            //            if (Input.GetKeyDown(KeyCode.X))
            //            {
            //                StartLegacyVR();
            //            }
            //#endif
            //            if (Input.GetKeyDown(KeyCode.Z))
            //            {
            //                StopXR();
            //            }
        }

        private void CheckHMDEvents()
        {
            if (!PreviousHMDActive && HMDActive)
            {
                if (!_isHMDFirstActivationReported)
                {
                    HMDFirstActivation.Invoke();
                    _isHMDFirstActivationReported = true;
                }

                HMDActivated.Invoke();
            }
            else if (PreviousHMDActive && !HMDActive)
            {
                HMDDeactivated.Invoke();
            }
        }

        private void CheckUserPresentEvents()
        {
            if (!PreviousUserPresent && UserPresent)
            {
                //Debug.Log($"user present");
                UserSensed.Invoke();
            }
            else if (PreviousUserPresent && !UserPresent)
            {
                //Debug.Log($"user not present");
                UserNotSensed.Invoke();
            }
        }


        private void Start()
        {
            UpdateDevices();
        }


        private void OnDeviceDisconnected(InputDevice device)
        {
            if (_applicationExiting)
                return;
            Debug.Log($"disconnected {device.name},{device.manufacturer}");
            UpdateDeviceInformation(device);
        }

        private void OnDeviceConnected(InputDevice device)
        {
            if (_applicationExiting)
                return;
            Debug.Log($"connected {device.name},{device.manufacturer}");
            //FYI: steamvr causes this to fire even if the controller is off if the controller was previously on.
            UpdateDeviceInformation(device);
        }

        private void OnDeviceConfigChanged(InputDevice device)
        {
            if (_applicationExiting)
                return;
            Debug.Log($"config changed {device.name},{device.manufacturer}");
            UpdateDeviceInformation(device);
        }


        private void UpdateDeviceInformation(InputDevice device)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
            {
                if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
                {
                    LeftControllerName = device.name;
                    LeftManufacturer = device.manufacturer;

                    UpdateLeftController(device);
                }

                if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
                {
                    RightControllerName = device.name;
                    RightManufacturer = device.manufacturer;

                    UpdateRightController(device);
                }
            }

            if (device.characteristics.HasFlag(InputDeviceCharacteristics.HeadMounted))
            {
                UpdateHMD(device);
            }
        }

        public Vector3 hmdpos;

        private void UpdateHMD(InputDevice device)
        {
            if (device.isValid)
            {
                HMDName = device.name;
                HMDManufacturer = device.manufacturer;

                HMDFeatures.Clear();
                var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();
                if (device.TryGetFeatureUsages(inputFeatures))
                {
                    foreach (var feature in inputFeatures)
                    {
                        HMDFeatures.Add($"{feature.name}");
                    }
                }
            }
        }

        private void UpdateRightController(InputDevice device)
        {
            //RightHasTrackPad = false;

            RightFeatures.Clear();
            var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();
            if (device.TryGetFeatureUsages(inputFeatures))
            {
                foreach (var feature in inputFeatures)
                {
                    RightFeatures.Add($"{feature.name}");
                    if (feature.name == "Secondary2DAxis")
                    {
                        //RightHasTrackPad = true;
                    }
                }
            }

            RightControllerType = GetController(RightDevice.manufacturer?.ToLower(), RightDevice.name?.ToLower());
            RightController = UpdateController(RightControllerType, device, HVRHandSide.Right);
            if (device.isValid)
                RightControllerConnected.Invoke(RightController);

        }

        private void UpdateLeftController(InputDevice device)
        {
            // LeftHasTrackPad = false;

            LeftFeatures.Clear();

            var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();
            if (device.TryGetFeatureUsages(inputFeatures))
            {
                foreach (var feature in inputFeatures)
                {
                    LeftFeatures.Add($"{feature.name}");
                    if (feature.name == "Secondary2DAxis")
                    {
                        // LeftHasTrackPad = true;
                    }
                }
            }

            LeftXRInputSystem = GetController(LeftDevice.manufacturer?.ToLower(), LeftDevice.name?.ToLower());
            LeftController = UpdateController(LeftXRInputSystem, device, HVRHandSide.Left);
            if (device.isValid)
                LeftControllerConnected.Invoke(LeftController);
        }

        private HVRController UpdateController(HVRControllerType controllerType, InputDevice device, HVRHandSide side)
        {
            HVRInputSettings inputMap = null;
            Vector2 deadZone;

            //var hasTrackPad = side == HVRHandSide.Left ? LeftHasTrackPad : RightHasTrackPad;
            var wasNone = CurrentSDK == InputSDK.None;
            GetSDK();

            if (CurrentSDK == InputSDK.Oculus)
            {
#if !HVR_OCULUS
                Debug.LogWarning($"OVRInputs cannot be used because HVR_OCULUS define symbol is missing. Oculus Asset and the Hurricane Oculus Integration is required to use OVRInputs." +
                                 $"Falling back to XRInputs.");
                CurrentSDK = InputSDK.XRInput;
#endif
            }

            if (wasNone && CurrentSDK != InputSDK.None)
            {
                Debug.Log($"InputSDK : {CurrentSDK}");
            }

            switch (controllerType)
            {
                case HVRControllerType.ReverbG2:
                    deadZone = WMRDeadzone;
                    inputMap = ReverbG2InputMap;
                    break;
                case HVRControllerType.WMR:
                    inputMap = WMRInputMap;
                    deadZone = WMRDeadzone;
                    break;
                case HVRControllerType.Oculus:
                    inputMap = OculusInputMap;
                    deadZone = OculusDeadzone;
                    break;
                case HVRControllerType.Vive:
                    deadZone = ViveDeadzone;
                    inputMap = ViveInputMap;
                    break;
                case HVRControllerType.Knuckles:
                    deadZone = KnucklesDeadzone;
                    inputMap = KnucklesInputMap;
                    break;
                case HVRControllerType.Cosmos:
                    inputMap = CosmosInputMap;
                    deadZone = CosmosDeadzone;
                    break;
                default:
                    inputMap = OculusInputMap;
                    deadZone = OculusDeadzone;
                    break;
            }


            if (!inputMap)
            {
                inputMap = OculusInputMap;
            }


            HVRController controller = null;

            if (side == HVRHandSide.Left)
            {
                if (LeftOculusController) LeftOculusController.enabled = false;
                if (LeftSteamController) LeftSteamController.enabled = false;
                if (LeftXRInputController) LeftXRInputController.enabled = false;
                if (LeftInputSystemController) LeftInputSystemController.enabled = false;
            }
            else
            {
                if (RightOculusController) RightOculusController.enabled = false;
                if (RightSteamController) RightSteamController.enabled = false;
                if (RightXRInputController) RightXRInputController.enabled = false;
                if (RightInputSystemController) RightInputSystemController.enabled = false;
            }

            switch (CurrentSDK)
            {
                case InputSDK.None:
                case InputSDK.XRInput:

                    controller = side == HVRHandSide.Left ? LeftXRInputController : RightXRInputController;

                    HVRXRInputController xrController = controller as HVRXRInputController;

                    if (!controller)
                    {
                        xrController = gameObject.AddComponent<HVRXRInputController>();
                        controller = xrController;

                        if (side == HVRHandSide.Left)
                        {
                            LeftXRInputController = controller;
                        }
                        else
                        {
                            RightXRInputController = controller;
                        }
                    }

                    break;
                case InputSDK.Oculus:

#if HVR_OCULUS
                    controller = side == HVRHandSide.Left ? LeftOculusController : RightOculusController;

                    if (!controller)
                    {
                        var oculusController = gameObject.AddComponent<HVROculusController>();
                        controller = oculusController;
                        oculusController.OVRHaptics = OVRHaptics;

                        if (side == HVRHandSide.Left)
                        {
                            LeftOculusController = controller;
                        }
                        else
                        {
                            RightOculusController = controller;
                        }
                    }
#endif

                    break;
                case InputSDK.SteamVR:

#if HVR_STEAMVR
                    controller = side == HVRHandSide.Left ? LeftSteamController : RightSteamController;
                    if (!controller)
                    {
                        var steamController = gameObject.AddComponent<HVRSteamVRController>();
                        controller = steamController;
                        if (side == HVRHandSide.Left)
                            LeftSteamController = steamController;
                        else
                            RightSteamController = steamController;
                    }

#endif
                    break;
                case InputSDK.InputSystem:
#if ENABLE_INPUT_SYSTEM
                    controller = side == HVRHandSide.Left ? LeftInputSystemController : RightInputSystemController;
                    if (!controller)
                    {
                        var inputSystemController = gameObject.AddComponent<HVRInputSystemController>();
                        inputSystemController.IsOpenXR = IsOpenXR;
                        controller = inputSystemController;
                        if (side == HVRHandSide.Left)
                        {
                            LeftInputSystemController = inputSystemController;
                        }
                        else
                        {
                            RightInputSystemController = inputSystemController;
                        }
                    }
#endif
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (OverrideDeadzone)
            {
                deadZone = DeadzoneOverride;
            }

            if (controller != null)
            {
                controller.XRNode = side == HVRHandSide.Left ? XRNode.LeftHand : XRNode.RightHand;
                controller.ThumbstickDeadZone = deadZone;
                controller.Side = side;
                controller.InputMap = inputMap;
                controller.enabled = true;
                controller.ControllerType = controllerType;
            }

            controller.FingerSettings = FingerSettings;

            return controller;
        }

        public HVRController GetController(HVRHandSide side)
        {
            return side == HVRHandSide.Left ? LeftController : RightController;
        }

        public InputDevice GetDevice(HVRHandSide side)
        {
            if (side == HVRHandSide.Left) return LeftDevice;
            return RightDevice;
        }

        private HVRControllerType GetController(string manufaturerToLower, string nameToLower)
        {
            if (string.IsNullOrWhiteSpace(manufaturerToLower) && string.IsNullOrWhiteSpace(nameToLower))
                return HVRControllerType.None;

            if (manufaturerToLower == null)
            {
                manufaturerToLower = "";
            }

            if (nameToLower == null)
            {
                nameToLower = "";
            }

            manufaturerToLower = manufaturerToLower.ToLower();
            nameToLower = nameToLower.ToLower();

            if (IsKnuckles(manufaturerToLower, nameToLower))
            {
                return HVRControllerType.Knuckles;
            }



            if (manufaturerToLower.Contains(Oculus))
            {
                return HVRControllerType.Oculus;
            }

            if (nameToLower.Contains(Cosmos))
                return HVRControllerType.Cosmos;

            if (manufaturerToLower.Contains(HTC) || nameToLower.Contains(Vive))
                return HVRControllerType.Vive;

            var isWindowsMixedReality = manufaturerToLower.Contains(WindowsMR) || nameToLower.Contains(WMRController) || nameToLower.Contains(WindowsMROpenXR.ToLower()) ||
                                        nameToLower.Contains(OpenXR_G2);

            if (isWindowsMixedReality)
            {
                if (nameToLower.Contains(ReverbG2.ToLower()) || nameToLower.Contains(OpenXR_G2.ToLower()))
                {
                    return HVRControllerType.ReverbG2;
                }

                var hmd = HMDName.ToLower();
                if (hmd.Contains(Reverb) && hmd.Contains(G2))
                {
                    return HVRControllerType.ReverbG2;
                }

                return HVRControllerType.WMR;
            }

            return HVRControllerType.None;
        }

        private bool IsKnuckles(string manufacturer, string controllerName)
        {
            if (controllerName.Contains(Knuckles))
                return true;
            if (controllerName.Contains(KnucklesOpenXR.ToLower())) //openxr
                return true;
            return false;
        }

        public void GetSDK()
        {
            if (!IsVRInitialized)
            {
                CurrentSDK = InputSDK.None;
                return;
            }

            if (CurrentSDK != InputSDK.None && !UseOVRInputs)
                return;

            var isXRPlugin = VRPlugin == VRMode.XRPlugin;
            var steamvrFound = CheckForSteamVR();
            var isLegacy = !isXRPlugin;

            if (steamvrFound && isLegacy)
            {
                CurrentSDK = InputSDK.SteamVR;
                return;
            }

#if ENABLE_INPUT_SYSTEM
            if (UseNewInputSystem)
            {
                CurrentSDK = InputSDK.InputSystem;
                return;
            }
#endif

            if (isXRPlugin)
            {
                var loader = XRPluginLoader.ToLower();
                if (loader == OculusLoader.ToLower())
                {
                    CurrentSDK = UseOVRInputs ? InputSDK.Oculus : InputSDK.XRInput;
                    return;
                }

#if ENABLE_INPUT_SYSTEM
                if (loader == OpenXRLoader.ToLower())
                {
                    CurrentSDK = InputSDK.InputSystem;
                    return;
                }
#endif

                if (loader == OpenVRLoader.ToLower())
                {
                    if (!steamvrFound)
                    {
                        Debug.LogWarning($"HVR: {OpenVRLoader} active without SteamVR installed or HVR_STEAMVR define set.");
                    }
                    CurrentSDK = InputSDK.SteamVR;
                    return;
                }


                CurrentSDK = InputSDK.XRInput;
                return;
            }

            //legacy vr
            if (LeftXRInputSystem == HVRControllerType.Oculus || RightControllerType == HVRControllerType.Oculus)
            {
                CurrentSDK = UseOVRInputs ? InputSDK.Oculus : InputSDK.XRInput;
                return;
            }

            CurrentSDK = InputSDK.XRInput;
        }

        private bool CheckForSteamVR()
        {
#if !HVR_STEAMVR
            return false;
#else

            //OPENVR_DESKTOP and OPENVR_XR from HurricaneVR.Framework asmdef version defines

#if !OPENVR_DESKTOP && !OPENVR_XR
            return false;
#else

#if OPENVR_DESKTOP

            if (!Valve.VR.SteamVR.usingNativeSupport)
            {
                return false;
            }
#endif

            try
            {
                EVRInitError error = EVRInitError.None;
                EVRInitError error1 = EVRInitError.None;
                EVRInitError error2 = EVRInitError.None;


                OpenVR.GetGenericInterface(OpenVR.IVRCompositor_Version, ref error);
                OpenVR.GetGenericInterface(OpenVR.IVROverlay_Version, ref error1);
                OpenVR.GetGenericInterface(OpenVR.IVRInput_Version, ref error2);

                if (error != EVRInitError.None || error1 != EVRInitError.None || error2 != EVRInitError.None)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }

            return true;
#endif
#endif
        }

        private void CheckXRStatus()
        {
#if USING_XR_MANAGEMENT

            VRPlugin = VRMode.XRPlugin;

            XRPluginLoader = XRGeneralSettings.Instance.Manager.activeLoader?.name ?? "";

            string loaders = "";

#if USING_XR_MANAGEMENT_V4
            loaders = string.Join(",", XRGeneralSettings.Instance.Manager.activeLoaders.Select(e => e.name));
#else
            loaders = string.Join(",", XRGeneralSettings.Instance.Manager.loaders.Select(e => e.name));
#endif
            Debug.Log($"{  XRGeneralSettings.Instance.Manager.automaticLoading}");
            Debug.Log($"XRGeneralSettings.Instance.Manager.isInitializationComplete { XRGeneralSettings.Instance.Manager.isInitializationComplete}");
            Debug.Log($"XRPlugin Detected | XRSettings.enabled {XRSettings.enabled} | Loader : {XRPluginLoader} | Loaders Enabled: {loaders}");

            XRPluginActive = XRSettings.enabled && XRGeneralSettings.Instance.Manager.isInitializationComplete;
#elif !UNITY_2020_1_OR_NEWER

            var legacyDevices = "";
            foreach (var t in XRSettings.supportedDevices)
            {
                legacyDevices += t + ",";
            }

            Debug.Log($"Legacy VR Detected | XRSettings.enabled {XRSettings.enabled} | SDKs: {legacyDevices}");

            VRPlugin = VRMode.Legacy;

            LegacyActive = XRSettings.enabled && !string.IsNullOrWhiteSpace(XRSettings.loadedDeviceName);
#endif
        }

        public bool StartXR()
        {
#if USING_XR_MANAGEMENT
            Debug.Log($"Attempting to start XR Plugin");
            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            XRPluginLoader = XRGeneralSettings.Instance.Manager.activeLoader?.name ?? "";
            if (!string.IsNullOrEmpty(XRPluginLoader))
            {
                Debug.Log($"{XRPluginLoader} loader started. Starting subsystems");
                XRGeneralSettings.Instance.Manager.StartSubsystems();
                XRPluginActive = XRSettings.enabled && XRGeneralSettings.Instance.Manager.isInitializationComplete;
                Initialize();
                return true;
            }
            else
            {
                XRPluginActive = false;
                Debug.LogWarning($"Failed to start the xr plugin manually");
                return false;
            }
#else
            Debug.LogWarning($"XR Plugin not detected. Cannot start XR.");
            return false;
#endif
        }



        public void StopXR()
        {
#if USING_XR_MANAGEMENT
            Debug.Log($"stopping xr plugin");
            if (XRGeneralSettings.Instance && XRGeneralSettings.Instance.Manager &&
                XRGeneralSettings.Instance.Manager.activeLoader != null && XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
                XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            }

            OnXRStopped();
#elif !UNITY_2020_1_OR_NEWER
            Debug.Log($"stopping legacy xr");
            OnXRStopped();
            XRSettings.enabled = false;
            if (XRSettings.supportedDevices.Contains(LegacyNone))
            {
                Debug.Log($"loading legacy 'None'");
                XRSettings.LoadDeviceByName(LegacyNone);
            }
#endif
        }

        /// <summary>
        /// Starting requires a coroutine, the callback will be fired with true for success, and false for failure
        /// </summary>
        public void StartLegacyVR(Action<bool> callback = null)
        {
#if USING_XR_MANAGEMENT
            Debug.LogWarning($"XR Plugin Management is in use. Cannot load Legacy VR");
            return;
#elif !UNITY_2020_1_OR_NEWER

            StartCoroutine(LoadLegacy(callback));
#endif

        }


#if!UNITY_2020_1_OR_NEWER

        private IEnumerator LoadLegacy(Action<bool> callback)
        {
            foreach (var sdkName in XRSettings.supportedDevices)
            {
                if (sdkName == "None")
                    continue;

                Debug.Log($"Attempting to start {sdkName} SDK");
                XRSettings.LoadDeviceByName(sdkName);
                yield return null;
                if (XRSettings.loadedDeviceName == sdkName)
                {
                    XRSettings.enabled = true;
                    break;
                }
            }

            if (XRSettings.loadedDeviceName != null && XRSettings.loadedDeviceName != LegacyNone)
            {
                Debug.Log($"Legacy VR Initialized with {XRSettings.loadedDeviceName}. Updating Devices.");
                LegacyActive = true;
                Initialize();
                if (callback != null)
                {
                    callback(true);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback(false);
                }
                LegacyActive = false;
            }
        }

#endif

        /// <summary>
        /// sets up steamvr if necessary, set up controller objects, applies tracking origin
        /// </summary>
        public void Initialize()
        {
            InitSteamVR();
            UpdateDevices();
            ApplyTrackingOrigin();
        }

        private void OnXRStopped()
        {
            StopSteamVR();
            LegacyActive = false;
            XRPluginActive = false;
            CurrentSDK = InputSDK.None;
            UpdateDevices();
        }

        private void InitSteamVR()
        {
            if (!InitializeSteamVR || !CheckForSteamVR())
                return;

#if !HVR_STEAMVR
            return;
#else

            if (!IsVRInitialized)
            {
                Debug.LogWarning($"Cannot InitSteamVR. VR not initialized yet.");
                return;
            }

            Valve.VR.SteamVR.Initialize();


            if (InitializeSteamVRActions)
            {
                var actionSet = SteamVR_Input.GetActionSet(@"\actions\HVR");
                actionSet.Activate();
            }

            IsSteamVR = true;
#endif

        }

        private void StopSteamVR()
        {
#if !HVR_STEAMVR
            return;
#else

            Valve.VR.SteamVR.SafeDispose();


            if (InitializeSteamVRActions)
            {
                var actionSet = SteamVR_Input.GetActionSet(@"\actions\HVR");
                actionSet.Deactivate();
            }
#endif
        }

        public void ApplyTrackingOrigin()
        {
            if (IsVRInitialized)
            {
                StartCoroutine(UpdateTrackingOrigin(TrackingSpace));
            }
        }

        private IEnumerator UpdateTrackingOrigin(TrackingOriginModeFlags originFlags)
        {
            yield return null;

#if USING_XR_MANAGEMENT
            var subsystems = new List<XRInputSubsystem>();
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
            Debug.Log("Found " + subsystems.Count + " input subsystems.");

            for (int i = 0; i < subsystems.Count; i++)
            {
                if (subsystems[i].TrySetTrackingOriginMode(originFlags))
                    Debug.Log("Successfully set TrackingOriginMode to Floor");
                else
                    Debug.Log("Failed to set TrackingOriginMode to Floor");
            }

#elif !UNITY_2020_1_OR_NEWER


            if (originFlags == TrackingOriginModeFlags.Floor)
            {
#pragma warning disable 0618
                if (XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale))
                {
                    Debug.Log("Tracking change to RoomScale.");
                }
                else
                {
                    Debug.Log("Failed Tracking change to RoomScale.");
                }
            }
            else
            {
                XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
#pragma warning restore 0618
                Debug.Log("Tracking change to stationary.");
            }

#endif

        }
    }

    public enum VRMode
    {
        Legacy,
        XRPlugin
    }

}
