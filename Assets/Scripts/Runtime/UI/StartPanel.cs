using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button m_BtnStart;

    private void Start()
    {
        m_BtnStart.onClick.AddListener(OnBtnStartClicked);
    }
    
    private void OnBtnStartClicked()
    {
        Debug.Log("YVRCraft --> on click start button");
        EventManager.BrocastT(CommonDefine.EMEventType.StartButtonClicked);
    }
}
