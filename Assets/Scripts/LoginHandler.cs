using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    bool isMainScreen;
    private bool hasAttemptedLogin;
    AndroidJavaClass javaClass;
    AndroidJavaObject javaObject;
    
    public TextMeshProUGUI loginButtonText;
    public WindowHandler handler;
    private bool isAnimating;
    
    public void Login()
    {
	    async Task LoadingButtonText()
	    {
		    if (isAnimating) return;
		    isAnimating = true;
		    
		    loginButtonText.text = "Loading";
		    await Task.Delay(550);
		    foreach (int placeholder in Enumerable.Range(1,3))
		    {
			    loginButtonText.text += ".";
			    await Task.Delay(600);
		    }
		    loginButtonText.text = "Sign In";
		    isAnimating = false;
	    }
	    LoadingButtonText();
	    
	    if (Application.platform == RuntimePlatform.WindowsEditor) return;
	    if (hasAttemptedLogin) return;
	    javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	    javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
	    JNIStorage.apiClass.CallStatic("login", javaObject);
	    CheckVerification();
	    hasAttemptedLogin = true;
    }
    
    private async void CheckVerification() {
	    if (Application.platform == RuntimePlatform.WindowsEditor)
		    return;
	    while (isMainScreen == false)
	    {
		    await Task.Delay(1500);
		    
		    JNIStorage.accountObj = JNIStorage.apiClass.GetStatic<AndroidJavaObject>("currentAcc");
		    Debug.Log("Check Login State");
		    if (JNIStorage.accountObj != null) 
		    {
			    handler.MainPanelSwitch();
			    handler.LoadAv();
			    isMainScreen = true;
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
      Application.Quit();
    }
}