using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformTestBtn : MonoBehaviour
{
    public Button btn;
    public BtnType btnType;

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            OnButtonClicked();
        });
    }

    public virtual void OnButtonClicked()
    {
        switch (btnType)
        {
            case BtnType.Default:
                break;
            case BtnType.GetLoggedInUser:
                GetLoggedInUser();
                break;
            case BtnType.GetAllDefinitions:
                GetAllDefinitions();
                break;
            case BtnType.GetAllProgress:
                GetAllProgress();
                break;
            case BtnType.GetViewerEntitled:
                GetViewerEntitled();
                break;
            case BtnType.IsDeeplinkLaunch:
                IsDeeplinkLaunch();
                break;
            case BtnType.GetDeeplinkRoomId:
                GetDeeplinkRoomId();
                break;
            case BtnType.GetDeeplinkApiName:
                GetDeeplinkApiName();
                break;
            case BtnType.GetFriends:
                GetFriends();
                break;
            case BtnType.None:
                break;
            default:
                break;
        }
    }

    private void GetLoggedInUser()
    {
        YVR.Platform.Account.GetLoggedInUser().OnComplete(GetLoggedInUserCallback);
    }
    private void GetLoggedInUserCallback(YVR.Platform.YVRMessage<YVR.Platform.AccountData> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.LogFormat("account ID : {0} \nuser name : {1} \nuser icon : {2} \nuser sex : {3}", msg.data.accountID, msg.data.nickname, msg.data.avatar, msg.data.gender);
    }

    private void GetAllDefinitions()
    {
        YVR.Platform.Achievement.GetAllDefinitions().OnComplete(GetAllDefinitionsCallback);
    }
    private void GetAllDefinitionsCallback(YVR.Platform.YVRMessage<YVR.Platform.AchievementDefinitionList> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("AllDefinitions : " + msg.data.Count);
    }

    private void GetAllProgress()
    {
        YVR.Platform.Achievement.GetAllProgress().OnComplete(GetAllProgressCallback);
    }
    private void GetAllProgressCallback(YVR.Platform.YVRMessage<YVR.Platform.AchievementProgressList> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("AllProgress : " + msg.data.Count);
    }

    #region Entitlement
    private void GetViewerEntitled()
    {
        YVR.Platform.PlatformCore.GetViewerEntitled().OnComplete(GetLoggedInUserCallback);
    }
    private void GetLoggedInUserCallback(YVR.Platform.YVRMessage<YVR.Platform.Entitlement> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("is entitled ? " + msg.data.isEntitled);
    }
    #endregion

    private void IsDeeplinkLaunch()
    {
        bool result = YVR.Platform.Deeplink.IsDeeplinkLaunch();

        Debug.Log("is deeplink launch ? " + result);
    }

    private void GetDeeplinkRoomId()
    {
        string result = YVR.Platform.Deeplink.GetDeeplinkRoomId();

        Debug.Log("Deeplink room id: " + result);
    }

    private void GetDeeplinkApiName()
    {
        string result = YVR.Platform.Deeplink.GetDeeplinkApiName();

        Debug.Log("Deeplink api name: " + result);
    }

    private void GetFriends()
    {
        YVR.Platform.Friends.GetFriends().OnComplete(GetFriendsCallback);
    }
    private void GetFriendsCallback(YVR.Platform.YVRMessage<YVR.Platform.FriendsList> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("Friends : " + msg.data.Count);
    }

    public enum BtnType
    {
        Default = 0,
        GetLoggedInUser = 1,
        AchievementAddCount = 2,
        AchievementAddFields = 3,
        UnlockAchievement = 4,
        GetAllDefinitions = 5,
        GetDefinitionByName = 6,
        GetAllProgress = 7,
        GetProgressByName = 8,
        Initialize = 9,
        GetViewerEntitled = 10,
        IsDeeplinkLaunch = 11,
        GetDeeplinkRoomId = 12,
        GetDeeplinkApiName = 13,
        GetFriends = 14,
        GetFriendInfo = 15,
        GetLeaderboardByPage =16,
        GetLeaderboardByRank =17,
        AddRankItem = 18,
        SetDebug = 19,
        IsYVRUserLogin = 20,
        LoginUserInfo = 21,
        GetGoodsList = 22,
        Pay = 23,
        GetOrders = 24,
        None = 999,
    }
}
