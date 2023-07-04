using System;
using System.Collections.Generic;
using System.Reflection;

namespace YVR.Utilities
{
    // Inspired by https://github.com/Burtsev-Alexey/net-object-deep-copy
    public static partial class ObjectExtensions
    {
        private static readonly MethodInfo s_CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Make a deep copy of <param name="original"></param> object
        /// </summary>
        /// <param name="original"> The object to be copied. </param>
        /// <typeparam name="T"> The type of the <param name="original"></param> </typeparam>
        /// <returns> The copied object. </returns>
        public static T DeepCopy<T>(this T original)
        {
            return (T) InternalCopy((object) original, new Dictionary<object, object>(new ReferenceEqualityComparer()));
        }

        private static object InternalCopy(object originalObject, IDictionary<object, object> visited)
        {
            if (originalObject == null) return null;
            Type originObjectType = originalObject.GetType();
            if (IsPrimitive(originObjectType)) return originalObject;
            if (visited.ContainsKey(originalObject)) return visited[originalObject];

            if (typeof(Delegate).IsAssignableFrom(originObjectType)) return null;

            object cloneObject = s_CloneMethod.Invoke(originalObject, null);

            if (originObjectType.IsArray)
            {
                Type elementType = originObjectType.GetElementType();
                if (!IsPrimitive(elementType))
                {
                    Array clonedArray = (Array) cloneObject;
                    clonedArray.IndexForEach((indices) => clonedArray.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }
            }

            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, originObjectType);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, originObjectType);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type originType)
        {
            if (originType.BaseType == null) return;

            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, originType.BaseType);
            CopyFields(originalObject, visited, cloneObject, originType.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                object copiedValue = InternalCopy(fieldInfo.GetValue(originalObject), visited);
                fieldInfo.SetValue(cloneObject, copiedValue);
            }
        }

        private static bool IsPrimitive(this Type type)
        {
            if (type == typeof(string)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        private class ReferenceEqualityComparer : EqualityComparer<object>
        {
            public override bool Equals(object x, object y) => ReferenceEquals(x, y);

            public override int GetHashCode(object obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}