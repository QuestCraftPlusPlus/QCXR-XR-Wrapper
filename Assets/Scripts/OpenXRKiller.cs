using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpenXRKiller : MonoBehaviour
{
    private bool hasKilled = false;
    public void KillOpenXR()
    {
        if (!hasKilled)
        {
            var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
            foreach (var xrDisplay in xrDisplaySubsystems)
            {
                xrDisplay.Destroy();
            }
            hasKilled = true;
        }
    }
}
