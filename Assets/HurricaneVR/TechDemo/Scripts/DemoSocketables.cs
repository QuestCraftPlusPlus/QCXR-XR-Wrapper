using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Sockets;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoSocketables : HVREnumFlagsSocketable<DemoSocketableItems>
    {
    }

    [Flags]
    public enum DemoSocketableItems
    {
        None = 0,
        Pistol = 1 << 0,
        PistolMagazine = 1 << 1,
        SMG = 1 << 2,
        SMGMagazine = 1 << 3,
        PowerCell = 1 << 4,
        SmallObject = 1 << 5,
        LargeObject = 1 << 6,
        DoorKey = 1 << 7,
        ShotgunShell = 1 << 8,
        Valve = 1 << 9,
        Key = 1 << 10,
        All = ~0
    }
}