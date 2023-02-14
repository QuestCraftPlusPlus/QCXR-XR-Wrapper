using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Bags
{


    public class HVRGrabbableBag : MonoBehaviour
    {
        public VRGrabbableEvent GrabbableRemoved = new VRGrabbableEvent();

        [Header("Settings")]
        [Tooltip("If true, grabbed objects will be penalized with the sorting.")]
        public bool PenalizeGrabbed = true;

        public HVRSortMode hvrSortMode = HVRSortMode.SquareMagnitude;
        public float MaxDistanceAllowed;

        [Header("Transforms")] 
        [Tooltip("If assigned, the position of this transform will be used to calculate the distance.")]
        public Transform DistanceSource;
        
        public HVRGrabberBase Grabber;

        public List<HVRGrabbable> ValidGrabbables = new List<HVRGrabbable>(1000);
        public HVRGrabbable ClosestGrabbable;
        
        public List<HVRGrabbable> IgnoredGrabbables = new List<HVRGrabbable>();
        
        [SerializeField]
        private List<HVRGrabbable> _allGrabbables = new List<HVRGrabbable>();
        private readonly HashSet<HVRGrabbable> _distinctGrabbables = new HashSet<HVRGrabbable>();
        private readonly List<HVRGrabbable> _grabbablesToRemove = new List<HVRGrabbable>(1000);

        private readonly HashSet<HVRGrabbable> _ignoredGrabbables = new HashSet<HVRGrabbable>();
        private Sorter sorter = new Sorter();

        protected virtual void Awake()
        {

        }
        
        protected virtual void Start()
        {
            IgnoredGrabbables.ForEach(g => _ignoredGrabbables.Add(g));
        }

        private void FixedUpdate()
        {
            Calculate();
        }

        protected void AddGrabbable(HVRGrabbable grabbable)
        {
            if (_distinctGrabbables.Contains(grabbable))
            {
                return;
            }
            _distinctGrabbables.Add(grabbable);
            _allGrabbables.Add(grabbable);
        }

        protected virtual void RemoveGrabbable(HVRGrabbable grabbable)
        {
            if (_distinctGrabbables.Contains(grabbable))
            {
                _allGrabbables.Remove(grabbable);
            }
            _distinctGrabbables.Remove(grabbable);
            GrabbableRemoved.Invoke(grabbable);
        }

        protected virtual void Calculate()
        {
            ValidGrabbables.Clear();
            _grabbablesToRemove.Clear();
            sorter.DistanceMap.Clear();

            var anyDestroyedOrDisabled = false;

            for (var i = 0; i < _allGrabbables.Count; i++)
            {
                var grabbable = _allGrabbables[i];
                if (!grabbable || !grabbable.gameObject.activeInHierarchy || !grabbable.enabled)
                {
                    anyDestroyedOrDisabled = true;
                    continue;
                }

                sorter.DistanceMap[grabbable] = DistanceToGrabbable(grabbable);

                if (!grabbable.HasConcaveColliders && sorter.DistanceMap[grabbable] > MaxDistanceAllowed)
                {
                    _grabbablesToRemove.Add(grabbable);
                }
                else if (IsValid(grabbable))
                {
                    if (PenalizeGrabbed && grabbable.IsBeingHeld)
                    {
                        sorter.DistanceMap[grabbable] += 1000f;
                    }

                    ValidGrabbables.Add(grabbable);
                }
            }

            if (anyDestroyedOrDisabled)
            {
                _distinctGrabbables.RemoveWhere(e => e == null || !e.gameObject.activeInHierarchy || !e.enabled);
                _allGrabbables.RemoveAll(e => e == null || !e.gameObject.activeInHierarchy || !e.enabled);
            }

            for (var index = 0; index < _grabbablesToRemove.Count; index++)
            {
                var invalid = _grabbablesToRemove[index];
                RemoveGrabbable(invalid);
            }

            SortHelper.Sort(ValidGrabbables, 0, ValidGrabbables.Count, sorter);

            ClosestGrabbable = ValidGrabbables.FirstOrDefault();
        }

        public virtual float DistanceToGrabbable(HVRGrabbable grabbable)
        {
            var point = DistanceSource ? DistanceSource.position : Grabber.transform.position;
            
            if (hvrSortMode == HVRSortMode.Distance)
                return grabbable.GetDistanceToGrabber(point);
            return grabbable.GetSquareDistanceToGrabber(point);
        }

        protected virtual bool IsValid(HVRGrabbable grabbable)
        {
            if (grabbable.RequiresGrabbable)
            {
                if (!grabbable.RequiredGrabbable || !grabbable.RequiredGrabbable.IsBeingHeld)
                {
                    return false;
                }
            }
            return grabbable.CanBeGrabbed && !_ignoredGrabbables.Contains(grabbable);
        }
    }

    internal class SortHelper
    {

        public static void Sort<T>(List<T> keys, int index, int length, IComparer<T> comparer)
        {
            SortHelper.IntrospectiveSort(keys, index, length, comparer);
        }

        private static void SwapIfGreater<T>(List<T> keys, IComparer<T> comparer, int a, int b)
        {
            if (a == b || comparer.Compare(keys[a], keys[b]) <= 0)
                return;
            T key = keys[a];
            keys[a] = keys[b];
            keys[b] = key;
        }

        private static void Swap<T>(List<T> a, int i, int j)
        {
            if (i == j)
                return;
            T obj = a[i];
            a[i] = a[j];
            a[j] = obj;
        }

        internal static void IntrospectiveSort<T>(List<T> keys, int left, int length, IComparer<T> comparer)
        {
            if (length < 2)
                return;
            IntroSort<T>(keys, left, length + left - 1, 2 * FloorLog2(keys.Count), comparer);
        }

        internal static int FloorLog2(int n)
        {
            int num = 0;
            for (; n >= 1; n /= 2)
                ++num;
            return num;
        }

        private static void IntroSort<T>(
          List<T> keys,
          int lo,
          int hi,
          int depthLimit,
          IComparer<T> comparer)
        {
            int num1;
            for (; hi > lo; hi = num1 - 1)
            {
                int num2 = hi - lo + 1;
                if (num2 <= 16)
                {
                    if (num2 == 1)
                        break;
                    if (num2 == 2)
                    {
                        SwapIfGreater(keys, comparer, lo, hi);
                        break;
                    }
                    if (num2 == 3)
                    {
                        SwapIfGreater(keys, comparer, lo, hi - 1);
                        SwapIfGreater(keys, comparer, lo, hi);
                        SwapIfGreater(keys, comparer, hi - 1, hi);
                        break;
                    }
                    InsertionSort(keys, lo, hi, comparer);
                    break;
                }
                if (depthLimit == 0)
                {
                    Heapsort(keys, lo, hi, comparer);
                    break;
                }
                --depthLimit;
                num1 = PickPivotAndPartition(keys, lo, hi, comparer);
                IntroSort(keys, num1 + 1, hi, depthLimit, comparer);
            }
        }

        private static int PickPivotAndPartition<T>(List<T> keys, int lo, int hi, IComparer<T> comparer)
        {
            int index = lo + (hi - lo) / 2;
            SwapIfGreater(keys, comparer, lo, index);
            SwapIfGreater(keys, comparer, lo, hi);
            SwapIfGreater(keys, comparer, index, hi);
            T key = keys[index];
            Swap(keys, index, hi - 1);
            int i = lo;
            int j = hi - 1;
            while (i < j)
            {
                do
                {
                } while (comparer.Compare(keys[++i], key) < 0);
                do
                {
                } while (comparer.Compare(key, keys[--j]) < 0);
                if (i < j)
                    Swap(keys, i, j);
                else
                    break;
            }
            Swap(keys, i, hi - 1);
            return i;
        }

        private static void Heapsort<T>(List<T> keys, int lo, int hi, IComparer<T> comparer)
        {
            int n = hi - lo + 1;
            for (int i = n / 2; i >= 1; --i)
                DownHeap(keys, i, n, lo, comparer);
            for (int index = n; index > 1; --index)
            {
                Swap(keys, lo, lo + index - 1);
                DownHeap(keys, 1, index - 1, lo, comparer);
            }
        }

        private static void DownHeap<T>(List<T> keys, int i, int n, int lo, IComparer<T> comparer)
        {
            T key = keys[lo + i - 1];
            int num;
            for (; i <= n / 2; i = num)
            {
                num = 2 * i;
                if (num < n && comparer.Compare(keys[lo + num - 1], keys[lo + num]) < 0)
                    ++num;
                if (comparer.Compare(key, keys[lo + num - 1]) < 0)
                    keys[lo + i - 1] = keys[lo + num - 1];
                else
                    break;
            }
            keys[lo + i - 1] = key;
        }

        private static void InsertionSort<T>(List<T> keys, int lo, int hi, IComparer<T> comparer)
        {
            for (int index1 = lo; index1 < hi; ++index1)
            {
                int index2 = index1;
                T key;
                for (key = keys[index1 + 1]; index2 >= lo && comparer.Compare(key, keys[index2]) < 0; --index2)
                    keys[index2 + 1] = keys[index2];
                keys[index2 + 1] = key;
            }
        }
    }


    internal class Sorter : IComparer<HVRGrabbable>
    {
        internal readonly Dictionary<HVRGrabbable, float> DistanceMap = new Dictionary<HVRGrabbable, float>();

        public int Compare(HVRGrabbable x, HVRGrabbable y)
        {
            return DistanceMap[x].CompareTo(DistanceMap[y]);
        }
    }
}