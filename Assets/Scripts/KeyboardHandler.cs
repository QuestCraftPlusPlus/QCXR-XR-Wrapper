using TMPro;
using UnityEngine;

public class KeyboardHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchBox;
    [SerializeField] private TMP_InputField ramSetter;
    [SerializeField] private GameObject keyboard;
    private static bool isShift;

    private void Update()
    {
        keyboard.SetActive(searchBox.IsActive() || ramSetter.IsActive());
    }
    
    public void KeyPress(string key)
    {
        if (searchBox.IsActive())
        {
            searchBox.text += isShift ? key.ToUpper() : key.ToLower();
        }
        else if (ramSetter.IsActive())
        {
            ramSetter.text += isShift ? key.ToUpper() : key.ToLower();
        }
    }

    public void BackspacePress()
    {
        if (searchBox.IsActive())
        {
            searchBox.text = searchBox.text.Remove(searchBox.text.Length - 1, 1);
        }
        else if (ramSetter.IsActive())
        {
            ramSetter.text = ramSetter.text.Remove(ramSetter.text.Length - 1, 1);
        }
    }
    
    public void ShiftPress()
    {
        isShift = !isShift;
    }

    public void SpacePress()
    {
        if (searchBox.IsActive())
        {
            searchBox.text += " ";
        }
        else if (ramSetter.IsActive())
        {
            ramSetter.text += " ";
        }
    }
}