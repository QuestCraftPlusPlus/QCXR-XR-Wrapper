using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVRStringSocketFilter : HVRSocketFilter
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
                Hash = Animator.StringToHash(SocketType.ToLowerInvariant());
            }
        }

        public override bool IsValid(HVRSocketable socketable)
        {
            if (!Hash.HasValue || !socketable)
                return false;
            var stringSocketable = socketable as HVRStringSocketable;
            if (!stringSocketable || !stringSocketable.Hash.HasValue)
                return false;
            return Hash.Value == stringSocketable.Hash.Value;
        }
    }
}