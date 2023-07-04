using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YVR.Core
{
    public class VRApiHandlersTrigger : MonoBehaviour
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
                int recordingTime = 3;
                recordingTime = intent.Call<int>("getIntExtra", new object[] {"time", recordingTime});

                StartCoroutine(StartReplay(recordingTime, recordPath));
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


        private IEnumerator StartReplay(int replayTime, string replayPath)
        {
            while (true)
            {
                yvrStartReplay(replayPath);
                yield return new WaitForSecondsRealtime(replayTime);
            }
        }
    }
}