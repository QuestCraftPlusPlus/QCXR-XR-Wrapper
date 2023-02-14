using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.TechDemo.Scripts
{
    [RequireComponent(typeof(DemoPassthroughSocket))]
    public class DemoLock : MonoBehaviour
    {
        public DemoPassthroughSocket Socket;
        public HVRGrabbable FaceGrabbable;
        public GameObject Face;
        public Transform Key;
        public float AnimationTime = 1f;
        public AudioClip SFXUnlocked;
        public AudioClip SFXKeyInserted;
        public float LockThreshold = 89f;

        public UnityEvent Unlocked = new UnityEvent();

        private bool _unlocked;

        public void Start()
        {
            Socket = GetComponent<DemoPassthroughSocket>();
            Socket.Grabbed.AddListener(OnKeyGrabbed);
        }

        public void Update()
        {
            if (!_unlocked && FaceGrabbable.transform.localRotation.eulerAngles.x > LockThreshold)
            {
                _unlocked = true;
                Unlocked.Invoke();
                Debug.Log($"lock unlocked!");
                FaceGrabbable.ForceRelease();
                FaceGrabbable.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                FaceGrabbable.CanBeGrabbed = false;
                FaceGrabbable.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXUnlocked, FaceGrabbable.transform.position);
            }
        }
    
        private void OnKeyGrabbed(HVRGrabberBase grabber, HVRGrabbable key)
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
                key.transform.position = Vector3.Lerp(start, Key.position, elapsed / AnimationTime);
                key.transform.rotation = Quaternion.Lerp(startRot, Key.rotation, elapsed / AnimationTime);

                elapsed += Time.deltaTime;
                yield return null;
            }

            if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXKeyInserted, FaceGrabbable.transform.position);
            FaceGrabbable.gameObject.SetActive(true);
            Face.SetActive(false);
            Destroy(key.gameObject);
        }

    }
}