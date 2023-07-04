using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable ExpressionIsAlwaysNull

namespace YVR.Utilities.Test
{
    [TestFixture, Category("Reflection")]
    public class ObjectReflectionTests
    {
        private static string s_PrivateIntFieldName = "m_PrivateIntField";
        private static string s_PrivateIntPropertyName = "privateIntProperty";

        [Test]
        public void TryGetFieldValue_ExistPrivateField_GetCorrectValue()
        {
            ObjectForTestRef objectForTest = new ObjectForTestRef();
            bool valueGot = objectForTest.TryGetFieldValue(s_PrivateIntFieldName, out int value);
            Assert.That(valueGot);
            Assert.That(value, Is.EqualTo(-1));
        }

        [Test]
        public void TryGetFieldValue_NonExistPrivateField_ReturnFalse()
        {
            ObjectForTestRef objectForTest = new ObjectForTestRef();
            bool valueGot = objectForTest.TryGetFieldValue("JustAWrongName", out int value);
            Assert.That(!valueGot);
        }

        [Test]
        public void TrySetFieldValue_ExistPrivateField_SetValueSucceed()
        {
            ObjectForTestRef objectForTest = new ObjectForTestRef();
            int targetValue = 1;
            bool valueSet = objectForTest.TrySetFieldValue(s_PrivateIntFieldName, targetValue);
            Assert.That(valueSet);

            objectForTest.TryGetFieldValue(s_PrivateIntFieldName, out int value);
            Assert.That(value, Is.EqualTo(targetValue));
        }

        [Test]
        public void TrySetFieldValue_ExistPrivateField_ReturnFalse()
        {
            ObjectForTestRef objectForTest = new ObjectForTestRef();
            int targetValue = 1;
            bool valueSet = objectForTest.TrySetFieldValue("JustAWrongName", targetValue);
            Assert.That(!valueSet);
        }

        [Test]
        public void TryGetFieldValue_ExistPrivateFieldInBaseClass_GetCorrectValue()
        {
            ObjectForTestRef objectForTest = new ObjectForTestInheritRef();

            bool valueGot = objectForTest.TryGetFieldValue(s_PrivateIntFieldName, out int value);
            Assert.That(valueGot);
            Assert.That(value, Is.EqualTo(-1));
        }

        [Test]
        public void TrySetFieldValue_ExistPrivateFieldInBaseClass_SetValueSucceed()
        {
            ObjectForTestRef objectForTest = new ObjectForTestInheritRef();
            int targetValue = 1;
            bool valueSet = objectForTest.TrySetFieldValue(s_PrivateIntFieldName, targetValue);
            Assert.That(valueSet);

            objectForTest.TryGetFieldValue(s_PrivateIntFieldName, out int value);
            Assert.That(value, Is.EqualTo(targetValue));
        }

        [Test]
        public void TryGetPropertyValue_ExistPrivateProperty_GetCorrectValue()
        {
            ObjectForTestRef objectForTest = new ObjectForTestRef();
            bool valueGot = objectForTest.TryGetPropertyValue(s_PrivateIntPropertyName, out int value);
            Assert.That(valueGot);
            Assert.That(value, Is.EqualTo(-1));
        }

        [Test]
        public void TrySetPropertyValue_ExistPrivateProperty_SetValueSucceed()
        {
            ObjectForTestRef objectForTest = new ObjectForTestRef();
            int targetValue = 1;
            bool valueSet = objectForTest.TrySetPropertyValue(s_PrivateIntPropertyName, targetValue);
            Assert.That(valueSet);

            objectForTest.TryGetPropertyValue(s_PrivateIntPropertyName, out int value);
            Assert.That(value, Is.EqualTo(targetValue));
        }

        [Test]
        public void TryGetPropertyValue_ExistPrivatePropertyInBaseClass_GetCorrectValue()
        {
            ObjectForTestRef objectForTest = new ObjectForTestInheritRef();
            bool valueGot = objectForTest.TryGetPropertyValue(s_PrivateIntPropertyName, out int value);
            Assert.That(valueGot);
            Assert.That(value, Is.EqualTo(-1));
        }

        [Test]
        public void TrySetPropertyValue_ExistPrivatePropertyInBaseClass_SetValueSucceed()
        {
            ObjectForTestRef objectForTest = new ObjectForTestInheritRef();
            int targetValue = 1;
            bool valueSet = objectForTest.TrySetPropertyValue(s_PrivateIntPropertyName, targetValue);
            Assert.That(valueSet);

            objectForTest.TryGetPropertyValue(s_PrivateIntPropertyName, out int value);
            Assert.That(value, Is.EqualTo(targetValue));
        }

        #region ReflectionEqual

        [Test]
        public void ReflectionEqual_DifferentType_NotEqual()
        {
            ObjectForTestRef objectForTestRef = new ObjectForTestRef();
            ObjectForTestStruct objectForTestStruct = new ObjectForTestStruct();

            Assert.That(!objectForTestRef.ReflectEquals(objectForTestStruct));
        }

