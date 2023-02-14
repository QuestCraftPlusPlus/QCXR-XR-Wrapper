using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;

public class YVRControllerExtTests
{
    [DllImport("yvrplugin")]
    private static extern IntPtr YVRGetControllerSerialNumber(uint controller);

    [DllImport("yvrplugin")]
    private static extern void ReleasePointerMemory(IntPtr pt);

    [Test]
    public void GetSerialNumber_NotNone()
    {
        IntPtr leftResult = YVRGetControllerSerialNumber(0);
        string leftSN = Marshal.PtrToStringAuto(leftResult);
        IntPtr rightResult = YVRGetControllerSerialNumber(1);
        string rightSN = Marshal.PtrToStringAuto(rightResult);

        Debug.Log($"Left Serial Number is {leftSN}");
        Debug.Log($"Right Serial Number is {rightSN}");
        ReleasePointerMemory(leftResult);
        ReleasePointerMemory(rightResult);
        Assert.That(!string.IsNullOrEmpty(leftSN));
        Assert.That(!string.Equals(rightSN,leftSN));
    }
}