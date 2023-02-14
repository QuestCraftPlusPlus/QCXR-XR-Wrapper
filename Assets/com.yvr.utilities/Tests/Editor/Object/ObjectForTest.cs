using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.TestTools;

#pragma warning disable 0414 // Unused warning
namespace YVR.Utilities.Test
{
    public class ObjectForTestStruct
    {
        private int m_PrimitiveField = 0;
    }

    public class ObjectForTestRef
    {
        private int m_PrivateIntField = -1;
        public int publicIntField = -1;

        private int privateIntProperty { get; set; } = -1;
        private int privateIntPropertyReadOnly => m_PrivateIntField;

        public int publicIntProperty { get; set; } = -1;
        public int publicIntPropertyReadOnly => publicIntField;

        private ObjectForTestStruct structProperty { get; set; }

        private ObjectForTestRef refProperty { get; set; }
    }

    public class ObjectForTestInheritRef : ObjectForTestRef { }
}
#pragma warning disable 0414