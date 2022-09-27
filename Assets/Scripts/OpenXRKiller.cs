using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpenXRKiller : MonoBehaviour
{
    public void KillOpenXR()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            xrDisplay.Destroy();
        }
    }
}
