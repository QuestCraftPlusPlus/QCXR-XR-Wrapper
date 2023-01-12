using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public enum EyeSide { Left = 1 << 0, Right = 1 << 1, Both = Left | Right }
}
