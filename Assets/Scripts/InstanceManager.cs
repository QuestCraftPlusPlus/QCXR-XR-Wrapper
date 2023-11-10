using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InstanceManager : MonoBehaviour
{
    [SerializeField] private GameObject modArray;
    
	private string currModSlug;
    public AndroidJavaObject[] MinecraftVersion;
    public TMP_InputField instanceName;
    public GameObject instanceButton;

    public void CreateInstances()
    {
        ResetArray();
    }

    public void NewInstance()
    {
        JNIStorage.apiClass.CallStatic("createNewInstance", JNIStorage.activity, instanceName, JNIStorage.home);
    }

    private void ResetArray()
    {
        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(modArray.transform.GetChild(i).gameObject);
        }
    }
}
