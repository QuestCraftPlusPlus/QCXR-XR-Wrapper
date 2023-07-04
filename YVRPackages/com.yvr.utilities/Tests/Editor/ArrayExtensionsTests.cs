using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class ArrayExtensionsTests
    {
        private static Array[] s_TestArrayCases =
        {
            new int[] { },
            new int[] {1, 2, 3},
            new int[,] {{1, 2, 3}, {4, 5, 6}},
            new int[,,]
            {
                {{1, 2, 3}, {4, 5, 6}},
                {{7, 8, 9}, {10, 11, 12}},
                {{13, 14, 15}, {16, 17, 18}},
            }
        };

        private static List<int>[] s_TargetCastIntLists =
        {
            new List<int>(),
            new List<int>() {1, 2, 3},
            new List<int>() {1, 2, 3, 4, 5, 6},
            new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18},
        };

        private static string[] s_TargetDebugStr =
        {
            "",
            "1, 2, 3",
            "1, 2, 3, 4, 5, 6",
            "1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18"
        };

        private static object[] s_Array2StrMatch =
        {
            new object[] {s_TestArrayCases[0], s_TargetDebugStr[0]},
            new object[] {s_TestArrayCases[1], s_TargetDebugStr[1]},
            new object[] {s_TestArrayCases[2], s_TargetDebugStr[2]},
        };

        private static object[] s_Array2IntListMatch =
        {
            new object[] {s_TestArrayCases[0], s_TargetCastIntLists[0]},
            new object[] {s_TestArrayCases[1], s_TargetCastIntLists[1]},
            new object[] {s_TestArrayCases[2], s_TargetCastIntLists[2]},
        };

        [Test]
        [TestCaseSource(nameof(s_Array2StrMatch))]
        public void ToElementString_IntElement_GetTargetResult(Array array, string targetStr)
        {
            Assert.That(array.ToElementsString(), Is.EqualTo(targetStr));
        }

        #region Cast

        [Test]
        [TestCaseSource(nameof(s_Array2IntListMatch))]
        public void Cast_Generic_SameType_SequenceEqual(Array array, List<int> targetList)
        {
            IEnumerable<int> result = array.Cast<int>();

            Assert.That(result.SequenceEqual(targetList), Is.True);
        }

        #endregion

        [Test]
        [TestCaseSource(nameof(s_TestArrayCases))]
        public void ForEach_ActionBeCalledForEveryElement(Array array)
        {
            IEnumerable<int> target = array.Cast<int>().ToList();
            List<int> result = new List<int>();
            array.ForEach<int>(element => { result.Add(element); });
            Assert.That(result.SequenceEqual(target), Is.True);
        }

        [Test]
        [TestCaseSource(nameof(s_TestArrayCases))]
        public void IndexForEach_MultiDimensionalArray_TriggerEachIndices(Array array)
        {
            // Creating
            Array result = null;
            switch (array.Rank)
            {
                case 1:
                    result = new int[array.Length];
                    break;
                case 2:
                    result = new int[array.GetLength(0), array.GetLength(1)];
                    break;
                case 3:
                    result = new int[array.GetLength(0), array.GetLength(1), array.GetLength(2)];
                    break;
                default:
                    result = null;
                    break;
            }

            array.IndexForEach(indices => result.SetValue(array.GetValue(indices), indices));

            List<int> arrayList = array.Cast<int>().ToList();
            List<int> resultList = result.Cast<int>().ToList();

            Assert.That(resultList.SequenceEqual(arrayList), Is.True);
        }

        [Test]
        public void Select_EachElementAddOne_EachElementIncreasedByOne()
        {
            IEnumerable<int> targets = new List<int>() {2, 3, 4};

            Array array = new[] {1, 2, 3};

            IEnumerable<int> result = array.Select<int, int>(element => element + 1);

            Assert.That(result.SequenceEqual(targets));
        }

        [Test]
        public void Select_DifferentType_EachElementAddOne_EachElementIncreasedByOne()
        {
            IEnumerable<float> targets = new List<float>() {2.0f, 3.0f, 4.0f};

            Array array = new[] {1, 2, 3};

            IEnumerable<float> result = array.Select<int, float>(element => element + 1);

            Assert.That(result.SequenceEqual(targets));
        }
    }
}