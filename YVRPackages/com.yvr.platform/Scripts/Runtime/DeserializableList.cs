using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    public class DeserializableList<T> : IList<T>
    {
        public List<T> data { protected set; get; }

        public T this[int index] { get { return data[index]; } set { data[index] = value; } }

        public int Count { get { return data.Count; } }

        public bool IsReadOnly { get { return ((IList<T>)data).IsReadOnly; } }

        public void Add(T item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(T item)
        {
            return data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            data.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return data.Remove(item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("Count:【{0}】,\n\r", Count));
            str.Append(string.Format("IsReadOnly:【{0}】,\n\r", IsReadOnly));
            str.Append(string.Format("data:【{0}】,\n\r", data != null ? data.ToString() : "null"));
            return str.ToString();
        }
    }
}