using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHandler : MonoBehaviour
{

    public GameObject mainPanel;
    public GameObject startPanel;

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
