namespace HurricaneVR.Framework.Core.Utils
{
    public static class HVRConstants
    {
        public const int GrabbableLayer = 20;
        public const int HandLayer = 21;
        public const int DynamicPose = 22;
        public const int PlayerLayer = 8;

        public const int PlayerHandGrabbableMask = DefaultHandMask | DefaultPlayerMask | DefaultGrabbableMask;
        public const int DefaultPlayerMask = 1 << PlayerLayer;
        public const int DefaultHandMask = 1 << HandLayer;
        public const int DefaultGrabbableMask = 1 << GrabbableLayer;
    }
}