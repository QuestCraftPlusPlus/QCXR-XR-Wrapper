using UnityEngine;

namespace HurricaneVR.Framework.Core.Utils
{
    public static class HVRJointUtilities
    {
        public static void LockXMotion(this ConfigurableJoint joint)
        {
            joint.xMotion = ConfigurableJointMotion.Locked;
        }

        public static void LockYMotion(this ConfigurableJoint joint)
        {
            joint.yMotion = ConfigurableJointMotion.Locked;
        }

        public static void LockZMotion(this ConfigurableJoint joint)
        {
            joint.zMotion = ConfigurableJointMotion.Locked;
        }



        public static void SetLinearLimit(this ConfigurableJoint joint, float limit, float bounciness = 0f, float contactDistance = .01f)
        {
            var ll = joint.linearLimit;
            ll.limit = limit;
            ll.bounciness = bounciness;
            ll.contactDistance = contactDistance;
            joint.linearLimit = ll;
        }

        public static void SetAngularXLowLimit(this ConfigurableJoint joint, float limit, float bounciness = 0f, float contactDistance = 0f)
        {
            var ll = joint.lowAngularXLimit;
            ll.limit = limit;
            ll.bounciness = bounciness;
            ll.contactDistance = contactDistance;
            joint.lowAngularXLimit = ll;
        }

        public static void SetAngularXHighLimit(this ConfigurableJoint joint, float limit, float bounciness = 0f, float contactDistance = 0f)
        {
            var ll = joint.highAngularXLimit;
            ll.limit = limit;
            ll.bounciness = bounciness;
            ll.contactDistance = contactDistance;
            joint.highAngularXLimit = ll;
        }

        public static void SetAngularYLimit(this ConfigurableJoint joint, float limit, float bounciness = 0f, float contactDistance = 0f)
        {
            joint.angularYLimit = new SoftJointLimit() { bounciness = bounciness, contactDistance = contactDistance, limit = limit };
        }

        public static void SetAngularZLimit(this ConfigurableJoint joint, float limit, float bounciness = 0f, float contactDistance = 0f)
        {
            joint.angularZLimit = new SoftJointLimit() { bounciness = bounciness, contactDistance = contactDistance, limit = limit };
        }

        public static void SetAngularLimits(this ConfigurableJoint joint, float xLow, float xHigh, float y, float z)
        {
            joint.SetAngularXLowLimit(xLow);
            joint.SetAngularXHighLimit(xHigh);
            joint.SetAngularYLimit(y);
            joint.SetAngularZLimit(z);
        }

        public static void LimitXMotion(this ConfigurableJoint joint)
        {
            joint.xMotion = ConfigurableJointMotion.Limited;
        }

        public static void LimitYMotion(this ConfigurableJoint joint)
        {
            joint.yMotion = ConfigurableJointMotion.Limited;
        }

        public static void LimitZMotion(this ConfigurableJoint joint)
        {
            joint.zMotion = ConfigurableJointMotion.Limited;
        }

        public static void FreeLinearMotion(this ConfigurableJoint joint)
        {
            joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Free;
        }

        public static void LimitLinearMotion(this ConfigurableJoint joint)
        {
            joint.LimitXMotion();
            joint.LimitYMotion();
            joint.LimitZMotion();
        }

        public static void LockLinearMotion(this ConfigurableJoint joint)
        {
            joint.LockXMotion();
            joint.LockYMotion();
            joint.LockZMotion();
        }

        public static void LimitAngularMotion(this ConfigurableJoint joint)
        {
            joint.LimitAngularXMotion();
            joint.LimitAngularYMotion();
            joint.LimitAngularZMotion();
        }

        public static void LimitAngularXMotion(this ConfigurableJoint joint)
        {
            joint.angularXMotion = ConfigurableJointMotion.Limited;
        }

        public static void LimitAngularYMotion(this ConfigurableJoint joint)
        {
            joint.angularYMotion = ConfigurableJointMotion.Limited;
        }

        public static void LimitAngularZMotion(this ConfigurableJoint joint)
        {
            joint.angularZMotion = ConfigurableJointMotion.Limited;
        }

        public static void LockAngularXMotion(this ConfigurableJoint joint)
        {
            joint.angularXMotion = ConfigurableJointMotion.Locked;
        }

        public static void LockAngularYMotion(this ConfigurableJoint joint)
        {
            joint.angularYMotion = ConfigurableJointMotion.Locked;
        }

        public static void LockAngularZMotion(this ConfigurableJoint joint)
        {
            joint.angularZMotion = ConfigurableJointMotion.Locked;
        }

        public static void LockAllAngularMotion(this ConfigurableJoint joint)
        {
            joint.LockAngularXMotion();
            joint.LockAngularYMotion();
            joint.LockAngularZMotion();
        }


        public static void SetXDrive(this ConfigurableJoint joint, float spring, float damper, float maxforce)
        {
            var drive = joint.xDrive;
            drive.positionSpring = spring;
            drive.positionDamper = damper;
            drive.maximumForce = maxforce;
            joint.xDrive = drive;
        }

        public static void SetYDrive(this ConfigurableJoint joint, float spring, float damper, float maxforce)
        {
            var drive = joint.yDrive;
            drive.positionSpring = spring;
            drive.positionDamper = damper;
            drive.maximumForce = maxforce;
            joint.yDrive = drive;
        }

        public static void SetZDrive(this ConfigurableJoint joint, float spring, float damper, float maxforce)
        {
            var drive = joint.zDrive;
            drive.positionSpring = spring;
            drive.positionDamper = damper;
            drive.maximumForce = maxforce;
            joint.zDrive = drive;
        }

        public static void SetSlerpDrive(this ConfigurableJoint joint, float spring, float damper, float maxforce)
        {
            var drive = joint.slerpDrive;
            drive.positionSpring = spring;
            drive.positionDamper = damper;
            drive.maximumForce = maxforce;
            joint.slerpDrive = drive;
        }

        public static void SetAngularXDrive(this ConfigurableJoint joint, float spring, float damper, float maxforce)
        {
            var drive = joint.angularXDrive;
            drive.positionSpring = spring;
            drive.positionDamper = damper;
            drive.maximumForce = maxforce;
            joint.angularXDrive = drive;
        }

        public static void SetLinearDrive(this ConfigurableJoint joint, float spring, float damper, float maxforce)
        {
            joint.SetXDrive(spring, damper, maxforce);
            joint.SetYDrive(spring, damper, maxforce);
            joint.SetZDrive(spring, damper, maxforce);
        }
    }
}