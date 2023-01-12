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
            GetComponent<TextMeshProUGUI>().text  = GetComponent<TextMeshProUGUI>().text + "Click Microsoft Login button once this says its done.";
        }
    }
}
