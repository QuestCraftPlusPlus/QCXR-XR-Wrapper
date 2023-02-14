using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.ControllerInput
{
    public class HVRControllerEvents : MonoBehaviour
    {
        public static HVRControllerEvents Instance { get; private set; }

        public UnityEvent LeftGripActivated = new UnityEvent();
        public UnityEvent LeftGripDeactivated = new UnityEvent();
        public UnityEvent RightGripActivated = new UnityEvent();
        public UnityEvent RightGripDeactivated = new UnityEvent();

        public UnityEvent LeftTriggerActivated = new UnityEvent();
        public UnityEvent LeftTriggerDeactivated = new UnityEvent();
        public UnityEvent RightTriggerActivated = new UnityEvent();
        public UnityEvent RightTriggerDeactivated = new UnityEvent();

        public UnityEvent LeftPrimaryActivated = new UnityEvent();
        public UnityEvent LeftPrimaryDeactivated = new UnityEvent();
        public UnityEvent RightPrimaryActivated = new UnityEvent();
        public UnityEvent RightPrimaryDeactivated = new UnityEvent();

        public UnityEvent LeftSecondaryActivated = new UnityEvent();
        public UnityEvent LeftSecondaryDeactivated = new UnityEvent();
        public UnityEvent RightSecondaryActivated = new UnityEvent();
        public UnityEvent RightSecondaryDeactivated = new UnityEvent();

        public UnityEvent LeftMenuActivated = new UnityEvent();
        public UnityEvent LeftMenuDeactivated = new UnityEvent();
        public UnityEvent RightMenuActivated = new UnityEvent();
        public UnityEvent RightMenuDeactivated = new UnityEvent();

        public UnityEvent LeftPrimaryTouchActivated = new UnityEvent();
        public UnityEvent LeftPrimaryTouchDeactivated = new UnityEvent();
        public UnityEvent RightPrimaryTouchActivated = new UnityEvent();
        public UnityEvent RightPrimaryTouchDeactivated = new UnityEvent();

        public UnityEvent LeftSecondaryTouchActivated = new UnityEvent();
        public UnityEvent LeftSecondaryTouchDeactivated = new UnityEvent();
        public UnityEvent RightSecondaryTouchActivated = new UnityEvent();
        public UnityEvent RightSecondaryTouchDeactivated = new UnityEvent();

        public UnityEvent LeftJoystickActivated = new UnityEvent();
        public UnityEvent LeftJoystickDeactivated = new UnityEvent();
        public UnityEvent RightJoystickActivated = new UnityEvent();
        public UnityEvent RightJoystickDeactivated = new UnityEvent();

        public UnityEvent LeftTrackpadActivated = new UnityEvent();
        public UnityEvent LeftTrackpadDeactivated = new UnityEvent();
        public UnityEvent RightTrackpadActivated = new UnityEvent();
        public UnityEvent RightTrackpadDeactivated = new UnityEvent();

        public UnityEvent LeftJoystickTouchActivated = new UnityEvent();
        public UnityEvent LeftJoystickTouchDeactivated = new UnityEvent();
        public UnityEvent RightJoystickTouchActivated = new UnityEvent();
        public UnityEvent RightJoystickTouchDeactivated = new UnityEvent();

        public UnityEvent LeftTrackPadTouchActivated = new UnityEvent();
        public UnityEvent LeftTrackPadTouchDeactivated = new UnityEvent();
        public UnityEvent RightTrackPadTouchActivated = new UnityEvent();
        public UnityEvent RightTrackPadTouchDeactivated = new UnityEvent();

        public UnityEvent LeftTriggerTouchActivated = new UnityEvent();
        public UnityEvent LeftTriggerTouchDeactivated = new UnityEvent();
        public UnityEvent RightTriggerTouchActivated = new UnityEvent();
        public UnityEvent RightTriggerTouchDeactivated = new UnityEvent();

        public UnityEvent LeftThumbTouchActivated = new UnityEvent();
        public UnityEvent LeftThumbTouchDeactivated = new UnityEvent();
        public UnityEvent RightThumbTouchActivated = new UnityEvent();
        public UnityEvent RightThumbTouchDeactivated = new UnityEvent();

        public UnityEvent LeftTrackPadUpActivated = new UnityEvent();
        public UnityEvent LeftTrackPadUpDeactivated = new UnityEvent();
        public UnityEvent RightTrackPadUpActivated = new UnityEvent();
        public UnityEvent RightTrackPadUpDeactivated = new UnityEvent();

        public UnityEvent LeftTrackPadLeftActivated = new UnityEvent();
        public UnityEvent LeftTrackPadLeftDeactivated = new UnityEvent();
        public UnityEvent RightTrackPadLeftActivated = new UnityEvent();
        public UnityEvent RightTrackPadLeftDeactivated = new UnityEvent();

        public UnityEvent LeftTrackPadRightActivated = new UnityEvent();
        public UnityEvent LeftTrackPadRightDeactivated = new UnityEvent();
        public UnityEvent RightTrackPadRightActivated = new UnityEvent();
        public UnityEvent RightTrackPadRightDeactivated = new UnityEvent();

        public UnityEvent LeftTrackPadDownActivated = new UnityEvent();
        public UnityEvent LeftTrackPadDownDeactivated = new UnityEvent();
        public UnityEvent RightTrackPadDownActivated = new UnityEvent();
        public UnityEvent RightTrackPadDownDeactivated = new UnityEvent();



        private HVRGlobalInputs _globalInputs;
        private bool _hasInputs;

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

            //testing...
            //LeftGripActivated.AddListener(() => { Debug.Log("LeftGripActivated"); });
            //LeftGripDeactivated.AddListener(() => { Debug.Log("LeftGripDeactivated"); });
            //RightGripActivated.AddListener(() => { Debug.Log("RightGripActivated"); });
            //RightGripDeactivated.AddListener(() => { Debug.Log("RightGripDeactivated"); });
            //LeftTriggerActivated.AddListener(() => { Debug.Log("LeftTriggerActivated"); });
            //LeftTriggerDeactivated.AddListener(() => { Debug.Log("LeftTriggerDeactivated"); });
            //RightTriggerActivated.AddListener(() => { Debug.Log("RightTriggerActivated"); });
            //RightTriggerDeactivated.AddListener(() => { Debug.Log("RightTriggerDeactivated"); });
            //LeftPrimaryActivated.AddListener(() => { Debug.Log("LeftPrimaryActivated"); });
            //LeftPrimaryDeactivated.AddListener(() => { Debug.Log("LeftPrimaryDeactivated"); });
            //RightPrimaryActivated.AddListener(() => { Debug.Log("RightPrimaryActivated"); });
            //RightPrimaryDeactivated.AddListener(() => { Debug.Log("RightPrimaryDeactivated"); });
            //LeftSecondaryActivated.AddListener(() => { Debug.Log("LeftSecondaryActivated"); });
            //LeftSecondaryDeactivated.AddListener(() => { Debug.Log("LeftSecondaryDeactivated"); });
            //RightSecondaryActivated.AddListener(() => { Debug.Log("RightSecondaryActivated"); });
            //RightSecondaryDeactivated.AddListener(() => { Debug.Log("RightSecondaryDeactivated"); });
            //LeftMenuActivated.AddListener(() => { Debug.Log("LeftMenuActivated"); });
            //LeftMenuDeactivated.AddListener(() => { Debug.Log("LeftMenuDeactivated"); });
            //RightMenuActivated.AddListener(() => { Debug.Log("RightMenuActivated"); });
            //RightMenuDeactivated.AddListener(() => { Debug.Log("RightMenuDeactivated"); });
            //LeftPrimaryTouchActivated.AddListener(() => { Debug.Log("LeftPrimaryTouchActivated"); });
            //LeftPrimaryTouchDeactivated.AddListener(() => { Debug.Log("LeftPrimaryTouchDeactivated"); });
            //RightPrimaryTouchActivated.AddListener(() => { Debug.Log("RightPrimaryTouchActivated"); });
            //RightPrimaryTouchDeactivated.AddListener(() => { Debug.Log("RightPrimaryTouchDeactivated"); });
            //LeftSecondaryTouchActivated.AddListener(() => { Debug.Log("LeftSecondaryTouchActivated"); });
            //LeftSecondaryTouchDeactivated.AddListener(() => { Debug.Log("LeftSecondaryTouchDeactivated"); });
            //RightSecondaryTouchActivated.AddListener(() => { Debug.Log("RightSecondaryTouchActivated"); });
            //RightSecondaryTouchDeactivated.AddListener(() => { Debug.Log("RightSecondaryTouchDeactivated"); });
            //LeftJoystickActivated.AddListener(() => { Debug.Log("LeftJoystickActivated"); });
            //LeftJoystickDeactivated.AddListener(() => { Debug.Log("LeftJoystickDeactivated"); });
            //RightJoystickActivated.AddListener(() => { Debug.Log("RightJoystickActivated"); });
            //RightJoystickDeactivated.AddListener(() => { Debug.Log("RightJoystickDeactivated"); });
            //LeftTrackpadActivated.AddListener(() => { Debug.Log("LeftTrackpadActivated"); });
            //LeftTrackpadDeactivated.AddListener(() => { Debug.Log("LeftTrackpadDeactivated"); });
            //RightTrackpadActivated.AddListener(() => { Debug.Log("RightTrackpadActivated"); });
            //RightTrackpadDeactivated.AddListener(() => { Debug.Log("RightTrackpadDeactivated"); });
            //LeftJoystickTouchActivated.AddListener(() => { Debug.Log("LeftJoystickTouchActivated"); });
            //LeftJoystickTouchDeactivated.AddListener(() => { Debug.Log("LeftJoystickTouchDeactivated"); });
            //RightJoystickTouchActivated.AddListener(() => { Debug.Log("RightJoystickTouchActivated"); });
            //RightJoystickTouchDeactivated.AddListener(() => { Debug.Log("RightJoystickTouchDeactivated"); });
            //LeftTrackPadTouchActivated.AddListener(() => { Debug.Log("LeftTrackPadTouchActivated"); });
            //LeftTrackPadTouchDeactivated.AddListener(() => { Debug.Log("LeftTrackPadTouchDeactivated"); });
            //RightTrackPadTouchActivated.AddListener(() => { Debug.Log("RightTrackPadTouchActivated"); });
            //RightTrackPadTouchDeactivated.AddListener(() => { Debug.Log("RightTrackPadTouchDeactivated"); });
            //LeftTriggerTouchActivated.AddListener(() => { Debug.Log("LeftTriggerTouchActivated"); });
            //LeftTriggerTouchDeactivated.AddListener(() => { Debug.Log("LeftTriggerTouchDeactivated"); });
            //RightTriggerTouchActivated.AddListener(() => { Debug.Log("RightTriggerTouchActivated"); });
            //RightTriggerTouchDeactivated.AddListener(() => { Debug.Log("RightTriggerTouchDeactivated"); });
            //LeftThumbTouchActivated.AddListener(() => { Debug.Log("LeftThumbTouchActivated"); });
            //LeftThumbTouchDeactivated.AddListener(() => { Debug.Log("LeftThumbTouchDeactivated"); });
            //RightThumbTouchActivated.AddListener(() => { Debug.Log("RightThumbTouchActivated"); });
            //RightThumbTouchDeactivated.AddListener(() => { Debug.Log("RightThumbTouchDeactivated"); });
            //LeftTrackPadUpActivated.AddListener(() => { Debug.Log("LeftTrackPadUpActivated"); });
            //LeftTrackPadUpDeactivated.AddListener(() => { Debug.Log("LeftTrackPadUpDeactivated"); });
            //RightTrackPadUpActivated.AddListener(() => { Debug.Log("RightTrackPadUpActivated"); });
            //RightTrackPadUpDeactivated.AddListener(() => { Debug.Log("RightTrackPadUpDeactivated"); });
            //LeftTrackPadLeftActivated.AddListener(() => { Debug.Log("LeftTrackPadLeftActivated"); });
            //LeftTrackPadLeftDeactivated.AddListener(() => { Debug.Log("LeftTrackPadLeftDeactivated"); });
            //RightTrackPadLeftActivated.AddListener(() => { Debug.Log("RightTrackPadLeftActivated"); });
            //RightTrackPadLeftDeactivated.AddListener(() => { Debug.Log("RightTrackPadLeftDeactivated"); });
            //LeftTrackPadRightActivated.AddListener(() => { Debug.Log("LeftTrackPadRightActivated"); });
            //LeftTrackPadRightDeactivated.AddListener(() => { Debug.Log("LeftTrackPadRightDeactivated"); });
            //RightTrackPadRightActivated.AddListener(() => { Debug.Log("RightTrackPadRightActivated"); });
            //RightTrackPadRightDeactivated.AddListener(() => { Debug.Log("RightTrackPadRightDeactivated"); });
            //LeftTrackPadDownActivated.AddListener(() => { Debug.Log("LeftTrackPadDownActivated"); });
            //LeftTrackPadDownDeactivated.AddListener(() => { Debug.Log("LeftTrackPadDownDeactivated"); });
            //RightTrackPadDownActivated.AddListener(() => { Debug.Log("RightTrackPadDownActivated"); });
            //RightTrackPadDownDeactivated.AddListener(() => { Debug.Log("RightTrackPadDownDeactivated"); });

        }

        private void Start()
        {
            _globalInputs = HVRGlobalInputs.Instance;
            _hasInputs = _globalInputs;
        }


        private void Update()
        {
            if (!_hasInputs)
                return;

            if (_globalInputs.LeftGripButtonState.JustActivated)
            {
                LeftGripActivated.Invoke();
            }
            else if (_globalInputs.LeftGripButtonState.JustDeactivated)
            {
                LeftGripDeactivated.Invoke();
            }
            if (_globalInputs.RightGripButtonState.JustActivated)
            {
                RightGripActivated.Invoke();
            }
            else if (_globalInputs.RightGripButtonState.JustDeactivated)
            {
                RightGripDeactivated.Invoke();
            }
            if (_globalInputs.LeftTriggerButtonState.JustActivated)
            {
                LeftTriggerActivated.Invoke();
            }
            else if (_globalInputs.LeftTriggerButtonState.JustDeactivated)
            {
                LeftTriggerDeactivated.Invoke();
            }
            if (_globalInputs.RightTriggerButtonState.JustActivated)
            {
                RightTriggerActivated.Invoke();
            }
            else if (_globalInputs.RightTriggerButtonState.JustDeactivated)
            {
                RightTriggerDeactivated.Invoke();
            }
            if (_globalInputs.LeftPrimaryButtonState.JustActivated)
            {
                LeftPrimaryActivated.Invoke();
            }
            else if (_globalInputs.LeftPrimaryButtonState.JustDeactivated)
            {
                LeftPrimaryDeactivated.Invoke();
            }
            if (_globalInputs.RightPrimaryButtonState.JustActivated)
            {
                RightPrimaryActivated.Invoke();
            }
            else if (_globalInputs.RightPrimaryButtonState.JustDeactivated)
            {
                RightPrimaryDeactivated.Invoke();
            }
            if (_globalInputs.LeftSecondaryButtonState.JustActivated)
            {
                LeftSecondaryActivated.Invoke();
            }
            else if (_globalInputs.LeftSecondaryButtonState.JustDeactivated)
            {
                LeftSecondaryDeactivated.Invoke();
            }
            if (_globalInputs.RightSecondaryButtonState.JustActivated)
            {
                RightSecondaryActivated.Invoke();
            }
            else if (_globalInputs.RightSecondaryButtonState.JustDeactivated)
            {
                RightSecondaryDeactivated.Invoke();
            }
            if (_globalInputs.LeftMenuButtonState.JustActivated)
            {
                LeftMenuActivated.Invoke();
            }
            else if (_globalInputs.LeftMenuButtonState.JustDeactivated)
            {
                LeftMenuDeactivated.Invoke();
            }
            if (_globalInputs.RightMenuButtonState.JustActivated)
            {
                RightMenuActivated.Invoke();
            }
            else if (_globalInputs.RightMenuButtonState.JustDeactivated)
            {
                RightMenuDeactivated.Invoke();
            }
            if (_globalInputs.LeftPrimaryTouchButtonState.JustActivated)
            {
                LeftPrimaryTouchActivated.Invoke();
            }
            else if (_globalInputs.LeftPrimaryTouchButtonState.JustDeactivated)
            {
                LeftPrimaryTouchDeactivated.Invoke();
            }
            if (_globalInputs.RightPrimaryTouchButtonState.JustActivated)
            {
                RightPrimaryTouchActivated.Invoke();
            }
            else if (_globalInputs.RightPrimaryTouchButtonState.JustDeactivated)
            {
                RightPrimaryTouchDeactivated.Invoke();
            }
            if (_globalInputs.LeftSecondaryTouchButtonState.JustActivated)
            {
                LeftSecondaryTouchActivated.Invoke();
            }
            else if (_globalInputs.LeftSecondaryTouchButtonState.JustDeactivated)
            {
                LeftSecondaryTouchDeactivated.Invoke();
            }
            if (_globalInputs.RightSecondaryTouchButtonState.JustActivated)
            {
                RightSecondaryTouchActivated.Invoke();
            }
            else if (_globalInputs.RightSecondaryTouchButtonState.JustDeactivated)
            {
                RightSecondaryTouchDeactivated.Invoke();
            }
            if (_globalInputs.LeftJoystickButtonState.JustActivated)
            {
                LeftJoystickActivated.Invoke();
            }
            else if (_globalInputs.LeftJoystickButtonState.JustDeactivated)
            {
                LeftJoystickDeactivated.Invoke();
            }
            if (_globalInputs.RightJoystickButtonState.JustActivated)
            {
                RightJoystickActivated.Invoke();
            }
            else if (_globalInputs.RightJoystickButtonState.JustDeactivated)
            {
                RightJoystickDeactivated.Invoke();
            }
            if (_globalInputs.LeftTrackpadButtonState.JustActivated)
            {
                LeftTrackpadActivated.Invoke();
            }
            else if (_globalInputs.LeftTrackpadButtonState.JustDeactivated)
            {
                LeftTrackpadDeactivated.Invoke();
            }
            if (_globalInputs.RightTrackpadButtonState.JustActivated)
            {
                RightTrackpadActivated.Invoke();
            }
            else if (_globalInputs.RightTrackpadButtonState.JustDeactivated)
            {
                RightTrackpadDeactivated.Invoke();
            }
            if (_globalInputs.LeftJoystickTouchState.JustActivated)
            {
                LeftJoystickTouchActivated.Invoke();
            }
            else if (_globalInputs.LeftJoystickTouchState.JustDeactivated)
            {
                LeftJoystickTouchDeactivated.Invoke();
            }
            if (_globalInputs.RightJoystickTouchState.JustActivated)
            {
                RightJoystickTouchActivated.Invoke();
            }
            else if (_globalInputs.RightJoystickTouchState.JustDeactivated)
            {
                RightJoystickTouchDeactivated.Invoke();
            }
            if (_globalInputs.LeftTrackPadTouchState.JustActivated)
            {
                LeftTrackPadTouchActivated.Invoke();
            }
            else if (_globalInputs.LeftTrackPadTouchState.JustDeactivated)
            {
                LeftTrackPadTouchDeactivated.Invoke();
            }
            if (_globalInputs.RightTrackPadTouchState.JustActivated)
            {
                RightTrackPadTouchActivated.Invoke();
            }
            else if (_globalInputs.RightTrackPadTouchState.JustDeactivated)
            {
                RightTrackPadTouchDeactivated.Invoke();
            }
            if (_globalInputs.LeftTriggerTouchState.JustActivated)
            {
                LeftTriggerTouchActivated.Invoke();
            }
            else if (_globalInputs.LeftTriggerTouchState.JustDeactivated)
            {
                LeftTriggerTouchDeactivated.Invoke();
            }
            if (_globalInputs.RightTriggerTouchState.JustActivated)
            {
                RightTriggerTouchActivated.Invoke();
            }
            else if (_globalInputs.RightTriggerTouchState.JustDeactivated)
            {
                RightTriggerTouchDeactivated.Invoke();
            }
            if (_globalInputs.LeftThumbTouchState.JustActivated)
            {
                LeftThumbTouchActivated.Invoke();
            }
            else if (_globalInputs.LeftThumbTouchState.JustDeactivated)
            {
                LeftThumbTouchDeactivated.Invoke();
            }
            if (_globalInputs.RightThumbTouchState.JustActivated)
            {
                RightThumbTouchActivated.Invoke();
            }
            else if (_globalInputs.RightThumbTouchState.JustDeactivated)
            {
                RightThumbTouchDeactivated.Invoke();
            }
            if (_globalInputs.LeftTrackPadUp.JustActivated)
            {
                LeftTrackPadUpActivated.Invoke();
            }
            else if (_globalInputs.LeftTrackPadUp.JustDeactivated)
            {
                LeftTrackPadUpDeactivated.Invoke();
            }
            if (_globalInputs.RightTrackPadUp.JustActivated)
            {
                RightTrackPadUpActivated.Invoke();
            }
            else if (_globalInputs.RightTrackPadUp.JustDeactivated)
            {
                RightTrackPadUpDeactivated.Invoke();
            }
            if (_globalInputs.LeftTrackPadLeft.JustActivated)
            {
                LeftTrackPadLeftActivated.Invoke();
            }
            else if (_globalInputs.LeftTrackPadLeft.JustDeactivated)
            {
                LeftTrackPadLeftDeactivated.Invoke();
            }
            if (_globalInputs.RightTrackPadLeft.JustActivated)
            {
                RightTrackPadLeftActivated.Invoke();
            }
            else if (_globalInputs.RightTrackPadLeft.JustDeactivated)
            {
                RightTrackPadLeftDeactivated.Invoke();
            }
            if (_globalInputs.LeftTrackPadRight.JustActivated)
            {
                LeftTrackPadRightActivated.Invoke();
            }
            else if (_globalInputs.LeftTrackPadRight.JustDeactivated)
            {
                LeftTrackPadRightDeactivated.Invoke();
            }
            if (_globalInputs.RightTrackPadRight.JustActivated)
            {
                RightTrackPadRightActivated.Invoke();
            }
            else if (_globalInputs.RightTrackPadRight.JustDeactivated)
            {
                RightTrackPadRightDeactivated.Invoke();
            }
            if (_globalInputs.LeftTrackPadDown.JustActivated)
            {
                LeftTrackPadDownActivated.Invoke();
            }
            else if (_globalInputs.LeftTrackPadDown.JustDeactivated)
            {
                LeftTrackPadDownDeactivated.Invoke();
            }
            if (_globalInputs.RightTrackPadDown.JustActivated)
            {
                RightTrackPadDownActivated.Invoke();
            }
            else if (_globalInputs.RightTrackPadDown.JustDeactivated)
            {
                RightTrackPadDownDeactivated.Invoke();
            }

        }
    }
}