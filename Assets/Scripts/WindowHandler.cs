using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    public GameObject devOptionsMenu;
    public GameObject legalPanel;
    
    public GameObject mainPanel;
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

    public void LoadAv(string username)
    {
        Debug.Log("QCXR: Getting PFP and Username.");
        skinHandler.LoadSkin(username);
    }

    public void MainPanelSwitch()
    {
        mainPanel.SetActive(true);
        modSearchMenu.SetActive(false);
        instanceMenu.SetActive(false);
    }
    
    public void DevMenuSetter()
    {
	    devOptionsMenu.SetActive(true);
    }
    
    public void DevMenuUnsetter()
    {
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

    public void LegalSetter()
    {
        legalPanel.SetActive(!legalPanel.activeSelf);
    }

}