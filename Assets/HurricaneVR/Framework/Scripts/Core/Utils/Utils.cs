using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Utils
{
    public static class VRUtilities
    {
        public static Transform FindChildRecursive(this Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Contains(name))
                    return child;

                var result = child.FindChildRecursive(name);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static Rigidbody GetRigidbody(this MonoBehaviour b)
        {
            return b.GetComponent<Rigidbody>();
        }

        public static Rigidbody GetRigidbody(this GameObject b)
        {
            return b.GetComponent<Rigidbody>();
        }

        public static T AddCopyOf<T>(this GameObject go, T toCopy) where T : Component
        {
            var type = toCopy.GetType();
            return go.AddComponent(type).GetCopyOf(toCopy);
        }

        public static T GetCopyOf<T>(this Component newComponent, T toCopy) where T : Component
        {
            Type type = newComponent.GetType();

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(newComponent, pinfo.GetValue(toCopy, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }
            FieldInfo[] finfos = type.GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(newComponent, finfo.GetValue(toCopy));
            }
            return newComponent as T;
        }
    }
}
