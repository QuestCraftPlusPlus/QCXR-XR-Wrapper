using System.Collections.Generic;
using NUnit.Framework;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class CollectionGenericExtensionAddElementsFromTests
    {
        [Test, TestCaseSource(nameof(sourceIntData))]
        public void TestAddElementsFrom(List<int> sampleList)
        {
            List<int> resultList = new List<int>();
            //Adds elements greater than p1 from the sampleList to the resultList
            resultList.AddElementsFrom(sampleList, (p1, p2) => p1 < p2);
            Assert.AreEqual(resultList.Count, sampleList.Count);
        }

        private static List<int>[] sourceIntData =
        {
            new List<int>(new int[]{1,2,3,4,5}),
            new List<int>(new int[]{6,7,8,9,10}),
            new List<int>(new int[]{11,12,13,14,15}),
        };
    }
}