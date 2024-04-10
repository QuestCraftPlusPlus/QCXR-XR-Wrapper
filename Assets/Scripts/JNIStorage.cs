using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JNIStorage : MonoBehaviour
{
    public static AndroidJavaClass apiClass;
    public static AndroidJavaObject accountObj;
    public static AndroidJavaObject activity;
    public static AndroidJavaObject[] instances;
    public TMP_InputField RAMSetterField;
    [SerializeField, FormerlySerializedAs("DevToggle")]
    private Toggle _devToggle;
    public static string home;

    private void Start()
    {
        Microphone.Start(Microphone.devices[0], true, 1, 44100);
        apiClass = new AndroidJavaClass("pojlib.api.API_V1");
        AndroidJavaClass constants = new AndroidJavaClass("pojlib.util.Constants");
        home = constants.GetStatic<string>("MC_DIR");
        apiClass.SetStatic("developerMods", _devToggle.isOn);
        UpdateInstances();
		apiClass.SetStatic("model", OpenXRFeatureSystemInfo.GetHeadsetName());
    }

    public static void UpdateInstances()
    {
        AndroidJavaClass instance = new AndroidJavaClass("pojlib.instance.MinecraftInstance");
        instances = apiClass.CallStatic<AndroidJavaObject[]>("getQCSupportedVersions");
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public void SetMemoryValue()
    {
        if (RAMSetterField.text != null)
        {
            apiClass.SetStatic("memoryValue", RAMSetterField.text);
        }
    }
}