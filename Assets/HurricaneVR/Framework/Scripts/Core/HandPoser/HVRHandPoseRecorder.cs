//using HurricaneVR.Scripts.Core;
////using HurricaneVR.Scripts.Core.Grabbers;
//using HurricaneVR.Scripts.Core.Utils;
//using UnityEngine;

//namespace HurricaneVR.Scripts.HandPoser
//{
//    public class HVRHandPoseRecorder : MonoBehaviour
//    {
//        public HVRPosableHand Hand;
//        //public HVRHandGrabber Grabber;
//        public HVRHandAnimator Animator;
//        //public HVRHandPhysics HandPhysics;

//        public bool UsePhysicsPoser;
//        public bool RecordPoses;
//        public bool DisablePhysics;

//        private bool _previousRecordPoses;

//        void Start()
//        {
//            if (!Hand) Hand = GetComponentInChildren<HVRPosableHand>();
//            //if (!Grabber) Grabber = GetComponentInChildren<HVRHandGrabber>();
//            if (!Animator) Animator = GetComponentInChildren<HVRHandAnimator>();
//            //if (!HandPhysics) HandPhysics = GetComponentInChildren<HVRHandPhysics>();
//        }

//        void Update()
//        {
//            if (Animator) Animator.UsePhysicsPoser = UsePhysicsPoser;
        
//            if (_previousRecordPoses != RecordPoses)
//            {
//                if (RecordPoses && DisablePhysics)
//                {
//                    //HandPhysics.DisableCollision();
//                }
//                else if (!RecordPoses)
//                {
//                    //HandPhysics.EnableCollision();
//                }
//                _previousRecordPoses = !RecordPoses;
//            }

//            //if (RecordPoses && Hand && Grabber && VRInputActions.GetButtonState(Hand.Side, VRButtons.Primary).JustActivated)
//            //{
//            //    var closest = Grabber.GrabBag.ClosestGrabbable;
//            //    if (closest && closest.VRGrabPoints)
//            //    {
//            //        closest.VRGrabPoints.Add(Hand);
//            //    }
//            //}
//        }
//    }
//}
