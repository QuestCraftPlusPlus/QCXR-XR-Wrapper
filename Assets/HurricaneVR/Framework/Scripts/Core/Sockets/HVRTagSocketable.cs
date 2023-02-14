using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVRTagSocketable : HVRSocketable
    {
        [Header("Socketable Tags")]
        public HVRSocketableTag Tags;


        public void Reset()
        {
            if (Tags == null)
            {
                Tags = new HVRSocketableTag();

#if UNITY_EDITOR

                UnityEditor.EditorUtility.SetDirty(this);

#endif
                Tags.Tags = HVRSettings.Instance.DefaultSocketableTags;
            }
        }
    }
}