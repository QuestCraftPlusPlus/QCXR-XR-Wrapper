using System;
using System.Collections.Generic;
using UnityEngine;
using YVR.Core.XR;

namespace YVR.Core
{
    public class YVREventTracking : Singleton<YVREventTracking>
    {
        private AndroidJavaClass m_UnityPlayerClass;
        private AndroidJavaObject m_CurrentActivity;
        private AndroidJavaClass m_NativeCoreSdkClass;
        private string m_SdkVersion;
        private string m_SdkType;
        private long m_AppId;

        public AndroidJavaObject CurrentActivity
        {
            get
            {
                if (m_UnityPlayerClass == null)
                {
                    m_UnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                }

                if (m_CurrentActivity == null)
                {
                    m_CurrentActivity = m_UnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
                }

                return m_CurrentActivity;
            }
        }

        public AndroidJavaClass NativeCoreSdkClass
        {
            get
            {
                if (m_NativeCoreSdkClass == null)
                {
                    m_NativeCoreSdkClass = new AndroidJavaClass("com.yvr.nativecoresdk.SdkUtils");
                }

                return m_NativeCoreSdkClass;
            }
        }

        public void UploadSDKInfo()
        {
            if (YVRPlatformSetting.Instance != null)
            {
                long.TryParse(YVRPlatformSetting.Instance.appID, out m_AppId);
            }

            SetSDKInfo("com.yvr.core");
            SetSDKInfo("com.yvr.utilities");
            SetSDKInfo("com.yvr.platform");
        }

        private void InitSdk(string sdkType, string sdkVersion, long appId)
        {
            string engine = "unity";
            NativeCoreSdkClass.CallStatic("initSdk", CurrentActivity, engine, sdkType, sdkVersion, appId);
        }

        private void SetSDKInfo(string m_SdkType)
        {
            bool result = PackageVersion.TryGetPackageVersion(m_SdkType, out m_SdkVersion);
            if (result) InitSdk(m_SdkType, m_SdkVersion, m_AppId);
        }

    }
}