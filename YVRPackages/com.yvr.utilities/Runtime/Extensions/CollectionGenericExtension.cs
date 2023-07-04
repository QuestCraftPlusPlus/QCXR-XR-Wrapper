using System;
using System.Linq;
using System.Collections.Generic;

public static class CollectionGenericExtension
{
    /// <summary>
    /// Safe version of Dictionary Add function, which will not trigger error while target key already exists
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
    /// <param name="dictionary">The dictionary to add a value to.</param>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value to add.</param>
    /// <param name="override">Whether to override the existing value if the key is already present in the dictionary.</param>
    /// <returns>True if the key was already present in the dictionary, false if the key was added successfully.</returns>
    public static bool SafeAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value,
                                             bool @override = false)
    {
        if (dictionary.ContainsKey(key))
        {
            if (@override)
                dictionary[key] = value;
            return true;
        }

        dictionary.Add(key, value);
        return false;
    }

    /// <summary>
    /// Safe version of Dictionary [] operator, which will optionally add element if target elements does not exist based on <paramref name="autoAdd"/>
    /// Returns the default value of the value type if the key does not exist and the <paramref name="autoAdd"/> parameter is false.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
    /// <param name="dictionary">The dictionary to get a value from.</param>
    /// <param name="key">The key to get the value for.</param>
    /// <param name="autoAdd">Whether to automatically add a default value to the dictionary if the key is not already present. Default is true.</param>
    /// <returns>The value associated with the specified key if the key is found, otherwise the default value of the value type.</returns>
    public static TValue SafeGet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, bool autoAdd = true)
        where TValue : new()
    {
        if (!dictionary.ContainsKey(key))
        {
            if (autoAdd) dictionary.Add(key, new TValue());
            else return default;
        }

        return dictionary[key];
    }

    /// <summary>
    /// Extract the elements that meet the condition and remove the extracted elements from the source list
    /// </summary>
    /// <param name="targetList"> Target list </param>
    /// <param name="condition"> Condition </param>
    /// <typeparam name="T"> Value type </typeparam>
    /// <returns> List of eligible elements </returns>
    public static List<T> Extract<T>(this List<T> targetList, Func<T, bool> condition)
    {
        List<T> resultList = targetList.Where(condition).ToList();
        targetList.RemoveAll(resultList, (p1, p2) => p1.Equals(p2));
        return resultList;
    }

    /// <summary>
    /// Adds elements from the specified data list to the target list based on the specified condition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the lists.</typeparam>
    /// <param name="targetList">The list to add elements to.</param>
    /// <param name="dataList">The list of elements to add.</param>
    /// <param name="condition">The condition that must be satisfied for an element to be added.</param>
    /// <remarks>
    /// This method checks each element in the data list against each element in the target list using the specified condition. If the condition is true for all elements in the target list, the data list element is added to the target list. This method does not modify the data list.
    /// </remarks>
    public static void AddElementsFrom<T>(this List<T> targetList, List<T> dataList, Func<T, T, bool> condition)
    {
        dataList.ForEach(item =>
        {
            if (targetList.All(p => condition.Invoke(p, item)))
                targetList.Add(item);
        });
    }

    /// <summary>
    /// Removes all elements from the target list that satisfy the specified condition, using elements from the data list for comparison.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the lists.</typeparam>
    /// <param name="targetList">The list to remove elements from.</param>
    /// <param name="dataList">The list of elements to compare against.</param>
    /// <param name="condition">The condition that must be satisfied for an element to be removed.</param>
    /// <remarks>
    /// This method removes each element in the target list for which the condition is true when compared against each element in the data list. This method modifies the target list.
    /// </remarks>
    public static void RemoveAll<T>(this List<T> targetList, List<T> dataList, Func<T, T, bool> condition)
    {
        dataList.ForEach(item => targetList.RemoveAll(p => condition.Invoke(p, item)));
    }
}