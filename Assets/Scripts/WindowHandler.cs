using System.Collections;
using System.Collections.Generic;
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
    public GameObject modInfoMenu;
    public GameObject modManagerMainpage;
    public GameObject errorWindow;
    
    public void MainPanelSwitch()
        {
            startPanel.SetActive(false);
            modManagerPanel.SetActive(false);
            mainPanel.SetActive(true);
        }

    public void DevMenuSetter()
    {
        startButton.SetActive(false);
        devOptionsMenu.SetActive(true);
        devButton.SetActive(false);
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

    public void InstanceManagerButton()
    {
        modInfoMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modManagerMainpage.SetActive(false);
        instanceMenu.SetActive(true);
    }

    public void ModInfoSetter()
    {
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modManagerMainpage.SetActive(false);
        modInfoMenu.SetActive(true);
    }
    
    public void ErrorWindowSetter()
    {
        errorWindow.SetActive(true);
    }    
    
    public void ErrorWindowUnsetter()
    {
        errorWindow.transform.GetChild(2).gameObject.SetActive(true);
        errorWindow.SetActive(false);
    }

}