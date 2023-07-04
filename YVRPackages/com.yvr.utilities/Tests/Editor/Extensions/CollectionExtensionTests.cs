using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class CollectionExtensionTests
    {
        #region SafeAdd

        [Test, Category("Extensions/Collection")]
        public void SafeAdd_KeyNotExist_ValueBeAdded()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            testDict.SafeAdd(3, 3);

            Assert.That(testDict.ContainsKey(3));
            Assert.That(testDict[3] == 3);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeAdd_KeyNotExist_ReturnFalse()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            bool ret = testDict.SafeAdd(3, 3);

            Assert.That(!ret);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeAdd_KeyExist_ValueNotBeAdded()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            testDict.SafeAdd(2, 3);

            Assert.That(testDict.Count == 2);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeAdd_KeyExist_ReturnTrue()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            bool ret = testDict.SafeAdd(2, 3);

            Assert.That(ret);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeAdd_KeyExistOverrideFalse_OriginValueNotBeChanged()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            testDict.SafeAdd(2, 3);

            Assert.That(testDict[2] == 2);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeAdd_KeyExistOverrideTrue_OriginValueBeChanged()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            testDict.SafeAdd(2, 3, true);

            Assert.That(testDict[2] == 3);
        }

        #endregion

        #region SafeGet

        [Test, Category("Extensions/Collection")]
        public void SafeGet_KeyExisted_GetValue()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            int ret = testDict.SafeGet(2);

            Assert.That(ret == 2);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeGet_KeyNotExisted_GetDefault()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            int ret = testDict.SafeGet(3);

            Assert.That(ret == default);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeGet_KeyNotExistedAutoAddTrue_GetDefault()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            int ret = testDict.SafeGet(3);

            Assert.That(ret == default);
            Assert.That(testDict.Count == 3);
        }

        [Test, Category("Extensions/Collection")]
        public void SafeGet_KeyNotExistedAutoAddFalse_GetDefault()
        {
            var testDict = new Dictionary<int, int>() {{1, 1}, {2, 2}};
            int ret = testDict.SafeGet(3, false);

            Assert.That(ret == default);
            Assert.That(testDict.Count == 2);
        }

        #endregion
    }
}