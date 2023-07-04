using UnityEngine;

public class LoginHandler
{
    public LoginHandler()
    {
        EventManager.AddListenerT(CommonDefine.EMEventType.StartButtonClicked, OnClickStartButton);
        EventManager.AddListenerT(CommonDefine.EMEventType.LoginButtonClicked, OnClickLoginButton);
        EventManager.AddListenerT(CommonDefine.EMEventType.LogoutConfirmButtonClicked, OnClickLogoutConfirmButton);
    }

    public virtual string GetLoginText()
    {
        return "This is login text";
    }

    public virtual string GetProfileImgUrl()
    {
        return string.Empty;
    }

    public virtual bool Logout()
    {
        return false;
    }

    protected virtual bool Login()
    {
        return false;
    }

    protected virtual void OnClickStartButton()
    {
        EventManager.BrocastT(Login()
            ? CommonDefine.EMEventType.LoginSuccess
            : CommonDefine.EMEventType.LoginCheckFail);
    }

    protected virtual void OnClickLoginButton()
    {
        EventManager.BrocastT(Login() ? CommonDefine.EMEventType.LoginSuccess : CommonDefine.EMEventType.LoginFail);
    }

    protected virtual void OnClickLogoutConfirmButton()
    {
        if (Logout())
            EventManager.BrocastT(CommonDefine.EMEventType.LogoutSuccess);
    }
}