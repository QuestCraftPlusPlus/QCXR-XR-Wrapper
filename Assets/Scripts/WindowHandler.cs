using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    public GameObject devOptionsMenu;
    public GameObject loginElements;
    public GameObject mainPanel;
    public GameObject startPanel;
    public GameObject modSearchMenu;
    public GameObject modSearchPanel;
    public GameObject instanceMenu;
    public GameObject instanceMenuPanel;
    public GameObject instanceMainpage;
    public GameObject instanceCreator;
    public GameObject instanceEditor;
    public GameObject instanceInfo;
    public GameObject modInfoMenu;
    public GameObject instanceDeleteWarning;
    public GameObject logoutWindow;
    public GameObject errorWindow;
    public TextMeshProUGUI profileNameHolder;
    public RawImage pfpHolder;
    public InstanceManager instanceManager;
    public GameObject githubLogToggle;
    public GameObject githubLog;
    
    public void MainPanelSwitch()
    {
        startPanel.SetActive(false);
        mainPanel.SetActive(true);
        UIHandler.GetTexturePlusName(pfpHolder, profileNameHolder);
        Debug.Log("QCXR: Getting PFP and Username.");
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

    public void LogoutButton()
    {
        mainPanel.SetActive(false);
        startPanel.SetActive(true);
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
    }

    public void InstanceEditorSetter()
    {
        instanceMainpage.SetActive(false);
        instanceEditor.SetActive(true);
        instanceManager.CreateInstanceArray();
    }
    
    public void InstanceEditorUnsetter()
    {
        instanceEditor.SetActive(false);
        instanceMainpage.SetActive(true);
    }

    public void InstanceInfoSetter()
    {
        instanceMenuPanel.SetActive(false);
        instanceInfo.SetActive(true);
    }
    
    public void InstanceInfoUnsetter()
    {
        instanceInfo.SetActive(false);
        instanceMenuPanel.SetActive(true);
    }
    
    public void ModInfoSetter()
    {
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modInfoMenu.SetActive(true);
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
    }

    public void ErrorMenuSetter()
    {
        errorWindow.SetActive(true);
    }
    
    public void ErrorMenuUnsetter()
    {
        errorWindow.SetActive(false);
    }

    public void GithugLogSetter()
    {
        githubLog.SetActive(!githubLog.activeSelf);
        
        //folded out position on x: 314.3691
        //folded in position on x: 574.3691
        githubLogToggle.transform.localPosition = 
            githubLogToggle.transform.localPosition.x == 574.3691f ? 
                new(314.3691f , githubLogToggle.transform.localPosition.y) : 
                new( 574.3691f, githubLogToggle.transform.localPosition.y);

    }

}