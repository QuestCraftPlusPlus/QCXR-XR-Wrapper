using System.Collections.Generic;
using NUnit.Framework;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class CollectionGenericExtensionExtractTests
    {
        [Test, TestCaseSource(nameof(sourceIntData))]
        public void TestExtract(List<int> sampleList)
        {
            int originCount = sampleList.Count;
            List<int> resultList = sampleList.Extract(p => p > 3);

            int sum = resultList.Count + sampleList.Count;
            //The original list is split into two lists, so the sum of the two lists is the original count
            Assert.AreEqual(originCount, sum);

            resultList.ForEach(p =>
            {
                if (sampleList.Contains(p))
                    Assert.Fail();
            });
        }

        private static List<int>[] sourceIntData =
        {
            new List<int>(new int[]{1,2,3,4,5}),
            new List<int>(new int[]{6,7,8,9,10}),
            new List<int>(new int[]{11,12,13,14,15}),
        };
    }
}