using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyboardHandler : MonoBehaviour
{
    private List<TMP_InputField> inputFields = new List<TMP_InputField>();
    private List<TMP_Text> keys = new List<TMP_Text>();
    [SerializeField] private GameObject keyboard;
    public AudioSource keyPop;
    private static bool isShift;

    private void Awake()
    {
        inputFields = FindObjectsByType<TMP_InputField>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        keys = keyboard.GetNamedChild("Keys").GetComponentsInChildren<TMP_Text>().ToList();
        keys.RemoveAll(obj => obj.text.Length > 1); // we only want letters
    }

    private void Update()
    {
        keyboard.SetActive(inputFields.Any(inputField => inputField.IsActive()));
    }
    
    public void KeyPress(string key)
    {
        TMP_InputField activeInputField = inputFields.Find(inputField => inputField.IsActive());
        if(activeInputField)
        {
            activeInputField.text += isShift ? key.ToUpper() : key.ToLower();
            keyPop.Play();
        }
    }

    public void BackspacePress()
    {
        TMP_InputField activeInputField = inputFields.Find(inputField => inputField.IsActive());
        if (activeInputField)
        {
            activeInputField.text = activeInputField.text.Remove(activeInputField.text.Length - 1, 1);
            keyPop.Play();
        }
    }

    public void ShiftPress()
    {
        isShift = !isShift;

        foreach (var key in keys)
            key.text = isShift ? key.text.ToUpper() : key.text.ToLower();

        keyPop.Play();
    }

    public void SpacePress()
    {
        TMP_InputField activeInputField = inputFields.Find(inputField => inputField.IsActive());
        if (activeInputField)
        {
            activeInputField.text += ' ';
            keyPop.Play();
        }
    }
}
