using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public WindowHandler handler;
    bool isMainScreen;
    private bool hasAttemptedLogin;
    AndroidJavaClass jc;
    AndroidJavaObject jo;
    
    public void Login()
    {
	    if (hasAttemptedLogin) return;
	    jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	    jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
	    JNIStorage.apiClass.CallStatic("login", jo);
	    CheckVerification();
	    hasAttemptedLogin = true;
    }
    
    private async void CheckVerification() {
	    if (Application.platform == RuntimePlatform.WindowsEditor)
		    return;
	    while (isMainScreen == false)
	    {
		    await Task.Delay(1500);
		    
		    if (JNIStorage.accountObj != null && !isMainScreen) {
			    handler.MainPanelSwitch();
			    isMainScreen = true;
		    } else {
			    JNIStorage.accountObj = JNIStorage.apiClass.GetStatic<AndroidJavaObject>("currentAcc");
			    Debug.Log("Check Login State");
		    }
	    }
	}
    
	public void LogoutButton()
	{
		handler.LogoutWindowSetter();
		handler.logoutWindow.GetComponent<TextMeshProUGUI>().text = "Are you sure you would like to sign out?";
	}

    public void Logout()
    {
	    isMainScreen = false;
	    JNIStorage.accountObj = null;
	    JNIStorage.apiClass.CallStatic<bool>("logout", JNIStorage.activity);
        hasAttemptedLogin = false;
        handler.logoutWindow.SetActive(false);
        handler.startPanel.SetActive(true);
        handler.mainPanel.SetActive(false);
    }
}