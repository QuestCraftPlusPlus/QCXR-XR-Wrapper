using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    [CreateAssetMenu(menuName = "HurricaneVR/Socketables", fileName = "Socketables")]
    public class HVRSocketableTags : ScriptableObject
    {
        [SerializeField] private string[] _socketables = new string[32];

        public string this[int index]
        {
            get { return _socketables[index]; }
            set { _socketables[index] = value; }
        }

        public int GetIndex(string tag)
        {
            return Array.IndexOf(_socketables, tag);
        }

        public int GetCount()
        {
            var count = 0;

            foreach (var e in _socketables)
            {
                if (!string.IsNullOrWhiteSpace(e)) count++;
            }

            return count;
        }

        public IEnumerable<string> GetNames()
        {
            foreach (var s in _socketables)
            {
                yield return s;
            }
        }
    }

    [Serializable]
    public class HVRSocketableTag
    {
        [SerializeField]
        public HVRSocketableTags Tags;

        [SerializeField]
        private int _data;

        public bool this[int index]
        {
            get
            {
                return (_data & 1 << index) != 0;
            }
            set
            {
                if (index < 0 || index > 32)
                    return;

                if (value)
                    _data |= 1 << index;
                else
                    _data &= ~(1 << index);
            }
        }

        public string DisplayName
        {
            get
            {
                if (_data == 0)
                    return "None";

                if (Tags)
                {
                    var count = 0;
                    var last = "";
                    for (var i = 0; i < 32; i++)
                    {
                        if (this[i])
                        {
                            count++;
                            last = Tags[i];
                        }
                    }

                    if (count == 1)
                        return last;

                    return "Mixed...";
                }

                return "Select Tags";
            }
        }
    }
}