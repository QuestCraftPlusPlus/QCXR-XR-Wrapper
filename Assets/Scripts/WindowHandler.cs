using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHandler : MonoBehaviour
{

    public GameObject mainPanel;
    public GameObject startPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MainPanelSwitch()
        {
            mainPanel.SetActive(true);
            startPanel.SetActive(false);
        }

    public void LogoutButton()
        {
            startPanel.SetActive(false);
            mainPanel.SetActive(true);
        } 
}
