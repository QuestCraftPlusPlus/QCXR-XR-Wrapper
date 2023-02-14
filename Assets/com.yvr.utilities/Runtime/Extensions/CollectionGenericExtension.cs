using System;
using System.Linq;
using System.Collections.Generic;

public static class CollectionGenericExtension
{
    /// <summary>
    /// Safe version of Dictionary Add function, which will not trigger error while target key already exists
    /// </summary>
    /// <param name="dictionary"> Dictionary instance </param>
    /// <param name="key"> Target key </param>
    /// <param name="value"> Target value </param>
    /// <typeparam name="TKey"> Key type </typeparam>
    /// <typeparam name="TValue"> Value type </typeparam>
    /// <returns></returns>
    public static bool SafeAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key)) return true;

        dictionary.Add(key, value);
        return false;
    }

    /// <summary>
    /// Safe version of Dictionary [] operator, which will automatically add instance if target elements does not exist
    /// </summary>
    /// <param name="dictionary"> Dictionary instance </param>
    /// <param name="key"> Target key</param>
    /// <typeparam name="TKey"> Key type </typeparam>
    /// <typeparam name="TValue"> Value type </typeparam>
    /// <returns></returns>
    public static TValue SafeGet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
    {
        if (!dictionary.ContainsKey(key))
            dictionary.Add(key, new TValue());
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
    /// Select the elements that meet the condition and add to the list
    /// </summary>
    /// <param name="targetList"> Target list </param>
    /// <param name="dataList"> Reference data </param>
    /// <param name="condition"> Condition </param>
    /// <typeparam name="T"> Value type </typeparam>
    public static void AddElementsFrom<T>(this List<T> targetList, List<T> dataList, Func<T, T, bool> condition)
    {
        dataList.ForEach(item =>
        {
            if (targetList.All(p => condition.Invoke(p, item)))
                targetList.Add(item);
        });
    }

    /// <summary>
    /// Remove all the targetList element that matches the datalist element
    /// </summary>
    /// <param name="targetList"> Target list </param>
    /// <param name="dataList"> Reference data list </param>
    /// <param name="condition"> Condition </param>
    /// <typeparam name="T"> Value type </typeparam>
    public static void RemoveAll<T>(this List<T> targetList, List<T> dataList, Func<T, T, bool> condition)
    {
        dataList.ForEach(item => targetList.RemoveAll(p => condition.Invoke(p, item)));
    }
}