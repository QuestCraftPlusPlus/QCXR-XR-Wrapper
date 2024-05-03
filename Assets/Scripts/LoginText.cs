using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class LoginText : MonoBehaviour
{
    private TextMeshProUGUI loginText;
    private bool hasShown;
    public WindowHandler windowHandler;
    public string debugText;
    private void Start()
    {
        loginText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            loginText.text = debugText;
        else
            loginText.text = JNIStorage.apiClass.GetStatic<string>("msaMessage");

        
        if(loginText.text == "")
            return;

        if (!hasShown)
            windowHandler.AnimateLogin();
        hasShown = true;
        
        loginText.text =
            Regex.Replace(
                loginText.text,
                @"enter the code (.+) to authenticate.",
                "enter the code <color=#1574d4>$1</color> to authenticate."
            );
    }
}