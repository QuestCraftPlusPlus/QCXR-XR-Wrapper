using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginText : MonoBehaviour
{

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = JNIStorage.apiClass.GetStatic<string>("msaMessage").Replace("To sign in, use a web browser to open the page", "请在手机或电脑上登录网址：").Replace("and enter the code", "并输入代码").Replace("to authenticate.","  以验证账号。");
        if(GetComponent<TextMeshProUGUI>().text != "")
        {
            GetComponent<TextMeshProUGUI>().text  = GetComponent<TextMeshProUGUI>().text + "请耐心等待至所有文件都完成下载后，再次点击“微软登录”按键以开始游戏。";
        }
    }
}
