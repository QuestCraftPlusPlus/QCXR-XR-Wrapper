using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using YVR.Platform;

public class PlatformTestBtnWithInput : PlatformTestBtn
{
    public Button requestBtn;
    private static long s_APPID;
    private static Queue<string> s_logQueue = new Queue<string>();
    private void Start()
    {
        InitInputList();
        requestBtn.onClick.AddListener(OnButtonClicked);
    }

    private List<ParamInputComponent> inputList = new List<ParamInputComponent>();

    private void InitInputList()
    {
        inputList.AddRange(transform.GetComponentsInChildren<ParamInputComponent>());
    }

    public override void OnButtonClicked()
    {
        switch (btnType)
        {
            case BtnType.Default:
                break;
            case BtnType.AchievementAddCount:
                AchievementAddCount();
                break;
            case BtnType.AchievementAddFields:
                AchievementAddFields();
                break;
            case BtnType.UnlockAchievement:
                UnlockAchievement();
                break;
            case BtnType.GetDefinitionByName:
                GetDefinitionByName();
                break;
            case BtnType.GetProgressByName:
                GetProgressByName();
                break;
            case BtnType.Initialize:
                Initialize();
                break;
            case BtnType.GetFriendInfo:
                GetFriendInfo();
                break;
            case BtnType.GetLeaderboardByPage:
                GetLeaderboardInfoByPage();
                break;
            case BtnType.GetLeaderboardByRank:
                GetLeaderboardInfoByRank();
                break;
            case BtnType.AddRankItem:
                LeaderboardWriteItem();
                break;
            case BtnType.SetDebug:
                SetDebug();
                break;
            case BtnType.IsYVRUserLogin:
                IsYVRUserLogin();
                break;
            case BtnType.LoginUserInfo:
                LoginUserInfo();
                break;
            case BtnType.GetGoodsList:
                GetGoodsList();
                break;
            case BtnType.Pay:
                Pay();
                break;
            case BtnType.GetOrders:
                GetOrders();
                break;
            case BtnType.None:
                break;
            default:
                break;
        }
    }

    private void Initialize()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        s_APPID = long.Parse(inputList[0].GetParamData());

