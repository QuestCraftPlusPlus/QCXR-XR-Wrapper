using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class NotifiableList<T> : List<T> where T:INotifyPropertyChanged
{
    //TODO: reduce the memory allocating when trigger onCollectionChanged

    public event Action<int, string> onElementsPropertyChanged = default;
    public event Action<List<T>, List<T>> onCollectionChanged = default;

    public NotifiableList()
    {
        onCollectionChanged += ModifyPropertyChangedListener;
    }

    ~NotifiableList()
    {
        onCollectionChanged -= ModifyPropertyChangedListener;
    }

    private void ModifyPropertyChangedListener(List<T> newElements, List<T> removedElements)
    {
        if (newElements != null)
            RegisterElementsPropertyChanged(newElements);
        if (removedElements != null)
            UnRegisterElementsPropertyChanged(newElements);
    }

    private void RegisterElementsPropertyChanged(List<T> elements)
    {
        foreach (System.Object element in elements)
        {
            (element as INotifyPropertyChanged).PropertyChanged += OnElementsPropertyChanged;
        }
    }

    private void UnRegisterElementsPropertyChanged(List<T> elements)
    {
        foreach (System.Object element in elements)
        {
            (element as INotifyPropertyChanged).PropertyChanged -= OnElementsPropertyChanged;
        }
    }

    private void OnElementsPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        onElementsPropertyChanged?.Invoke(base.IndexOf((T)sender), args.PropertyName);
    }

    #region 
    public new T this[int index]
    {
        get { return base[index]; }
        set
        {
            if (value == null && base[index] != null)
            {
                onCollectionChanged?.Invoke(null, new List<T>() { base[index] });
            }
            if (base[index] == null && value != null)
                onCollectionChanged?.Invoke(new List<T>() { value }, null);

            base[index] = value;
        }
    }

    public new void Add(T item)
    {
        base.Add(item);
        onCollectionChanged(new List<T>() { item }, null);
    }

    public new void AddRange(IEnumerable<T> collection)
    {
        base.AddRange(collection);
        onCollectionChanged(new List<T>(collection), null);
    }

    public new void Insert(int index, T item)
    {
        base.Insert(index, item);
        onCollectionChanged(new List<T>() { item }, null);
    }

    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        base.InsertRange(index, collection);
        onCollectionChanged(new List<T>(collection), null);
    }

    public new void Remove(T item)
    {
        base.Remove(item);
        onCollectionChanged(null, new List<T>() { item });
    }

    public new void RemoveRange(int startIndex, int count)
    {
        onCollectionChanged(null, GetRange(startIndex, count));
        base.RemoveRange(startIndex, count);
    }

    public new void RemoveAll(Predicate<T> match)
    {
        onCollectionChanged(null, base.FindAll(match));
        base.RemoveAll(match);
    }
    #endregion
}