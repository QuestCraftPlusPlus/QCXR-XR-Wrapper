using TMPro;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public WindowHandler handler;
    bool hasAttemptedLogin;
    AndroidJavaClass jc;
    AndroidJavaObject jo;

    public void Login() {
	if (FindObjectOfType<LoginText>().getMsaCode() != "") {
	    Application.OpenURL("https://login.live.com/oauth20_remoteconnect.srf?otc=" + FindObjectOfType<LoginText>().getMsaCode());
	} else {
	    jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	    jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
	    JNIStorage.apiClass.CallStatic("login", jo);
    	    hasAttemptedLogin = true;
	}
    }

    public void Update()
    {
	    if(!hasAttemptedLogin) {
	        return;
	    }
        
        if (JNIStorage.accountObj != null) {
            handler.MainPanelSwitch();
        } else {
            JNIStorage.accountObj = JNIStorage.apiClass.GetStatic<AndroidJavaObject>("currentAcc");
        }
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
