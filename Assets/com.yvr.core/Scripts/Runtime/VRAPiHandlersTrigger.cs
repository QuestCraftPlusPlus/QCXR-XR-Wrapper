using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YVR.Core
{
    public class VRAPiHandlersTrigger : MonoBehaviour
    {
        [DllImport("yvrplugin")]
        private static extern void yvrStartRecording();

        [DllImport("yvrplugin")]
        private static extern void yvrStopRecording();

        [DllImport("yvrplugin")]
        private static extern void yvrStartReplay(string filePath);

        [DllImport("yvrplugin")]
        private static extern void yvrStopReplay();

        private void Start()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            string handler = intent.Call<string>("getStringExtra", "vrApiHandler");
            if (handler == "replay")
            {
                string recordPath = intent.Call<string>("getStringExtra", "record");
                yvrStartReplay(recordPath);
            }
            else if (handler == "record")
            {
                int recordingTime = 3;
                recordingTime = intent.Call<int>("getIntExtra", new object[] {"time", recordingTime});
                StartCoroutine(StartRecording(recordingTime));
            }
        }

        private IEnumerator StartRecording(int recordingTime)
        {
            yvrStartRecording();
            yield return new WaitForSecondsRealtime(recordingTime);
            yvrStopRecording();
            Application.Quit();
        }
    }
}