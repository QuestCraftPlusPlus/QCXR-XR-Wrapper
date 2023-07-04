using UnityEngine;
using UnityEngine.UI;

public class LogoutPopup : MonoBehaviour
{
    [SerializeField]
    private Button m_BtnConfirm;
    [SerializeField]
    private Button m_BtnCancel;

    private void Awake()
    {
        m_BtnConfirm.onClick.AddListener(OnBtnConfirmClicked);
        m_BtnCancel.onClick.AddListener(OnBtnCancelClicked);
    }

    private void OnBtnConfirmClicked()
    {
        gameObject.SetActive(false);   
        
        EventManager.BrocastT(CommonDefine.EMEventType.LogoutConfirmButtonClicked);
    }

    private void OnBtnCancelClicked()
    {
        gameObject.SetActive(false);   
    }
}