using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.ControllerInput
{
    public class HVRGlobalInputs : MonoBehaviour
    {
        public static HVRGlobalInputs Instance { get; private set; }

        public float LeftTrigger;
        public float LeftGrip;
        public float RightTrigger;
        public float RightGrip;

        public Vector2 LeftJoystickAxis;
        public Vector2 LeftTrackpadAxis;

        public Vector2 RightJoystickAxis;
        public Vector2 RightTrackpadAxis;

        public HVRButtonState LeftGripButtonState;
        public HVRButtonState LeftTriggerButtonState;
        public HVRButtonState LeftPrimaryButtonState;
        public HVRButtonState LeftSecondaryButtonState;
        public HVRButtonState LeftMenuButtonState;
        public HVRButtonState LeftPrimaryTouchButtonState;
        public HVRButtonState LeftSecondaryTouchButtonState;
        public HVRButtonState LeftJoystickButtonState;
        public HVRButtonState LeftTrackpadButtonState;

        public HVRButtonState LeftJoystickTouchState;
        public HVRButtonState LeftTrackPadTouchState;
        public HVRButtonState LeftTriggerTouchState;
        public HVRButtonState LeftThumbTouchState;

        public HVRButtonState LeftTriggerNearTouchState;
        public HVRButtonState LeftThumbNearTouchState;

        public HVRButtonState LeftTrackPadUp;
        public HVRButtonState LeftTrackPadLeft;
        public HVRButtonState LeftTrackPadRight;
        public HVRButtonState LeftTrackPadDown;


        public HVRButtonState RightGripButtonState;
        public HVRButtonState RightTriggerButtonState;
        public HVRButtonState RightPrimaryButtonState;
        public HVRButtonState RightSecondaryButtonState;
        public HVRButtonState RightMenuButtonState;
        public HVRButtonState RightPrimaryTouchButtonState;
        public HVRButtonState RightSecondaryTouchButtonState;
        public HVRButtonState RightJoystickButtonState;
        public HVRButtonState RightTrackpadButtonState;

        public HVRButtonState RightJoystickTouchState;
        public HVRButtonState RightTrackPadTouchState;
        public HVRButtonState RightTriggerTouchState;
        public HVRButtonState RightThumbTouchState;

        public HVRButtonState RightTriggerNearTouchState;
        public HVRButtonState RightThumbNearTouchState;

        public HVRButtonState RightTrackPadUp;
        public HVRButtonState RightTrackPadLeft;
        public HVRButtonState RightTrackPadRight;
        public HVRButtonState RightTrackPadDown;


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
        }

        private void Update()
        {
            var controller = HVRInputManager.Instance.LeftController;
            LeftTrigger = controller.Trigger;
            LeftGrip = controller.Grip;

            LeftJoystickAxis = controller.JoystickAxis;
            LeftTrackpadAxis = controller.TrackpadAxis;

            LeftGripButtonState = controller.GripButtonState;
            LeftTriggerButtonState = controller.TriggerButtonState;
            LeftPrimaryButtonState = controller.PrimaryButtonState;
            LeftSecondaryButtonState = controller.SecondaryButtonState;
            LeftMenuButtonState = controller.MenuButtonState;
            LeftPrimaryTouchButtonState = controller.PrimaryTouchButtonState;
            LeftSecondaryTouchButtonState = controller.SecondaryTouchButtonState;
            LeftJoystickButtonState = controller.JoystickButtonState;
            LeftTrackpadButtonState = controller.TrackpadButtonState;

            LeftJoystickTouchState = controller.JoystickTouchState;
            LeftTrackPadTouchState = controller.TrackPadTouchState;
            LeftTriggerTouchState = controller.TriggerTouchState;
            LeftThumbTouchState = controller.ThumbTouchState;

            LeftTriggerNearTouchState = controller.TriggerNearTouchState;
            LeftThumbNearTouchState = controller.ThumbNearTouchState;

            LeftTrackPadUp = controller.TrackPadUp;
            LeftTrackPadLeft = controller.TrackPadLeft;
            LeftTrackPadRight = controller.TrackPadRight;
            LeftTrackPadDown = controller.TrackPadDown;

            controller = HVRInputManager.Instance.RightController;

            RightTrigger = controller.Trigger;
            RightGrip = controller.Grip;

            RightJoystickAxis = controller.JoystickAxis;
            RightTrackpadAxis = controller.TrackpadAxis;

            RightGripButtonState = controller.GripButtonState;
            RightTriggerButtonState = controller.TriggerButtonState;
            RightPrimaryButtonState = controller.PrimaryButtonState;
            RightSecondaryButtonState = controller.SecondaryButtonState;
            RightMenuButtonState = controller.MenuButtonState;
            RightPrimaryTouchButtonState = controller.PrimaryTouchButtonState;
            RightSecondaryTouchButtonState = controller.SecondaryTouchButtonState;
            RightJoystickButtonState = controller.JoystickButtonState;
            RightTrackpadButtonState = controller.TrackpadButtonState;

            RightJoystickTouchState = controller.JoystickTouchState;
            RightTrackPadTouchState = controller.TrackPadTouchState;
            RightTriggerTouchState = controller.TriggerTouchState;
            RightThumbTouchState = controller.ThumbTouchState;

            RightTriggerNearTouchState = controller.TriggerNearTouchState;
            RightThumbNearTouchState = controller.ThumbNearTouchState;

            RightTrackPadUp = controller.TrackPadUp;
            RightTrackPadLeft = controller.TrackPadLeft;
            RightTrackPadRight = controller.TrackPadRight;
            RightTrackPadDown = controller.TrackPadDown;
        }
    }
}