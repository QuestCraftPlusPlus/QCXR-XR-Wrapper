using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{

    public GameObject mainPanel;
    public GameObject startPanel;
    public GameObject modManagerPanel;
    public GameObject modSearchMenu;
    public GameObject instanceMenu;
    public GameObject modInfoMenu;

    public void MainPanelSwitch()
        {
            startPanel.SetActive(false);
            mainPanel.SetActive(true);
        }

    public void LogoutButton()
        {
            mainPanel.SetActive(false);
            startPanel.SetActive(true);
        }

    public void ModManagerButton()
    {
        mainPanel.SetActive(false);
        modManagerPanel.SetActive(true);
    }

    public void ModSearchButton()
    {
        instanceMenu.SetActive(false);
        modInfoMenu.SetActive(false);
        modSearchMenu.SetActive(true);
    }

    public void InstanceManagerButton()
    {
        modInfoMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        instanceMenu.SetActive(true);
    }

    public void ModInfoSetter()
    {
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modInfoMenu.SetActive(true);
    }
    
}