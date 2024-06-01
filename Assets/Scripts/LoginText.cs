using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class LoginText : MonoBehaviour
{
    private TextMeshProUGUI loginText;
    private bool hasShown;
    public WindowHandler windowHandler;
    public string debugText;
    [HideInInspector] public string PreviousText;
    
    void Awake()
    {
        loginText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        string buffer;
        if (Application.isEditor)
            buffer = debugText;
        else
            buffer = JNIStorage.apiClass.GetStatic<string>("msaMessage");
        
        if(buffer == "" || PreviousText == buffer)
            return;
        
        PreviousText = buffer;
        loginText.text = buffer;
        
        if (!hasShown)
            windowHandler.AnimateLogin();
        hasShown = true;
        
        if (loginText.text.Contains("UnknownHostException"))
        {
            loginText.text = "<color=red>UnknownHostException!</color>\nCould not contact Microsoft's auth servers.\nCheck your internet connection and restart QuestCraft.";
            return;
        }
        
        loginText.text =
            Regex.Replace(
                loginText.text,
                @"enter the code (.+) to authenticate.",
                "enter the code <color=#1574d4>$1</color> to authenticate."
            );
    }
}
