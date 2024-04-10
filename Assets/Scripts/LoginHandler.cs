using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public WindowHandler handler;
    bool isMainScreen;
    AndroidJavaClass jc;
    AndroidJavaObject jo;
    
    public void Login() {
		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		JNIStorage.apiClass.CallStatic("login", jo);
		CheckVerification();
    }
    
    private async void CheckVerification() {
	    while (true)
	    {
		    await Task.Delay(3000);
		    
		    if (JNIStorage.accountObj != null && !isMainScreen) {
			    handler.MainPanelSwitch();
			    isMainScreen = true;
		    } else {
			    JNIStorage.accountObj = JNIStorage.apiClass.GetStatic<AndroidJavaObject>("currentAcc");
			    Debug.Log("Check Login State");
		    }
		    
		    Debug.Log("End of login task");
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
	    isMainScreen = false;
	    JNIStorage.accountObj = null;
        JNIStorage.apiClass.CallStatic<bool>("logout", JNIStorage.activity);
        
        handler.errorWindow.transform.GetChild(2).gameObject.SetActive(false);
        handler.errorWindow.SetActive(false);
        handler.startPanel.SetActive(true);
        handler.mainPanel.SetActive(false);
    }
}