using System;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private StartPanel m_StartPanel;
    [SerializeField] private LoginPanel m_LoginPanel;
    [SerializeField] private DownloadPanel m_DownloadPanel;
    [SerializeField] private LobbyPanel m_LobbyPanel;
    [SerializeField] private LoginFailTips m_LoginFailTips;
    [SerializeField] private LogoutPopup m_LogoutPopup;

    private void Start()
    {
        EventManager.AddListenerT(CommonDefine.EMEventType.PlatformCheckSuccess, OnPlatformCheckSuccess);
        EventManager.AddListenerT(CommonDefine.EMEventType.LoginSuccess, OnLoginSuccess);
        EventManager.AddListenerT(CommonDefine.EMEventType.LoginFail, OnLoginFail);
        EventManager.AddListenerT(CommonDefine.EMEventType.LoginCheckFail, OnLoginCheckFail);
        EventManager.AddListenerT(CommonDefine.EMEventType.DownloadSuccess, OnDownloadSuccess);
        EventManager.AddListenerT(CommonDefine.EMEventType.LoginBackButtonClicked, OnClickLoginBackButton);
        EventManager.AddListenerT(CommonDefine.EMEventType.LogoutButtonClicked, OnClickLogoutButton);
        EventManager.AddListenerT(CommonDefine.EMEventType.LogoutSuccess, OnLogoutSuccess);

        if (Application.isEditor)
            SwitchPanel(CommonDefine.EMPanelType.StartPanel);
    }

    private void SwitchPanel(CommonDefine.EMPanelType type)
    {
        m_StartPanel.gameObject.SetActive(type == CommonDefine.EMPanelType.StartPanel);
        m_LoginPanel.gameObject.SetActive(type == CommonDefine.EMPanelType.LoginPanel);
        m_DownloadPanel.gameObject.SetActive(type == CommonDefine.EMPanelType.DownloadPanel);
        m_LobbyPanel.gameObject.SetActive(type == CommonDefine.EMPanelType.LobbyPanel);
    }

    private void ShowPopup(CommonDefine.EMPopupType type)
    {
        m_LoginFailTips.gameObject.SetActive(type == CommonDefine.EMPopupType.LoginFailTips);
        m_LogoutPopup.gameObject.SetActive(type == CommonDefine.EMPopupType.LogputPopup);
    }

    private void OnPlatformCheckSuccess()
    {
        SwitchPanel(CommonDefine.EMPanelType.StartPanel);
    }

    private void OnLoginSuccess()
    {
        SwitchPanel(CommonDefine.EMPanelType.DownloadPanel);
    }

    private void OnLoginFail()
    {
        ShowPopup(CommonDefine.EMPopupType.LoginFailTips);
    }

    private void OnLoginCheckFail()
    {
        SwitchPanel(CommonDefine.EMPanelType.LoginPanel);
    }

    private void OnDownloadSuccess()
    {
        SwitchPanel(CommonDefine.EMPanelType.LobbyPanel);
    }

    private void OnLogoutSuccess()
    {
        SwitchPanel(CommonDefine.EMPanelType.StartPanel);
    }

    private void OnClickLoginBackButton()
    {
        SwitchPanel(CommonDefine.EMPanelType.StartPanel);
    }

    private void OnClickLogoutButton()
    {
        ShowPopup(CommonDefine.EMPopupType.LogputPopup);
    }
}