using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Bow
{
    [RequireComponent(typeof(HVRGrabbable))]
    [RequireComponent(typeof(Rigidbody))]
    public class HVRPhysicsBow : HVRBowBase
    {
        [Header("Physics Bow Settings")]
        public float StringSpring = 10000f;
        public float StringHeldSpring = 100f;

        [Tooltip("If true the nock joint will be freed on the forward axis which will allow the hand to rotate while holding the nock")]
        public bool CanNockRotate;


        public Rigidbody NockRigidbody => NockGrabbable.Rigidbody;

        private ConfigurableJoint _stringJoint;
        private ConfigurableJoint _stringLimitJoint;
        private ConfigurableJoint _nockJoint;
        private ConfigurableJoint _restJoint;
        private Vector3 _nockPosition;

        protected override void Start()
        {
            base.Start();

            SetupStringJoint();
            _nockPosition = NockGrabbable.transform.localPosition;
        }

        protected override void AfterFixedUpdateBow()
        {
            base.AfterFixedUpdateBow();
            UpdateRestAnchor();
        }

        private void UpdateRestAnchor()
        {
            if (_restJoint && Arrow)
            {
                var anchor = Arrow.transform.InverseTransformPoint(Rest.transform.position);
                anchor.Scale(Forward);
                _restJoint.connectedAnchor = anchor;
            }
        }


        protected override void OnStringReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            _stringJoint.SetXDrive(StringSpring, 0f, StringSpring);
        }

        protected override void OnStringGrabbed(HVRHandGrabber hand, HVRGrabbable nock)
        {
            if (Arrow)
            {
                hand.DisableHandCollision(Arrow.Grabbable);
            }

            _stringJoint.SetXDrive(StringHeldSpring, 0, StringHeldSpring);
        }



        protected override void OnArrowNocked(HVRArrow arrow)
        {
            base.OnArrowNocked(arrow);

            SetupNockJoint(arrow);
            SetupRestJoint(arrow);
        }

        protected override void OnBowSocketed(HVRSocket arg0, HVRGrabbable arg1)
        {
            base.OnBowSocketed(arg0, arg1);
            NockRigidbody.SetKinematic();
            NockGrabbable.transform.localPosition = _nockPosition;
        }

        protected override void OnBowUnsocketed(HVRSocket arg0, HVRGrabbable arg1)
        {
            base.OnBowUnsocketed(arg0, arg1);
            NockRigidbody.isKinematic = false;
            NockRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }


        protected override void OnArrowRemoved(HVRArrow arrow)
        {
            base.OnArrowRemoved(arrow);

            if (_nockJoint)
            {
                Destroy(_nockJoint);
            }

            if (_restJoint)
            {
                Destroy(_restJoint);
            }
        }

        protected override void ShootArrow(Vector3 direction)
        {
            var pos = Arrow.transform.position;
            var rot = Arrow.transform.rotation;

            //the joints are preventing the arrow from shooting in the same frame perhaps???
            this.ExecuteAfterFixedUpdate(() =>
            {
                Arrow.transform.SetPositionAndRotation(pos, rot);
                base.ShootArrow(direction);
            });
        }

        private void SetupStringJoint()
        {
            _stringJoint = gameObject.AddComponent<ConfigurableJoint>();

            var axis = Forward;

            _stringJoint.axis = axis;
            _stringJoint.secondaryAxis = axis.OrthogonalVector();

            _stringJoint.LimitXMotion();
            _stringJoint.LockYMotion();
            _stringJoint.LockZMotion();

            if (CanNockRotate)
            {
                _stringJoint.LockAngularYMotion();
                _stringJoint.LockAngularZMotion();
            }
            else
            {
                _stringJoint.LockAllAngularMotion();
            }

            _stringJoint.SetXDrive(StringSpring, 0, StringSpring);
            _stringJoint.anchor = transform.InverseTransformPoint(NockRigidbody.position) - axis * StringLimit;
            _stringJoint.targetPosition = Vector3.right * StringLimit;
            _stringJoint.autoConfigureConnectedAnchor = false;
            _stringJoint.connectedBody = NockRigidbody;
            _stringJoint.connectedAnchor = Vector3.zero;
            _stringJoint.projectionMode = JointProjectionMode.PositionAndRotation;
            _stringJoint.projectionDistance = .001f;

            _stringJoint.linearLimit = new SoftJointLimit()
            {
                limit = StringLimit
            };

            if (StringLimitStyle == HVRBowLimitStyle.Limit)
            {
                _stringLimitJoint = gameObject.AddComponent<ConfigurableJoint>();

                _stringLimitJoint.axis = axis;
                _stringLimitJoint.secondaryAxis = axis.OrthogonalVector();

                _stringLimitJoint.LimitXMotion();
                _stringLimitJoint.LockYMotion();
                _stringLimitJoint.LockZMotion();

                if (CanNockRotate)
                {
                    _stringLimitJoint.LockAngularYMotion();
                    _stringLimitJoint.LockAngularZMotion();
                }
                else
                {
                    _stringLimitJoint.LockAllAngularMotion();
                }

                _stringLimitJoint.anchor = transform.InverseTransformPoint(NockRigidbody.position);
                _stringLimitJoint.autoConfigureConnectedAnchor = false;
                _stringLimitJoint.connectedBody = NockRigidbody;
                _stringLimitJoint.connectedAnchor = Vector3.zero;
                _stringLimitJoint.projectionMode = JointProjectionMode.PositionAndRotation;
                _stringLimitJoint.projectionDistance = .001f;

                _stringLimitJoint.linearLimit = new SoftJointLimit()
                {
                    limit = StringLimit
                };
            }
        }

        private void SetupNockJoint(HVRArrow arrow)
        {
            _nockJoint = NockRigidbody.gameObject.AddComponent<ConfigurableJoint>();

            var axis = Forward;
            _nockJoint.axis = axis;
            _nockJoint.secondaryAxis = axis.OrthogonalVector();
            _nockJoint.LockYMotion();
            _nockJoint.LockZMotion();
            _nockJoint.LockLinearMotion();
            _nockJoint.LockAllAngularMotion();
            _nockJoint.anchor = Vector3.zero;
            _nockJoint.autoConfigureConnectedAnchor = false;
            _nockJoint.connectedBody = arrow.Rigidbody;
            _nockJoint.connectedAnchor = arrow.NotchPointLocal;
        }

        private void SetupRestJoint(HVRArrow arrow)
        {
            _restJoint = gameObject.AddComponent<ConfigurableJoint>();

            var axis = Forward;
            _restJoint.axis = axis;
            _restJoint.secondaryAxis = axis.OrthogonalVector();

            _restJoint.LockYMotion();
            _restJoint.LockZMotion();

            _restJoint.anchor = transform.InverseTransformPoint(Rest.transform.position);
            _restJoint.autoConfigureConnectedAnchor = false;
            _restJoint.connectedBody = arrow.Rigidbody;
            _restJoint.connectedAnchor = arrow.transform.InverseTransformPoint(Rest.transform.position);
        }
    }
}