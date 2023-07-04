using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour
{
    [SerializeField] private Image m_ImgAvatar;
    [SerializeField] private Button m_BtnLogout;
    [SerializeField] private Button m_BtnInstance;
    [SerializeField] private Button m_BtnStart;
    [SerializeField] private Button m_BtnMode;
    [SerializeField] private Text m_TxtInstanceName;
    
    private void Awake()
    {
        m_BtnLogout.onClick.AddListener(OnBtnLogoutClicked);
        m_BtnInstance.onClick.AddListener(OnBtnInstanceClicked);
        m_BtnStart.onClick.AddListener(OnBtnStartClicked);
        m_BtnMode.onClick.AddListener(OnBtnModeClicked);
    }
    
    private void OnEnable()
    {
        string avatarUrl = GameLogicManager.instance.loginHandler.GetProfileImgUrl();
    
        StartCoroutine(LoadAvatar(avatarUrl));
    
        m_TxtInstanceName.text = AndroidBridge.instance.GetCurrentInstance;
        
        Debug.Log("YVRCraft --> enter lobby");
    }
    
    private IEnumerator LoadAvatar(string url)
    {
        WWW www = new WWW(url);
    
        yield return www;
    
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height),
                new Vector2(0, 0));
            m_ImgAvatar.sprite = sprite;
        }
    }
    
    private void OnBtnLogoutClicked()
    {
        EventManager.BrocastT(CommonDefine.EMEventType.LogoutButtonClicked);
    }
    
    private void OnBtnInstanceClicked()
    {
        Debug.Log("YVRCraft --> on switch instance button clicked");
        m_TxtInstanceName.text = AndroidBridge.instance.SwitchInstance();
    }
    
    private void OnBtnStartClicked()
    {
        Debug.Log("YVRCraft --> on start game button clicked");
        EventManager.BrocastT(CommonDefine.EMEventType.StartGameButtonClicked);
    }
    
    private void OnBtnModeClicked()
    {
    }
}