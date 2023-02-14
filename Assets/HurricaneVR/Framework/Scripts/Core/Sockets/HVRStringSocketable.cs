using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVRStringSocketable : HVRSocketable
    {
        [Tooltip("Lower cased and trimmed prior to hashing.")]
        public string SocketType;
        
        public int? Hash { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            ResetHash();
        }

        public void ResetHash()
        {
            if (!string.IsNullOrWhiteSpace(SocketType))
            {
                SocketType = SocketType.Trim();
                Hash = Animator.StringToHash(SocketType);
            }
        }
    }
}