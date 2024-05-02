using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class LoginText : MonoBehaviour
{
    private TextMeshProUGUI loginText;
    private void Start()
    {
        loginText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            return;

        loginText.text = JNIStorage.apiClass.GetStatic<string>("msaMessage");
        if(loginText.text != "")
        {
            loginText.text =
                Regex.Replace(
                    loginText.text,
                    @"enter the code (.+) to authenticate.",
                    "enter the code <color=#1574d4>$1</color> to authenticate."
                    );
        }
    }
}