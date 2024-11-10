using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class LoginHandler : MonoBehaviour
{
    bool isLoggedIn;
    
    [SerializeField] private TMP_Dropdown accountDropdown;
    public List<ConfigHandler.Accounts> accounts = new();
    private Dictionary<string, string> accountUUIDMap = new();
    private string configPath;
    private string configFile;
    public GameObject loginText;
    public WindowHandler handler;
    public UIHandler UIHandler;
    public bool isDemoMode;
    
    public string selectedAccountUsername;
    private string selectedAccountUUID;
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
	    if (Application.platform != RuntimePlatform.Android) return;
		isLoggedIn = false;
	    selectedAccountUsername = accountDropdown.options[accountDropdown.value].text;
	    selectedAccountUUID = accountUUIDMap[selectedAccountUsername];
	    Debug.Log(selectedAccountUUID);
	    loginText.SetActive(true);
	    JNIStorage.apiClass.CallStatic("login", JNIStorage.activity, selectedAccountUsername == "Add Account" ? null : selectedAccountUUID);
	    CheckVerification();
    }

    public void ParseAccounts()
    {
	    accounts.Clear();
	    accountUUIDMap.Clear();

	    int i = 0;
	    foreach (var account in config.accounts.Where(account => account != null))
	    {
		    Debug.Log("Account " + i++);
		    accounts.Add(account);
		    accountUUIDMap[account.username] = account.uuid;
		    Debug.Log("Account " + i++ + " added.");
	    }

	    Debug.Log("Accounts parsed.");
	    ConfigHandler.Accounts addAccount = new ConfigHandler.Accounts { username = "Add Account", uuid = "null" };

	    if (accounts.Count == 0)
	    {
		    Debug.Log("No accounts, using account sign in.");
		    accounts.Add(addAccount);
		    accountUUIDMap[addAccount.username] = addAccount.uuid;
		    accountDropdown.ClearOptions();
		    accountDropdown.AddOptions(accounts.Select(account => account.username).ToList());
		    Login();
	    }
	    else
	    {
		    accounts.Add(addAccount);
		    accountUUIDMap[addAccount.username] = addAccount.uuid;
		    accountDropdown.ClearOptions();
		    accountDropdown.AddOptions(accounts.Select(account => account.username).ToList());
	    }
    }
    
    private async void CheckVerification() {
	    if (Application.platform != RuntimePlatform.Android) return;
	    while (!isLoggedIn)
	    {
		    await Task.Delay(1500);

		    JNIStorage.accountObj = JNIStorage.apiClass.GetStatic<AndroidJavaObject>("currentAcc");
		    Debug.Log("Check Login State");
		    if (JNIStorage.accountObj != JNIStorage.apiClass.GetStatic<AndroidJavaObject>("currentAcc"))
		    {
			    if (JNIStorage.accountObj != null)
			    {
				    string configFile = File.ReadAllText(configPath);
				    string accName = JNIStorage.apiClass.GetStatic<string>("profileName");
				    string accUUID = JNIStorage.apiClass.GetStatic<string>("profileUUID");
				    Debug.Log("Logged Into " + accName + " | " + accUUID);
				    loginText.SetActive(false);
				    isLoggedIn = true;
				    handler.LoadAv(accName);
				    config = JsonConvert.DeserializeObject<ConfigHandler.Config>(configFile);
				    if (config.accounts.All(account => account.uuid != accUUID))
				    {
					    config.accounts.Add(new ConfigHandler.Accounts { username = accName, uuid = accUUID });
				    }

				    config.lastSelectedAccount = accountDropdown.value;
				    string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
				    File.WriteAllText(configPath, JSON);
				    isDemoMode = JNIStorage.apiClass.GetStatic<bool>("isDemoMode");
				    UIHandler.UILoginCheck(isDemoMode);
				    ParseAccounts();
			    }
		    }
	    }
    }
    
    public void LogoutButton()
    {
		handler.LogoutWindowSetter();
    }

    public void RemoveAccount()
    {
	    JNIStorage.accountObj = null;
	    JNIStorage.apiClass.CallStatic<bool>("removeAccount", JNIStorage.activity, selectedAccountUUID);
	    
	    configFile = File.ReadAllText(configPath);
	    config = JsonConvert.DeserializeObject<ConfigHandler.Config>(configFile);
	    
	    config.accounts.RemoveAll(account => account.username == selectedAccountUsername);
	    config.lastSelectedAccount = 0;
	    string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
	    File.WriteAllText(configPath, JSON);
	    
	    accountDropdown.ClearOptions();
	    accountDropdown.AddOptions(accounts.Select(account => account.username).ToList());

	    handler.LogoutWindowUnsetter();
	    ParseAccounts();
    }
}