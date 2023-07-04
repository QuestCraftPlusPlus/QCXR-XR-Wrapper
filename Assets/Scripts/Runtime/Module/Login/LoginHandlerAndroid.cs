using UnityEngine;

public class LoginHandlerAndroid : LoginHandler
{
    public override string GetLoginText()
    {
        return AndroidBridge.instance.GetLoginText;
    }

    public override string GetProfileImgUrl()
    {
        return AndroidBridge.instance.GetProfileImgUrl;
    }

    public override bool Logout()
    {
        bool logoOutSucc = AndroidBridge.instance.Logout();
        Debug.Log($"YVRCraft --> logout result: {logoOutSucc}");
        return logoOutSucc;
    }

    protected override bool Login()
    {
        bool result = AndroidBridge.instance.Login();

        Debug.Log($"YVRCraft --> login result: {result}");

        return result;
    }
}
