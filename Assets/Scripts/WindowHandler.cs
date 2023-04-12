using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{

    public GameObject mainPanel;
    public GameObject startPanel;
    public GameObject pfpHolder;

    public void MainPanelSwitch()
        {
            mainPanel.SetActive(true);
            startPanel.SetActive(false);
        }

    public void LogoutButton()
        {
            startPanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    
}
