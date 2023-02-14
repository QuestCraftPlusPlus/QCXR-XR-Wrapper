using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif


namespace HurricaneVR.Framework.Core.Player
{
    public class HVRCameraRig : MonoBehaviour
    {
        public const string HeightKey = "SaveHVRHeight";

        /// <summary>
        /// HMD transform used to detect HMD movement at start
        /// </summary>
        [Header("Required Transforms")] public Transform Camera;

        /// <summary>
        /// Camera rig child used to offset the Y height of the HMD.
        /// </summary>
        public Transform FloorOffset;

        /// <summary>
        /// Camera's parent transform used to scale the camera and tracked device transforms when in standing mode.
        /// </summary>
        public Transform CameraScale;

        /// <summary>
        /// Manually setting this will raise / lower the HMD
        /// </summary>
        [Header("Manual Camera Offsetting")] [Tooltip("Manually modify the camera height if needed")]
        public float CameraYOffset;

        /// <summary>
        /// Used with height calibration to define the virtual player's HMD sight line.
        /// </summary>
        [FormerlySerializedAs("PlayerHeight")] [Tooltip("Height of the virtual player")]
        public float EyeHeight = 1.66f;

        /// <summary>
        /// Sitting - FloorOffset height is adjusted based on the difference between the HMD height and the EyeHeight.
        /// Standing - CameraScale is scaled based on the ratio of EyeHeight / HMD height.
        /// PlayerHeight - No offsetting or scaling performed.
        /// </summary>
        [Tooltip("Sitting or standing mode")] public HVRSitStand SitStanding = HVRSitStand.PlayerHeight;

        [Header("Debug Height Offsets")] [Tooltip("If true, use up and down arrow to change YOffset to help with testing.")]
        public bool DebugKeyboardOffset;

        public float DebugKeyboardIncrement = .25f;

        [Header("Debug Height Calibration")]
        /// <summary>
        /// Calibration height is saved to player prefs when height is calibrated.
        /// </summary>
        [Tooltip("Calibration height is saved to player prefs when height is calibrated.")]
        public bool SaveCalibrationHeight;

        /// <summary>
        /// None - no initial calibration
        /// HMDMoved - Calibrates the height after the HMD moves more than DebugCalibMovedThreshold .
        /// Immediately - Calibrates height immediately
        /// </summary>
        public HVRDebugCalibrate DebugCalibMode = HVRDebugCalibrate.None;

        /// <summary>
        /// When DebugCalibMode is HMDMoved - how much the HMD needs to move at start to trigger the auto calibration
        /// </summary>
        public float DebugCalibMovedThreshold = .05f;


        /// <summary>
        /// If true the height can be calibrated using the RelicbrateKey
        /// </summary>
        public bool DebugKeyboardRecalibrate;

#if ENABLE_LEGACY_INPUT_MANAGER

        public KeyCode RecalibrateKey = KeyCode.R;

#elif ENABLE_INPUT_SYSTEM
        public Key HeightCalibrateKey = Key.R;

#endif

        /// <summary>
        /// Set by the player controller to raise / lower the camera rig when performing a input driven crouch.
        /// </summary>
        [Header("For Debugging Display only")] public float PlayerControllerYOffset = 0f;

        /// <summary>
        /// Floor offset height adjusted by the Standing camera scale (if any).
        /// </summary>
        public float AdjustedCameraHeight;

        /// <summary>
        /// Delta between the hmd height and the EyeHeight
        /// </summary>
        public float SittingOffset;

        private Vector3 _cameraStartingPosition;
        private float _scale = 1f;

        /// <summary>
        /// Set to false on player prefab clones if using multiplayer frameworks like PUN
        /// </summary>
        public bool IsMine { get; set; } = true;


        protected virtual void Start()
        {
            if (DebugCalibMode == HVRDebugCalibrate.HMDMoved)
            {
                StartCoroutine(CalibrateOnHMDMoved());
            }
            else if (DebugCalibMode == HVRDebugCalibrate.Immediately)
            {
                DebugCalibrate();
            }
        }

        private void DebugCalibrate()
        {
            if (SaveCalibrationHeight)
            {
                CalibrateFromSaved();
            }
            else
            {
                Calibrate();
            }
        }

