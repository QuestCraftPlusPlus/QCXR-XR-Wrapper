using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginText : MonoBehaviour
{

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = JNIStorage.apiClass.GetStatic<string>("msaMessage");
        if(GetComponent<TextMeshProUGUI>().text != "")
        {
            GetComponent<TextMeshProUGUI>().text  = GetComponent<TextMeshProUGUI>().text + "Please wait until you see no more files downloading. Click the Microsoft button again once its done.";
        }
    }
}
