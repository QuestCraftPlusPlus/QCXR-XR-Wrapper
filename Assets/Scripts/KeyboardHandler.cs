using System.Threading.Tasks;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchBox;
    [SerializeField] private TMP_InputField ramSetter;
    [SerializeField] private TMP_InputField instanceEditor;
    [SerializeField] private GameObject keyboard;
    public AudioSource keyPop;
    private static bool isShift;

    private void Update()
    {
        keyboard.SetActive(searchBox.IsActive() || ramSetter.IsActive() || instanceEditor.IsActive());
    }
    
    public void KeyPress(string key)
    {
        if (searchBox.IsActive())
        {
            searchBox.text += isShift ? key.ToUpper() : key.ToLower();
            keyPop.Play();
        }
        else if (ramSetter.IsActive())
        {
            ramSetter.text += isShift ? key.ToUpper() : key.ToLower();
            keyPop.Play();
        } else if (instanceEditor.IsActive())
        {
            instanceEditor.text += isShift ? key.ToUpper() : key.ToLower();
            keyPop.Play();
        }
    }

    public void BackspacePress()
    {
        if (searchBox.IsActive())
        {
            searchBox.text = searchBox.text.Remove(searchBox.text.Length - 1, 1);
            keyPop.Play();
        }
        else if (ramSetter.IsActive())
        {
            ramSetter.text = ramSetter.text.Remove(ramSetter.text.Length - 1, 1);
            keyPop.Play();
        } else if (instanceEditor.IsActive())
        {
            instanceEditor.text = instanceEditor.text.Remove(instanceEditor.text.Length - 1, 1);
            keyPop.Play();
        }
    }

    public void ShiftPress()
    {
        ShiftPressTask();
    }

    private async Task ShiftPressTask()
    {
        isShift = !isShift;
        // var keys = keyboard.GetNamedChild("Keys").GetChildGameObjects().GetComponentsInChildren<TMP_Text>();
        //
        // foreach (var key in keys)
        // {
        //     key.text = isShift ? key.text.ToLower() : key.text.ToUpper();
        // }
        
        keyPop.Play();
    }

    public void SpacePress()
    {
        if (searchBox.IsActive())
        {
            searchBox.text += " ";
            keyPop.Play();
        }
        else if (ramSetter.IsActive())
        {
            ramSetter.text += " ";
            keyPop.Play();
        } else if (instanceEditor.IsActive())
        {
            instanceEditor.text += " ";
            keyPop.Play();
        }
    }
}