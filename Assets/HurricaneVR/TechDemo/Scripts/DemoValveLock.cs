using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    [RequireComponent(typeof(DemoPassthroughSocket))]
    public class DemoValveLock : MonoBehaviour
    {
        public DemoPassthroughSocket Socket;
        public HVRGrabbable FaceGrabbable;
        public float AnimationTime = 1f;

        public void Start()
        {
            Socket = GetComponent<DemoPassthroughSocket>();
            Socket.Grabbed.AddListener(OnValveGrabbed);
        }

        public void Update()
        {
      
        }

        private void OnValveGrabbed(HVRGrabberBase grabber, HVRGrabbable key)
        {
            StartCoroutine(MoveKey(key));
        }

        private IEnumerator MoveKey(HVRGrabbable key)
        {
            var start = key.transform.position;
            var startRot = key.transform.rotation;

            var elapsed = 0f;
            while (elapsed < AnimationTime)
            {
                key.transform.position = Vector3.Lerp(start, FaceGrabbable.transform.position, elapsed / AnimationTime);
                key.transform.rotation = Quaternion.Lerp(startRot, FaceGrabbable.transform.rotation, elapsed / AnimationTime);

                elapsed += Time.deltaTime;
                yield return null;
            }

            FaceGrabbable.gameObject.SetActive(true);
            Destroy(key.gameObject);
        }

    }
}