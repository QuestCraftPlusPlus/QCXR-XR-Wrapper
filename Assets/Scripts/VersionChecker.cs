using System;
using TMPro;
using UnityEngine;

public class VersionChecker : MonoBehaviour
{
    public string currentPublicVersion;
    public TextMeshProUGUI updateText;
    public TextMeshProUGUI version;
    string currentVersion;

    [ContextMenu("Check Version")]
    public void Start()
    {
        currentVersion = Application.version;
        version.text = currentVersion;
    }

    public void CheckVersion()
    {
        Debug.Log($"Comparing Current version: {currentVersion} with Latest: {currentPublicVersion}");

        if (!Version.TryParse(currentVersion, out var unused1) ||
            !Version.TryParse(currentPublicVersion, out var unused2))
        {
            Debug.Log("Couldn't parse one or both versions, assuming latest.");
            return;
        }
        
        if (Version.Parse(currentVersion) >= Version.Parse(currentPublicVersion))
        {
            Debug.Log("Up to date.");
            return;
        }
        
        Debug.Log("QuestCraft update available.");
        updateText.text += " <color=#D70A53>| Update Available!</color>";
    }
}