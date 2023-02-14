using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{
    public abstract class HVRTeleportMarkerBase : MonoBehaviour
    {
        public bool IsTeleportValid { get; protected set; }

        protected bool ForceChanged;

        public bool Activated { get; internal set; }

        public bool Deactivated { get; internal set; }
        
        public HVRTeleporter Teleporter { get; internal set; }

        public virtual void Awake()
        {

        }

        public void Activate()
        {
            if (Activated)
                return;
            Activated = true;
            ForceChanged = true;
            OnActivated();
        }

        public void Deactivate()
        {
            if (!Activated)
                return;
            Activated = false;
            OnDeactivated();
        }

        protected abstract void OnActivated();

        protected abstract void OnDeactivated();

      

        public virtual void UpdateState(bool isTeleportValid)
        {
            if (isTeleportValid != IsTeleportValid || ForceChanged)
            {
                ForceChanged = false;
                IsTeleportValid = isTeleportValid;
                OnValidTeleportChanged(IsTeleportValid);
            }
        }

        public virtual void OnValidTeleportChanged(bool isTeleportValid)
        {

        }
    }
}