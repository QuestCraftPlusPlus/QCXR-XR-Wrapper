using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Used to define center of mass of the controller relative to the hand for bonus throwing velocity calculations
    /// </summary>
    public class HVRThrowingCenterOfMass : MonoBehaviour
    {
        public HVRHandSide HandSide;
        public Transform Oculus;
        public Transform Vive;
        public Transform WMR;
        public Transform Knuckles;
        public Transform Fallback;

        public Transform CenterOfMass;

        private void Start()
        {
            if (HandSide == HVRHandSide.Left)
            {
                if (HVRInputManager.Instance.LeftController)
                    ControllerConnected(HVRInputManager.Instance.LeftController);
                HVRInputManager.Instance.LeftControllerConnected.AddListener(ControllerConnected);
            }
            else
            {
                if (HVRInputManager.Instance.RightController)
                    ControllerConnected(HVRInputManager.Instance.RightController);
                HVRInputManager.Instance.RightControllerConnected.AddListener(ControllerConnected);
            }
        }

        private void ControllerConnected(HVRController controller)
        {

            switch (controller.ControllerType)
            {
                case HVRControllerType.Oculus:
                    CenterOfMass = Oculus;
                    break;
                case HVRControllerType.ReverbG2:
                case HVRControllerType.WMR:
                    CenterOfMass = WMR;
                    break;
                case HVRControllerType.Vive:
                    CenterOfMass = Vive;
                    break;
                case HVRControllerType.Knuckles:
                    CenterOfMass = Knuckles;
                    break;
                default:
                    CenterOfMass = Fallback;
                    break;
            }

            if (!CenterOfMass)
                CenterOfMass = Fallback;
            if (!CenterOfMass)
                CenterOfMass = transform;
        }
    }
}
