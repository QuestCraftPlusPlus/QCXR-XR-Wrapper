using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRTeleportOptions : MonoBehaviour
    {
        public BeforeTeleportOptions BeforeTeleportOption = BeforeTeleportOptions.None;
        public AfterTeleportOptions AfterTeleportOption = AfterTeleportOptions.TeleporterDefault;

        public BoxCollider CustomBoundingBox;
    }

    public enum BeforeTeleportOptions
    {
        None,
        PreventsTeleport,
        DropsGrabbable
    }

    public enum AfterTeleportOptions
    {
        TeleporterDefault,
        BoundingBoxSweep,
        DisableCollision
    }
}