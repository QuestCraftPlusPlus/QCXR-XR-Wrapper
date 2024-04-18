using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    public GameObject devOptionsMenu;
    public GameObject startButton;
    public GameObject devButton;
    public GameObject mainPanel;
    public GameObject startPanel;
    public GameObject modManagerPanel;
    public GameObject modSearchMenu;
    public GameObject instanceMenu;
    public GameObject instanceMainpage;
    public GameObject instanceCreator;
    public GameObject instanceEditor;
    public GameObject instanceInfo;
    public GameObject modInfoMenu;
    public GameObject modManagerMainpage;
    public GameObject instanceDeleteWarning;
    public GameObject logoutWindow;
    public GameObject errorWindow;
    public TextMeshProUGUI profileNameHolder;
    public RawImage pfpHolder;
    public InstanceManager instanceManager;
    
    public void MainPanelSwitch()
    {
        startPanel.SetActive(false);
        modManagerPanel.SetActive(false);
        mainPanel.SetActive(true);
        UIHandler.GetTexturePlusName(pfpHolder, profileNameHolder);
        Debug.Log("QCXR: Getting PFP and Username.");
    }

    public void DevMenuSetter()
    {
        startButton.SetActive(false);
        devButton.SetActive(false);
	    devOptionsMenu.SetActive(true);
    }
    
    public void DevMenuUnsetter()
    {
        startButton.SetActive(true);
        devButton.SetActive(true);
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
        modManagerPanel.SetActive(true);
        modManagerMainpage.SetActive(true);
    }

    public void ModSearchButton()
    {
        instanceMenu.SetActive(false);
        modInfoMenu.SetActive(false);
        modManagerMainpage.SetActive(false);
        modSearchMenu.SetActive(true);
    }

    public void InstanceManagerSetter()
    {
        modManagerMainpage.SetActive(false);
        instanceMenu.SetActive(true);
    }
    
    public void InstanceManagerUnsetter()
    {
        modManagerMainpage.SetActive(true);
        instanceMenu.SetActive(false);
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

    public void instanceInfoSetter()
    {
        instanceMenu.SetActive(false);
        instanceInfo.SetActive(true);
    }
    
    public void instanceInfoUnsetter()
    {
        instanceInfo.SetActive(false);
        instanceMenu.SetActive(true);
    }
    
    public void ModInfoSetter()
    {
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modManagerMainpage.SetActive(false);
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

}