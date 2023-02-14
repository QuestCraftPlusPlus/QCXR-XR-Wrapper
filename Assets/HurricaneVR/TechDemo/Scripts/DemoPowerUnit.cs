using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoPowerUnit : MonoBehaviour
    {
        public UnityEvent PoweredUp = new UnityEvent();

        public HVRSocket LeftSocket;
        public HVRSocket RightSocket;

        public MeshRenderer LeftLight;
        public MeshRenderer RightLight;

        public HVRRotationTracker LeverRotation;
        public float RequiredAngle = 175f;

        public AudioClip SFXPoweredOn;
    
        public int RequiredCells = 2;
        public int SocketedCellCount;
        public bool Engaged;

        public Material OnMaterial;

        public bool IsPoweredUp => SocketedCellCount == RequiredCells;

        void Start()
        {
            LeftSocket.Grabbed.AddListener(OnLeftSocketGrabbed);
            RightSocket.Grabbed.AddListener(OnRightSocketGrabbed);
        }

        private void OnRightSocketGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            SocketedCellCount++;
            var mats = RightLight.materials;
            mats[0] = OnMaterial;
            RightLight.materials = mats;
        }

        private void OnLeftSocketGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            SocketedCellCount++;
            var mats = LeftLight.materials;
            mats[0] = OnMaterial;
            LeftLight.materials = mats;
        }

        void Update()
        {
            if (LeverRotation.Angle > RequiredAngle && IsPoweredUp && !Engaged)
            {
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXPoweredOn, LeftSocket.transform.position);
                Engaged = true;
                PoweredUp.Invoke();
                Debug.Log($"poweredup!");
            }
        }
    }
}