        YVR.Platform.YVRPlatform.Initialize(s_APPID);
        LoginPay.Initialize(s_APPID);
    }

    private void Update()
    {
        if (s_logQueue.Count>0)
        {
            Debug.Log(s_logQueue.Dequeue());
        }
    }

    private void AchievementAddCount()
    {
        if (inputList.Count < 2)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string name = inputList[0].GetParamData();
        int count = int.Parse(inputList[1].GetParamData());

        YVR.Platform.Achievement.AchievementAddCount(name, count).OnComplete(AchievementAddCountCallback);
    }
    private void AchievementAddCountCallback(YVR.Platform.YVRMessage msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("Achievement add count callback");
    }

    private void AchievementAddFields()
    {
        if (inputList.Count < 2)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string name = inputList[0].GetParamData();
        string field = inputList[1].GetParamData();

        YVR.Platform.Achievement.AchievementAddFields(name, field).OnComplete(AchievementAddFieldsCallback);
    }
    private void AchievementAddFieldsCallback(YVR.Platform.YVRMessage msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("Achievement add fields callback");
    }

    private void UnlockAchievement()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string name = inputList[0].GetParamData();

        YVR.Platform.Achievement.UnlockAchievement(name).OnComplete(UnlockAchievementCallback);
    }
    private void UnlockAchievementCallback(YVR.Platform.YVRMessage msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("Unlock achievement callback");
    }

    private void GetDefinitionByName()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string[] names = inputList[0].GetParamData().Split(';');

        YVR.Platform.Achievement.GetDefinitionByName(names).OnComplete(GetDefinitionByNameCallback);
    }
    private void GetDefinitionByNameCallback(YVR.Platform.YVRMessage<YVR.Platform.AchievementDefinitionList> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("DefinitionByName : " + msg.data.Count);
    }

    private void GetProgressByName()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string[] names = inputList[0].GetParamData().Split(';');

        YVR.Platform.Achievement.GetProgressByName(names).OnComplete(GetProgressByNameCallback);
    }
    private void GetProgressByNameCallback(YVR.Platform.YVRMessage<YVR.Platform.AchievementProgressList> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("ProgressByName : " + msg.data.Count);
    }

    private void GetFriendInfo()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        int accountID = int.Parse(inputList[0].GetParamData());

        YVR.Platform.Friends.GetFriendInfomation(accountID).OnComplete(GetFriendInfoCallback);
    }
    private void GetFriendInfoCallback(YVR.Platform.YVRMessage<YVR.Platform.FriendInfo> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.LogFormat("account ID : {0} \nuser name : {1} \nuser age : {2} \nuser sex : {3}\nuser icon : {4} \nuser online state : {5}", msg.data.accountID, msg.data.nickname, msg.data.age, msg.data.gender, msg.data.avatar, msg.data.onlineState);
    }

    private void GetLeaderboardInfoByPage()
    {
        if (inputList.Count < 4)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        LeaderboardByPage leaderboardByPage = new LeaderboardByPage();
        leaderboardByPage.current = string.IsNullOrEmpty(inputList[0].GetParamData()) ? 0 :long.Parse(inputList[0].GetParamData());
        leaderboardByPage.size = string.IsNullOrEmpty(inputList[1].GetParamData()) ? 0 : long.Parse(inputList[1].GetParamData());
        leaderboardByPage.leaderboardApiName = inputList[2].GetParamData();
        leaderboardByPage.pageType = (LeaderboardPageType)Enum.Parse(typeof(LeaderboardPageType), inputList[3].GetParamData());

        Leaderboard.GetLeaderboardInfoByPage(leaderboardByPage).OnComplete(GetLeaderboardInfoCallback);
    }

    private void GetLeaderboardInfoByRank()
    {
        if (inputList.Count < 5)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        LeaderboardByRank leaderboardByRank = new LeaderboardByRank();
        leaderboardByRank.currentStart = string.IsNullOrEmpty(inputList[0].GetParamData()) ? 0 : long.Parse(inputList[0].GetParamData());
        leaderboardByRank.dataDirection = (LeaderboardDataDirection)Enum.Parse(typeof(LeaderboardDataDirection), inputList[1].GetParamData());
        leaderboardByRank.size = string.IsNullOrEmpty(inputList[2].GetParamData()) ? 0 : long.Parse(inputList[2].GetParamData());
        leaderboardByRank.leaderboardApiName = inputList[3].GetParamData();
        leaderboardByRank.pageType = (LeaderboardPageType)Enum.Parse(typeof(LeaderboardPageType), inputList[4].GetParamData());

        Leaderboard.GetLeaderboardInfoByRank(leaderboardByRank).OnComplete(GetLeaderboardInfoCallback);
    }

    private void GetLeaderboardInfoCallback(YVRMessage<LeaderboardInfo> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("current msg is error");
            return;
        }

        Debug.Log(msg.data.ToString());
    }

    private void LeaderboardWriteItem()
    {
        if (inputList.Count < 4)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        LeaderboardEntry rankInfo = new LeaderboardEntry();
        rankInfo.leaderboardApiName = inputList[0].GetParamData();
        rankInfo.score = string.IsNullOrEmpty(inputList[1].GetParamData()) ? 0 : double.Parse(inputList[1].GetParamData());
        rankInfo.extraData = string.IsNullOrEmpty(inputList[2].GetParamData()) ? new sbyte[] { } : Array.ConvertAll(Encoding.Default.GetBytes(inputList[2].GetParamData()), q => Convert.ToSByte(q));
        rankInfo.extraDataLength = rankInfo.extraData.Length;
        rankInfo.forceUpdate = (LeaderboardUpdatePolicy)Enum.Parse(typeof(LeaderboardUpdatePolicy), inputList[3].GetParamData());

        Leaderboard.LeaderboardWriteItem(rankInfo).OnComplete(LeaderboardWriteItemCallback);
    }

    private void LeaderboardWriteItemCallback(YVRMessage msg)
    {
        if (msg.isError)
        {
            Debug.LogError("current msg is error");
            return;
        }

        Debug.Log(msg.ToString());
    }

    private void SetDebug()
    {
        LoginPay.LoginPaySetDebug();
    }

    private void IsYVRUserLogin()
    {
        bool loginResult = LoginPay.IsYVRUserLogin(s_APPID);
        Debug.Log($"loginResult:{loginResult}");
    }

    private void LoginUserInfo()
    {
        LoginPay.GetLogInUserInfo(s_APPID, LoginUserInfoCallback);
    }

    private void LoginUserInfoCallback(string userInfo)
    {
        s_logQueue.Enqueue(userInfo);
    }

    private void GetGoodsList()
    {
        LoginPay.GetGoodsList(GetGoodsCallback);
    }

    private void GetGoodsCallback(GoodsListResponse goods)
    {
        if (goods.errCode!=0 || string.IsNullOrEmpty(goods.errMsg))
        {
            s_logQueue.Enqueue($"Get goods list error code:{goods.errCode},error message:{goods.errMsg}");
        }

        foreach (var item in goods.goodsList.content)
        {
            s_logQueue.Enqueue(item.ToString());
        }
    }

    private void Pay()
    {
        string sku = inputList[0].GetParamData();
        int count = string.IsNullOrEmpty(inputList[1].GetParamData()) ? 0 : int.Parse(inputList[1].GetParamData());
        string cpOrderNo = inputList[2].GetParamData();

        LoginPay.Pay(sku, count, cpOrderNo, PayCallback);
    }

    private void PayCallback(PayResponse payResult)
    {
        if (payResult.errCode != 0 || string.IsNullOrEmpty(payResult.errMsg))
        {
            s_logQueue.Enqueue($"Pay result error code:{payResult.errCode},error message:{payResult.errMsg}");
            return;
        }

        s_logQueue.Enqueue(payResult.payContent.payResult.ToString());
    }

    private void GetOrders()
    {
        string sku = inputList[0].GetParamData();
        LoginPay.GetOrders(sku, GetOrdersCallback);
    }

    private void GetOrdersCallback(OrdersListResponse orderResult)
    {
        if (orderResult.errCode != 0 || string.IsNullOrEmpty(orderResult.errMsg))
        {
            s_logQueue.Enqueue($"Get orders list error code:{orderResult.errCode},error message:{orderResult.errMsg}");
            return;
        }

        foreach (var item in orderResult.orderList.content)
        {
            s_logQueue.Enqueue(item.ToString());
        }
    }

}
