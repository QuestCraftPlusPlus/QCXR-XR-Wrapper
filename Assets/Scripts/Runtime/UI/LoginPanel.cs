using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] private Text m_TxtLogin;
    [SerializeField] private Button m_BtnBack;
    [SerializeField] private Button m_BtnNext;

    private void Awake()
    {
        EventManager.AddListenerT<string>(CommonDefine.EMEventType.LoginTextChanged, OnLoginTextChanged);

        m_BtnBack.onClick.AddListener(OnBtnBackClicked);
        m_BtnNext.onClick.AddListener(OnBtnNextClicked);
    }

    private void OnEnable()
    {
        m_TxtLogin.text = GameLogicManager.instance.loginHandler.GetLoginText();
    }

    private void OnBtnBackClicked()
    {
        Debug.Log("YVRCraft --> on click login back button");
        
        EventManager.BrocastT(CommonDefine.EMEventType.LoginBackButtonClicked);
    }

    private void OnBtnNextClicked()
    {
        Debug.Log("YVRCraft --> on click login button");
        
        EventManager.BrocastT(CommonDefine.EMEventType.LoginButtonClicked);
    }

    private void OnLoginTextChanged(string text)
    {
        m_TxtLogin.text = text;
    }
}