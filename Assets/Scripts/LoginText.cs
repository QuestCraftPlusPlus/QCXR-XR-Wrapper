using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginText : MonoBehaviour
{

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = JNIStorage.apiClass.GetStatic<string>("msaMessage");
        if(GetComponent<TextMeshProUGUI>().text != "")
        {
            GetComponent<TextMeshProUGUI>().text  = GetComponent<TextMeshProUGUI>().text + "请耐心等待至所有文件都完成下载后，再次点击“微软登录”按键以开始游戏。";
        }
    }
}
