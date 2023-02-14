using HurricaneVR.Framework.Core.Utils;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRBulletEmitter : HVRGunEmitterBase
    {
        public override void Emit()
        {
            var clone = Instantiate(Prefab);
            var rb = clone.GetRigidbody();
            if (!rb)
                return;
            Launch(rb);
        }
    }
}