        protected virtual void CalibrateFromSaved()
        {
            if (PlayerPrefs.HasKey(HeightKey))
            {
                var height = PlayerPrefs.GetFloat(HeightKey);
                CalibrateHeight(height);
            }
            else
            {
                Calibrate();
            }
        }

        private IEnumerator CalibrateOnHMDMoved()
        {
            yield return null;
            _cameraStartingPosition = Camera.localPosition;

            while (Vector3.Distance(_cameraStartingPosition, Camera.transform.localPosition) < DebugCalibMovedThreshold)
            {
                yield return null;
            }

            //Debug.Log($"Camera movement detected, calibrating height.");
            DebugCalibrate();
        }

        protected virtual void Update()
        {
            UpdateFloorOffset();

            AdjustedCameraHeight = FloorOffset.transform.localPosition.y + Camera.localPosition.y * _scale;

            DebugHeightOffset();
        }

        /// <summary>
        /// Returns the Y offset to apply to the camera rig.
        /// </summary>
        protected virtual float GetYOffset()
        {
            return SittingOffset + CameraYOffset + PlayerControllerYOffset;
        }

        private void UpdateFloorOffset()
        {
            if (FloorOffset)
            {
                var pos = FloorOffset.transform.localPosition;
                var intendedOffset = GetYOffset();
                FloorOffset.transform.localPosition = new Vector3(pos.x, intendedOffset, pos.z);
            }
        }

        private void DebugHeightOffset()
        {
            if (IsMine)
            {
                if (DebugKeyboardOffset)
                {
#if ENABLE_LEGACY_INPUT_MANAGER

                    if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        CameraYOffset += -DebugKeyboardIncrement;
                    }
                    else if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        CameraYOffset += DebugKeyboardIncrement;
                    }
#elif ENABLE_INPUT_SYSTEM
                if (Keyboard.current[Key.UpArrow].wasPressedThisFrame)
                {
                    CameraYOffset += DebugKeyboardIncrement;
                }
                else if (Keyboard.current[Key.DownArrow].wasPressedThisFrame)
                {
                    CameraYOffset += -DebugKeyboardIncrement;
                }
#endif
                }

                if (DebugKeyboardRecalibrate)
                {
#if ENABLE_LEGACY_INPUT_MANAGER
                    if (Input.GetKeyDown(RecalibrateKey))
                    {
                        Calibrate();
                    }
#elif ENABLE_INPUT_SYSTEM
            if (Keyboard.current[HeightCalibrateKey].wasPressedThisFrame)
            {
               Calibrate();
            }
#endif
                }
            }
        }

        /// <summary>
        /// Calibrates the camera rig height.
        /// </summary>
        /// <param name="height">Height to compare against - usually the HMD device height or a saved value from a prior calibration.</param>
        public virtual void CalibrateHeight(float height)
        {
            //CalibratedHeight = height;

            if (SitStanding == HVRSitStand.Standing)
            {
                if (height < .01f)
                {
                    height = EyeHeight;
                }

                SittingOffset = 0f;
                _scale = EyeHeight / height;
            }
            else if (SitStanding == HVRSitStand.Sitting)
            {
                SittingOffset = EyeHeight - height;
                _scale = 1f;
            }
            else if (SitStanding == HVRSitStand.PlayerHeight)
            {
                SittingOffset = 0f;
                _scale = 1f;
            }

            if (CameraScale)
            {
                CameraScale.localScale = new Vector3(_scale, _scale, _scale);
            }
        }


        /// <summary>
        /// Calibrates the height using the HMD height.
        /// </summary>
        public void Calibrate()
        {
            CalibrateHeight(Camera.localPosition.y + CameraYOffset);
            if (SaveCalibrationHeight)
            {
                PlayerPrefs.SetFloat(HeightKey, Camera.localPosition.y);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// Updates the height calibration mode and forces a height calibration that uses the HMD height.
        /// </summary>
        public void SetSitStandMode(HVRSitStand sitStand)
        {
            if (sitStand == HVRSitStand.Standing && !CameraScale)
            {
                Debug.LogWarning($"Standing mode cannot be set without the CameraScale transform assigned and setup properly.");
                sitStand = HVRSitStand.PlayerHeight;
            }

            SitStanding = sitStand;
            Calibrate();
        }
    }

    public enum HVRSitStand
    {
        Sitting,
        Standing,
        PlayerHeight
    }

    public enum HVRDebugCalibrate
    {
        None,
        HMDMoved,
        Immediately
    }
}