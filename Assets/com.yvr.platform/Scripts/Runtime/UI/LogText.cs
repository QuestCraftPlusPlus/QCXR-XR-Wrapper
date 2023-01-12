using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
    public bool needStackTrace = false;

    public Toggle togStackTrace;

    private static LogText instance;
    public static LogText Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<LogText>();

            return instance;
        }
    }

    private void Awake()
    {
        togStackTrace.onValueChanged.AddListener(OnTogChanged);

        Application.logMessageReceived += SetLogText;
    }

    private void OnTogChanged(bool value)
    {
        needStackTrace = value;
    }

    public void SetLogText(string condition, string stackTrace, LogType type)
    {
        if (text == null)
            return;

        string colorText = "";

        switch (type)
        {
            case LogType.Error:
                colorText = "<color=#FF0000FF>{0}</color>";
                break;
            case LogType.Assert:
                colorText = "<color=#FFFFFFFF>{0}</color>";
                break;
            case LogType.Warning:
                colorText = "<color=#FFEE00FF>{0}</color>";
                break;
            case LogType.Log:
                colorText = "<color=#FFFFFFFF>{0}</color>";
                break;
            case LogType.Exception:
                colorText = "<color=#BC00A8FF>{0}</color>";
                break;
            default:
                break;
        }
        if (text.text.Length>30000)
        {
            text.text = "";
        }
        if (needStackTrace)
            text.text += string.Format(colorText, "\n\n" + string.Format("{0}\n{1}",condition, stackTrace));
        else
            text.text += string.Format(colorText, "\n" + condition);

        text.GetComponent<ContentSizeFitter>().SetLayoutVertical();
    }

    private Text text;

    private void Start()
    {
        this.text = gameObject.GetComponent<Text>();
    }
}
