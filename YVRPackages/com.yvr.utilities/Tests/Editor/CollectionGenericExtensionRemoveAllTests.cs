using System.Collections.Generic;
using NUnit.Framework;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class CollectionGenericExtensionRemoveAllTests
    {
        [Test, TestCaseSource(nameof(sourceStringData))]
        public void TestRemoveAll(List<string> sampleList)
        {
            List<string> resultList = new List<string>(new string[] { "A", "B", "C" });
            //Remove all elements from the resultList that are identical to those in the sampleList
            resultList.RemoveAll(sampleList, (p1, p2) => p1.Equals(p2));
            Assert.AreEqual(resultList.Count, 2);
        }

        private static List<string>[] sourceStringData =
        {
            new List<string>(new string[]{"A","a"}),
            new List<string>(new string[]{"b","B"}),
            new List<string>(new string[]{"C","c"}),
        };
    }
}