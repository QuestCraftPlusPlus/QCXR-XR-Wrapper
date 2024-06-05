using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    public GameObject devOptionsMenu;
    public GameObject loginElements;
    public GameObject loginButton;
    public CanvasGroup loginText; 
    
    public GameObject startPanel;
    
    public GameObject mainPanel;
    public TextMeshProUGUI profileNameHolder;
    public RawImage pfpHolder;
    
    public GameObject modSearchMenu;
    public GameObject modSearchPanel;
    public GameObject modInfoMenu;

    public InstanceManager instanceManager;
    public GameObject instanceMenu;
    public GameObject instanceMainpage;
    public GameObject instanceCreator;
    public GameObject instanceInfo;
    public GameObject instanceDeleteWarning;
    
    public GameObject logoutWindow;
    public GameObject errorWindow;
    
    public GameObject githubLogToggle;
    
    public GameObject needHelpPanel;
    
    public SkinHandler skinHandler;


    public void MainPanelSwitch()
    {
        startPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public async void LoadAv()
    {
        Debug.Log("QCXR: Getting PFP and Username.");
        await UIHandler.GetName(profileNameHolder);
        skinHandler.LoadSkin(profileNameHolder.text);
    }

    public void DevMenuSetter()
    {
        loginElements.SetActive(false);
	    devOptionsMenu.SetActive(true);
    }
    
    public void DevMenuUnsetter()
    {
        loginElements.SetActive(true);
        devOptionsMenu.SetActive(false);
    }

    public void ModManagerButton()
    {
        modSearchMenu.SetActive(false);
        mainPanel.SetActive(false);
        modInfoMenu.SetActive(false);
    }

    public void ModSearchButton()
    {
        instanceMenu.SetActive(false);
        modInfoMenu.SetActive(false);
        modSearchMenu.SetActive(true);
        modSearchPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void InstanceManagerSetter()
    {
        instanceManager.CreateInstanceArray();
        instanceMenu.SetActive(true);
        mainPanel.SetActive(false);
    }
    
    public void InstanceManagerUnsetter()
    {
        instanceMenu.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void InstanceCreatorSetter()
    {
        instanceMainpage.SetActive(false);
        instanceCreator.SetActive(true);
    }

    public void InstanceCreatorUnsetter()
    {
        instanceCreator.SetActive(false);
        instanceMainpage.SetActive(true);
        instanceManager.CreateInstanceArray();
    }

    public void InstanceInfoSetter()
    {
        instanceInfo.SetActive(true);
        instanceMainpage.SetActive(false);
    }
    
    public void InstanceInfoUnsetter()
    {
        instanceInfo.SetActive(false);
        instanceMainpage.SetActive(true);
        instanceManager.CreateInstanceArray();
        JNIStorage.instance.UpdateInstances();
    }
    
    public void LogoutWindowSetter()
    {
        logoutWindow.SetActive(true);
    }
    
    public void LogoutWindowUnsetter()
    {
        logoutWindow.SetActive(false);
    }
    
    public void InstanceDeleteWarningSetter()
    {
        instanceDeleteWarning.SetActive(true);
    }
    
    public void InstanceDeleteWarningUnsetter()
    {
        instanceDeleteWarning.SetActive(false);
        instanceManager.CreateInstanceArray();
    }

    public void ErrorMenuSetter()
    {
        errorWindow.SetActive(true);
    }
    
    public void ErrorMenuUnsetter()
    {
        errorWindow.SetActive(false);
    }

    [ContextMenu("LoginAnim")]
    public void AnimateLogin()
    {
            LeanTween.value(loginButton, loginButton.transform.localPosition.x, 0, 1).setEase(LeanTweenType.easeInOutCubic).setOnUpdate(newX =>
            {
                loginButton.transform.localPosition = new Vector3(newX, loginButton.transform.localPosition.y, 0);
                loginText.gameObject.transform.localPosition = new Vector3(newX + 500, loginText.transform.localPosition.y, 0);
            });
            LeanTween.delayedCall(0.5f, () =>
                LeanTween.value(loginText.gameObject, 0, 1, 0.5f).setEase(LeanTweenType.easeInOutCubic)
                    .setOnUpdate(opacity => loginText.alpha = opacity));
    }

    private bool githubLogAnimating;
    public void GithugLogSetter()
    {
        Debug.Log(githubLogToggle.transform.localPosition);
        if (githubLogAnimating)
            return;
        githubLogAnimating = true;
        
        //folded out position on x: -131.93
        //folded in position on x: 132.70
        if (githubLogToggle.transform.localPosition.x == -131.93f)
            LeanTween.value(githubLogToggle.gameObject, githubLogToggle.transform.localPosition, new(132.70f, githubLogToggle.transform.localPosition.y), 0.75f).setEase(LeanTweenType.easeInOutCubic).setOnUpdate((Vector3 loc) => githubLogToggle.transform.localPosition = loc).setOnComplete(() => githubLogAnimating = false);
        else
            LeanTween.value(githubLogToggle.gameObject, githubLogToggle.transform.localPosition, new Vector3(-131.93f, githubLogToggle.transform.localPosition.y), 0.75f).setEase(LeanTweenType.easeInOutCubic).setOnUpdate((Vector3 loc) => githubLogToggle.transform.localPosition = loc).setOnComplete(() => githubLogAnimating = false);
    }

    public void NeedHelpSetter()
    {
        needHelpPanel.SetActive(!needHelpPanel.activeSelf);
    }

}