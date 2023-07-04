using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace YVR.Utilities
{
    public static partial class ObjectExtensions
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
        public static bool TryGetFieldValue<T>(this object target, string name, out T value,
                                               BindingFlags bindingFlags = BindingFlags.Instance |
                                                                           BindingFlags.Public |
                                                                           BindingFlags.NonPublic)
        {
            FieldInfo field = GetTargetFieldInfo(target, name, bindingFlags);

            bool fieldFound = field != null;
            value = fieldFound ? (T) field.GetValue(target) : default(T);
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
        public static bool TrySetFieldValue<T>(this object target, string name, T value,
                                               BindingFlags bindingFlags = BindingFlags.Instance |
                                                                           BindingFlags.Public |
                                                                           BindingFlags.NonPublic)
        {
            FieldInfo field = GetTargetFieldInfo(target, name, bindingFlags);
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
        public static bool TryGetPropertyValue<T>(this object target, string name, out T value,
                                                  BindingFlags bindingFlags = BindingFlags.Instance |
                                                                              BindingFlags.Public |
                                                                              BindingFlags.NonPublic)
        {
            PropertyInfo property = GetTargetPropertyInfo(target, name, bindingFlags);
            bool propertyFound = property != null;
            value = propertyFound ? (T) property.GetValue(target) : default(T);
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
        public static bool TrySetPropertyValue<T>(this object target, string name, T value,
                                                  BindingFlags bindingFlags = BindingFlags.Instance |
                                                                              BindingFlags.Public |
                                                                              BindingFlags.NonPublic)
        {
            PropertyInfo property = GetTargetPropertyInfo(target, name, bindingFlags);
            bool propertyFound = property != null;
            if (propertyFound) property.SetValue(target, value);
            return propertyFound;
        }

        /// <summary>
        /// Using reflection to determinate whether object is equal to <param name="target"></param> or not.
        /// </summary>
        /// <param name="self"> Object to be invoked ReflectEquals on. </param>
        /// <param name="target"> The object to compare with the current object. </param>
        /// <returns> True if equals </returns>
        public static bool ReflectEquals(this object self, object target)
        {
            if (ReferenceEquals(self, target)) return true;
            Type selfType = self?.GetType();
            Type targetType = target?.GetType();
            if (selfType != targetType) return false;
            if (selfType.IsPrimitive || self is string || selfType.IsValueType) return self.Equals(target);
            if (IsGenericEnumerable(target)) return ReflectionSequenceEqual(self, target);


            List<PropertyInfo> selfProperties = self.GetType().GetProperties().ToList();
            List<PropertyInfo> targetProperties = self.GetType().GetProperties().ToList();

            List<FieldInfo> selfFields = self.GetType().GetFields().ToList();
            List<FieldInfo> targetFields = target.GetType().GetFields().ToList();

            bool equal = selfProperties.SequenceEqual(targetProperties) && selfFields.SequenceEqual(targetFields) &&
                         !selfProperties.Any(info =>
                         {
                             object selfVal = info.GetValue(self, null);
                             object targetVal = info.GetValue(target, null);

                             return !ReflectEquals(selfVal, targetVal);
                         }) && !selfFields.Any(info =>
                         {
                             object selfVal = info.GetValue(self);
                             object targetVal = info.GetValue(target);

                             return !ReflectEquals(selfVal, targetVal);
                         });

            return equal;
        }

        /// <summary>
        /// Determinate whether target object is belongs to generic enumerable, e.g. List, Dictionary, type or not.
        /// </summary>
        /// <param name="target"> Object to be determinate. </param>
        /// <returns> True if object is generic enumerable </returns>
        public static bool IsGenericEnumerable(this object target)
        {
            return target.GetType().GetInterfaces()
                .Count(type => type.ToString().Contains("System.Collections.Generic.IEnumerable`")) != 0;
        }

        /// <summary>
        /// Get element type of an generic enumerable object.
        /// </summary>
        /// <param name="target"> Generic enumerable object </param>
        /// <returns> Type of element </returns>
        public static Type GetEnumerableElementType(this object target)
        {
            Type genericIEnumerableType = target.GetType().GetInterfaces()
                .Single(type => type.ToString().Contains("System.Collections.Generic.IEnumerable`"));
            return genericIEnumerableType.GetGenericArguments().Single();
        }

        /// <summary>
        /// Call SequenceEqual function on two generic enumerable object
        /// </summary>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to <paramref name="second" />.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to the first sequence.</param>
        /// <returns> True if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; </returns>
        public static bool ReflectionSequenceEqual(this object first, object second)
        {
            MethodInfo sequenceEqualMethod = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Single(method =>
                    method.Name == "SequenceEqual" &&
                    method.GetParameters().Length == 2);
            MethodInfo targetGenericMethod = sequenceEqualMethod.MakeGenericMethod(GetEnumerableElementType(first));
            return (bool) targetGenericMethod.Invoke(null, new object[] {first, second});
        }

        private static FieldInfo GetTargetFieldInfo(object target, string name, BindingFlags bindingFlags)
        {
            Type targetType = target.GetType();
            FieldInfo field = null;

            do
            {
                field = targetType.GetField(name, bindingFlags);
                targetType = targetType.BaseType;
            }
            while (field == null && targetType != null);

            return field;
        }

        private static PropertyInfo GetTargetPropertyInfo(object target, string name, BindingFlags bindingFlags)
        {
            Type targetType = target.GetType();
            PropertyInfo property = null;

            do
            {
                property = targetType.GetProperty(name, bindingFlags);
                targetType = targetType.BaseType;
            }
            while (property == null && targetType != null);

            return property;
        }
    }
}