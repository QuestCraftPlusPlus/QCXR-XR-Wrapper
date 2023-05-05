using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class LoginHandler : MonoBehaviour
{
    public WindowHandler handler;
    bool hasAttemptedLogin = false;

    public void Login()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        JNIStorage.accountObj = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("login", "d17a73a2-707c-40f5-8c90-d3eda0956f10", jo);
        if (JNIStorage.accountObj != null)
        {
            handler.MainPanelSwitch();
        } else if(hasAttemptedLogin) {
            JNIStorage.apiClass.SetStatic("msaMessage", "Login failure! Ensure you have accepted the permissions then try again.");
        }
        hasAttemptedLogin = true;
    }

    public void LogoutButton()
    {
        handler.ErrorWindowSetter();
        handler.errorWindow.GetComponent<TextMeshProUGUI>().text = "Are you sure you would like to sign out?";
        handler.errorWindow.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void Logout()
    {
        JNIStorage.apiClass.CallStatic<AndroidJavaObject>("logout", JNIStorage.home);
        handler.errorWindow.transform.GetChild(2).gameObject.SetActive(false);
        handler.errorWindow.SetActive(false);
    }
}