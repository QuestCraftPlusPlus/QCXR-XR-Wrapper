using System.Reflection;
using UnityEngine;

namespace YVR.Utilities
{
    public static class UnityObjectExtensions
    {
        /// <summary>
        /// Get object field value by reflection
        /// </summary>
        /// <typeparam name="T"> Value type </typeparam>
        /// <param name="target">Target Object </param>
        /// <param name="name"> Target type </param>
        /// <param name="value"> Output value </param>
        /// <param name="bindingFlags"> BindingFlags for field </param>
        /// <returns> True if target field existed </returns>
        public static bool TryGetFieldValue<T>(this Object target, string name, out T value, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = target.GetType().GetField(name, bindingFlags);
            bool fieldFound = field != null;
            value = fieldFound ? (T)field.GetValue(target) : default(T);
            return fieldFound;
        }

        /// <summary>
        /// Set object field value by reflection
        /// </summary>
        /// <typeparam name="T"> Value type </typeparam>
        /// <param name="target">Target Object </param>
        /// <param name="name"> Target type </param>
        /// <param name="value"> Input value </param>
        /// <param name="bindingFlags"> BindingFlags for field </param>
        /// <returns>True if target field existed </returns>
        public static bool TrySetFieldValue<T>(this Object target, string name, T value, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = target.GetType().GetField(name, bindingFlags);
            bool fieldFound = field != null;
            if (fieldFound) field.SetValue(target, value);
            return fieldFound;
        }

        /// <summary>
        /// Get object property value by reflection
        /// </summary>
        /// <typeparam name="T"> Value type </typeparam>
        /// <param name="target">Target Object </param>
        /// <param name="name"> Target type </param>
        /// <param name="value"> Output value </param>
        /// <param name="bindingFlags"> BindingFlags for property </param>
        /// <returns> True if target property existed </returns>
        public static bool TryGetPropertyValue<T>(this Object target, string name, out T value, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            PropertyInfo property = target.GetType().GetProperty(name, bindingFlags);
            bool propertyFound = property != null;
            value = propertyFound ? (T)property.GetValue(target) : default(T);
            return propertyFound;
        }

        /// <summary>
        /// Set object property value by reflection
        /// </summary>
        /// <typeparam name="T"> Value type </typeparam>
        /// <param name="target">Target Object </param>
        /// <param name="name"> Target type </param>
        /// <param name="value"> Input value </param>
        /// <param name="bindingFlags"> BindingFlags for property </param>
        /// <returns>True if target property existed </returns>
        public static bool TrySetPropertyValue<T>(this Object target, string name, T value, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            PropertyInfo property = target.GetType().GetProperty(name, bindingFlags);
            bool propertyFound = property != null;
            if (propertyFound) property.SetValue(target, value);
            return propertyFound;
        }
    }
}
