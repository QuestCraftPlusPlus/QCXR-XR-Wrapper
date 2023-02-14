using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVRTagSocketFilter : HVRSocketFilter
    {
        public HVRSocketableTag Tags;

        [Tooltip("If true, the socketable is valid as long as a there is a single match, otherwise all selected tags must be assigned on the socketable.")]
        public bool Any = true;


        public void Reset()
        {
            if (Tags == null)
            {
                Tags = new HVRSocketableTag();

#if UNITY_EDITOR

                UnityEditor.EditorUtility.SetDirty(this);

#endif
            }

            Tags.Tags = HVRSettings.Instance.DefaultSocketableTags;
        }

        public override bool IsValid(HVRSocketable socketable)
        {
            var tagSocketable = socketable as HVRTagSocketable;
            
            if (tagSocketable == null || tagSocketable.Tags == null)
                return false;

            var socketableTags = tagSocketable.Tags;

            if (socketableTags.Tags != Tags.Tags)
                return false;
            
            var anyHit = false;
            
            for (var i = 0; i < 32; i++)
            {
                if (Any)
                {
                    if (Tags[i] && socketableTags[i])
                    {
                        anyHit = true;
                        break;
                    }
                }
                else
                {
                    if (Tags[i] && !socketableTags[i])
                        return false;
                }
            }

            return !Any || anyHit;
        }
    }
}