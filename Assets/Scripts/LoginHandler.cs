using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    bool isLoggedIn;
    private bool hasAttemptedLogin;
    
    [SerializeField] private TMP_Dropdown accountDropdown;
    public List<ConfigHandler.Accounts> accounts = new();
    private string configPath;
    private string configFile;
    public GameObject loginText;
    public Material profilePicture;
    public TextMeshProUGUI profileNameHolder;
    public WindowHandler handler;
    private bool isAnimating;

    private ConfigHandler.Config config;

    public void Start()
    {
	    Debug.Log(Application.persistentDataPath + "/launcher.conf");
	    configPath = Application.persistentDataPath + "/launcher.conf";
	    configFile = File.ReadAllText(configPath);
	    config = JsonConvert.DeserializeObject<ConfigHandler.Config>(configFile);
	    ParseAccounts();
	    Login();
    }

    public void Login()
    {
	    if (Application.platform != RuntimePlatform.Android || hasAttemptedLogin) return;

	    string selectedAccount = accountDropdown.options[accountDropdown.value].text;
	    JNIStorage.apiClass.CallStatic("login", JNIStorage.activity, selectedAccount == "Add Account" ? null : selectedAccount);
	    CheckVerification();
	    hasAttemptedLogin = true;
    }

    public void ParseAccounts()
    {
	    accounts.Clear();
	    int i = 0;
	    foreach (ConfigHandler.Accounts account in config.accounts)
	    {
		    if (account != null)
		    {
			    Debug.Log("Account " + i++);
			    accounts.Add(account);
			    Debug.Log("Account " + i++ + " added.");
		    }
		    
	    }

	    Debug.Log("Accounts parsed.");
	    ConfigHandler.Accounts addAccount = new ConfigHandler.Accounts { username = "Add Account", uuid = "" };

	    if (accounts.Count == 0)
	    {
		    Debug.Log("No accounts, using account sign in.");
		    accounts.Add(addAccount);
		    accountDropdown.ClearOptions();
		    accountDropdown.AddOptions(accounts.Select(account => account.username).ToList());
		    Login();
		    return;
	    }

	    accounts.Add(addAccount);
	    accountDropdown.ClearOptions();
	    accountDropdown.AddOptions(accounts.Select(account => account.username).ToList());
    }

    
    private async void CheckVerification() {
	    if (Application.platform != RuntimePlatform.Android)
		    return;
	    while (isLoggedIn == false)
	    {
		    await Task.Delay(1500);
		    
		    JNIStorage.accountObj = JNIStorage.apiClass.GetStatic<AndroidJavaObject>("currentAcc");
		    Debug.Log("Check Login State");
		    if (JNIStorage.accountObj != null) 
		    {
			    handler.LoadAv(profileNameHolder.text, profilePicture);
			    isLoggedIn = true;
			    string configFile = File.ReadAllText(configPath);
			    string accName = JNIStorage.apiClass.GetStatic<string>("profileName");
			    string accUUID = JNIStorage.apiClass.GetStatic<string>("profileUUID");
			    config = JsonConvert.DeserializeObject<ConfigHandler.Config>(configFile);
			    config.accounts.Add(new ConfigHandler.Accounts {username = accName, uuid = accUUID});
			    string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
			    File.WriteAllText(configPath, JSON);
			    ParseAccounts();
			    loginText.SetActive(false);
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
	  isLoggedIn = false;
	  JNIStorage.accountObj = null;
    }

    public void RemoveAccount()
    {
	    // Not implemented
    }
}