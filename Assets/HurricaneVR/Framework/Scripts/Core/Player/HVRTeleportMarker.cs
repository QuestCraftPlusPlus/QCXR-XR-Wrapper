using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRTeleportMarker : HVRTeleportMarkerBase
    {
        public GameObject Arrow;
        public GameObject Ring;

        public bool UseTeleporterColors = true;
        public Color ValidColor;
        public Color InvalidColor;

        protected Material RingMaterial;
        protected Material ArrowMaterial;


        public Color Color
        {
            get
            {
                if (UseTeleporterColors)
                {
                    return IsTeleportValid ? Teleporter.ValidColor : Teleporter.InvalidColor;
                }

                return IsTeleportValid ? ValidColor : InvalidColor;
            }
        }

        public override void Awake()
        {
            base.Awake();

            if (Ring && Ring.TryGetComponent(out MeshRenderer ringRenderer)) RingMaterial = ringRenderer.material;
            if (Arrow && Arrow.TryGetComponent(out MeshRenderer arrowRenderer)) ArrowMaterial = arrowRenderer.material;
        }


        protected override void OnActivated()
        {
            if (Arrow) Arrow.SetActive(true);
            if (Ring) Ring.SetActive(true);
        }

        protected override void OnDeactivated()
        {
            if (Arrow) Arrow.SetActive(false);
            if (Ring) Ring.SetActive(false);
        }

        public override void OnValidTeleportChanged(bool isTeleportValid)
        {
            base.OnValidTeleportChanged(isTeleportValid);

            UpdateMaterials();
        }

        protected virtual void UpdateMaterials()
        {
            if (RingMaterial) RingMaterial.color = Color;
            if (ArrowMaterial) ArrowMaterial.color = Color;
        }
    }
}