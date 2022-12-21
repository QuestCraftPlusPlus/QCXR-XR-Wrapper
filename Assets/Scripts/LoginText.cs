using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginText : MonoBehaviour
{
    public JNIStorage storage;

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = storage.apiClass.GetStatic<string>("msaMessage");
        if(GetComponent<TextMeshProUGUI>().text != "")
        {
            GetComponent<TextMeshProUGUI>().text  = GetComponent<TextMeshProUGUI>().text + " Click Microsoft Login button once done.";
        }
    }
}
