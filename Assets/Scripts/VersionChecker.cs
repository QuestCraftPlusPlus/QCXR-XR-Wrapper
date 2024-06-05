using System;
using TMPro;
using UnityEngine;

public class VersionChecker : MonoBehaviour
{
    public string currentPublicVersion;
    public TextMeshProUGUI updateText;

    [ContextMenu("Check Version")]
    public void CheckVersion()
    {
        string currentVersion = Application.version;
        Debug.Log($"Comparing Current version: {currentVersion} with Latest:{currentPublicVersion}");
        if (Version.Parse(currentVersion) >= Version.Parse(currentPublicVersion))
        {
            Debug.Log("Up to date.");
            return;
        }
        
        Debug.Log("QuestCraft update available.");
        updateText.text += " <color=#D70A53>| Update Available!</color>";
    }
}