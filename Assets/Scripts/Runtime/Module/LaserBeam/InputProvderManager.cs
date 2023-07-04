using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YVR.Core;

public class InputProvderManager : MonoBehaviour
{
    private void Update()
    {
        if (YVRInput.GetDown(YVRInput.RawButton.All, ControllerType.LeftTouch) && YVRInputModule.Instance.inputDataProviderSource != DataProviderSource.LeftController)
        {
            Debug.Log("YVRCraft --> switch input provider to left controller");
            YVRInputModule.Instance.SetInputDataProvider( DataProviderSource.LeftController);
        }
        else if (YVRInput.GetDown(YVRInput.RawButton.All, ControllerType.RightTouch)&& YVRInputModule.Instance.inputDataProviderSource != DataProviderSource.RightController)
        {
            Debug.Log("YVRCraft --> switch input provider to right controller");
            YVRInputModule.Instance.SetInputDataProvider( DataProviderSource.RightController);
        }
    }
    
}
