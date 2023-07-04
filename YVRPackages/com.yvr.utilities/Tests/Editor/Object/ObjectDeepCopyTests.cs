using System.Collections.Generic;
using NUnit.Framework;
using UnityEditorInternal;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class ObjectDeepCopyTests
    {
        [Test]
        public void DeepCopy_PrivateField_Independent()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            copied.TrySetFieldValue("m_PrivateIntField", 3);

            copied.TryGetFieldValue("m_PrivateIntField", out int copiedVal);
            origin.TryGetFieldValue("m_PrivateIntField", out int originVal);
            
            Assert.That(copiedVal, Is.Not.EqualTo(originVal));
        }

        [Test]
        public void DeepCopy_PublicField_Independent()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            copied.publicIntField = 3;

            Assert.That(copied.publicIntField, Is.Not.EqualTo(origin.publicIntField));
        }

        [Test]
        public void DeepCopy_PrivateProperty_Independent()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            copied.TrySetPropertyValue("privateIntProperty", 3);

            copied.TryGetPropertyValue("privateIntProperty", out int copiedVal);
            origin.TryGetPropertyValue("privateIntProperty", out int originVal);

            Assert.That(copiedVal, Is.Not.EqualTo(originVal));
        }

        [Test]
        public void DeepCopy_PublicProperty_Independent()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            copied.publicIntProperty = 3;

            Assert.That(copied.publicIntProperty, Is.Not.EqualTo(origin.publicIntProperty));
        }

        [Test]
        public void DeepCopy_PrivateField_Copied()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            copied.TryGetFieldValue("m_PrivateIntField", out int copiedVal);
            origin.TryGetFieldValue("m_PrivateIntField", out int originVal);
            Assert.That(copiedVal, Is.EqualTo(originVal));
        }

        [Test]
        public void DeepCopy_PublicField_Copied()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            Assert.That(copied.publicIntField, Is.EqualTo(origin.publicIntField));
        }

        [Test]
        public void DeepCopy_PrivateProperty_Copied()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            copied.TryGetPropertyValue("privateIntPropertyGetSet", out int copiedVal);
            origin.TryGetPropertyValue("privateIntPropertyGetSet", out int originVal);

            Assert.That(copiedVal, Is.EqualTo(originVal));
        }

        [Test]
        public void DeepCopy_PublicProperty_Copied()
        {
            ObjectForTestRef origin = new ObjectForTestRef();
            ObjectForTestRef copied = origin.DeepCopy();

            Assert.That(copied.publicIntProperty, Is.EqualTo(origin.publicIntProperty));
        }

        [Test]
        public void DeepCopy_ListWithPrimitiveElement_Independent()
        {
            List<int> origin = new List<int>() {1, 2, 3, 4};
            List<int> copied = origin.DeepCopy();

            origin[0] = 2;

            Assert.That(copied[0], Is.Not.EqualTo(origin[0]));
            for (int i = 1; i < origin.Count; i++)
                Assert.That(copied[i], Is.EqualTo(origin[i]));
        }
    }
}