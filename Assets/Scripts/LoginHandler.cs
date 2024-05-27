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
		async void LoadingButtonText()
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
			await Task.CompletedTask;
		}
		LoadingButtonText();

#if !UNITY_EDITOR
		if (hasAttemptedLogin) return;
		javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
		JNIStorage.apiClass.CallStatic("login", javaObject);
		CheckVerification();
		hasAttemptedLogin = true;
#endif
	}
	
	private async void CheckVerification() 
	{
#if !UNITY_EDITOR
		while (!isMainScreen)
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
#endif
		await Task.CompletedTask;
	}
	
	public void LogoutButton()
	{
		handler.LogoutWindowSetter();
		handler.logoutWindow.GetComponent<TextMeshProUGUI>().text = "Are you sure you would like to sign out?";
	}

	public void Logout()
	{
#if !UNITY_EDITOR
		isMainScreen = false;
		JNIStorage.accountObj = null;
		JNIStorage.apiClass.CallStatic<bool>("logout", JNIStorage.activity);
		Application.Quit();
#endif
	}
}