        [Test]
        public void ReflectionEqual_SameReference_Equal()
        {
            ObjectForTestRef objectForTestRef = new ObjectForTestRef();
            ObjectForTestRef anotherObj = objectForTestRef;

            Assert.That(objectForTestRef.ReflectEquals(anotherObj));
        }

        [Test]
        public void ReflectionEqual_SameTypeSameVal_Equal()
        {
            ObjectForTestRef objectForTestRef = new ObjectForTestRef();
            ObjectForTestRef anotherObj = new ObjectForTestRef();

            Assert.That(objectForTestRef.ReflectEquals(anotherObj));
        }

        [Test]
        public void ReflectionEqual_SameTypeDifferentVal_Equal()
        {
            ObjectForTestRef objectForTestRef = new ObjectForTestRef();
            ObjectForTestRef anotherObj = new ObjectForTestRef();
            anotherObj.publicIntProperty++;

            Assert.That(!objectForTestRef.ReflectEquals(anotherObj));
        }

        [Test]
        public void ReflectionEqual_BothNull_Equal()
        {
            ObjectForTestRef objectForTestRef = null;
            ObjectForTestRef anotherObj = null;
            Assert.That(objectForTestRef.ReflectEquals(anotherObj));
        }

        [Test]
        public void ReflectionEqual_EitherNull_NotEqual()
        {
            ObjectForTestRef objectForTestRef = new ObjectForTestRef();
            ObjectForTestRef anotherObj = null;
            Assert.That(!objectForTestRef.ReflectEquals(anotherObj));

            objectForTestRef = null;
            anotherObj = new ObjectForTestRef();
            Assert.That(!objectForTestRef.ReflectEquals(anotherObj));
        }

        #endregion

        #region EnumberableRelated

        [Test]
        public void IsGenericEnumerable_List_True()
        {
            List<int> intList = new List<int>();
            Assert.That(intList.IsGenericEnumerable());
        }

        [Test]
        public void IsGenericEnumerable_String_True()
        {
            string str = "Test";
            Assert.That(str.IsGenericEnumerable());
        }

        [Test]
        public void IsGenericEnumerable_Int_False()
        {
            int test = 0;
            Assert.That(!test.IsGenericEnumerable());
        }

        [Test]
        public void GetGenericEnumerableType_PrimitiveList_GetPrimitiveType()
        {
            List<int> intList = new List<int>();
            Type elementType = intList.GetEnumerableElementType();
            Assert.That(elementType, Is.EqualTo(typeof(int)));
        }

        [Test]
        public void GetEnumerableElementType_PrimitiveDict_GetKeyValueType()
        {
            Dictionary<string, int> str2IntDict = new Dictionary<string, int>();
            Type elementType = str2IntDict.GetEnumerableElementType();
            Assert.That(elementType, Is.EqualTo(typeof(KeyValuePair<string, int>)));
        }

        [Test]
        public void ReflectionSequenceEquals_SameValueList_True()
        {
            List<int> intList = new List<int>() {1, 2, 3};
            List<int> anotherIntList = new List<int>() {1, 2, 3};

            Assert.That(intList.ReflectionSequenceEqual(anotherIntList));
        }

        [Test]
        public void ReflectionSequenceEquals_SameTypeDifferentValueList_False()
        {
            List<int> intList = new List<int>() {1, 2, 3};
            List<int> anotherIntList = new List<int>() {1, 2, 3, 4};

            Assert.That(!intList.ReflectionSequenceEqual(anotherIntList));
        }

        [Test]
        public void ReflectionSequenceEquals_DifferentTypeList_GetArgumentationException()
        {
            Exception exception = null;
            try
            {
                List<int> intList = new List<int>() {1, 2, 3};
                List<string> strList = new List<string>() {"abc", "def"};

                Assert.That(!intList.ReflectionSequenceEqual(strList));
            } catch (Exception ex)
            {
                exception = ex;
            } finally
            {
                Assert.That(exception.GetType(), Is.EqualTo(typeof(System.ArgumentException)));
            }
        }

        [Test]
        public void ReflectionSequenceEqual_SameValueDictionary_True()
        {
            Dictionary<string, int> str2IntDict = new Dictionary<string, int>() {{"abc", 1}, {"def", 2}};
            Dictionary<string, int> str2IntDict2 = new Dictionary<string, int>() {{"abc", 1}, {"def", 2}};

            Assert.That(str2IntDict.ReflectionSequenceEqual(str2IntDict2));
        }

        [Test]
        public void ReflectionSequenceEquals_DifferentValueDictionary_False()
        {
            Dictionary<string, int> str2IntDict = new Dictionary<string, int>() {{"abc", 1}, {"def", 2}};
            Dictionary<string, int> str2IntDict2 = new Dictionary<string, int>() {{"abc", 1}, {"def", 2}, {"ghi", 3}};

            Assert.That(!str2IntDict.ReflectionSequenceEqual(str2IntDict2));
        }

        #endregion
    }
}