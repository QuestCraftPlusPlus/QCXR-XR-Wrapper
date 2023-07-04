using UnityEngine;
using UnityEngine.UI;

public class LoginHandlerEditor : LoginHandler
{
    public override string GetLoginText()
    {
        return "请在手机或电脑上登录网址：https://www.microsoft.com/link 并输入代码 ABCDEFGH 以验证账号。";
    }

    public override string GetProfileImgUrl()
    {
        return
            "https://logincdn.msauth.net/shared/1.0/content/images/microsoft_logo_ee5c8d9fb6248c938fd0dc19370e90bd.svg";
    }

    public override bool Logout()
    {
        return true;
    }

    protected override bool Login()
    {
        bool result = Random.Range(0, 2) == 1;

        Debug.Log($"YVRCraft --> fake login result: {result}");

        return result;
    }
}