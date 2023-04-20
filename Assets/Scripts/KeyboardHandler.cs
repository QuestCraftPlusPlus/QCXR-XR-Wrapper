using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardHandler : MonoBehaviour
{
    public TMP_InputField searchbox;
    public TMP_InputField ramSetter;
    public GameObject keyboard;
    public static bool isShift;

    void Update()
    {
        if (searchbox.IsActive())
        {
            keyboard.SetActive(true);
        } else if (ramSetter.IsActive())
        {
            keyboard.SetActive(true);
        }
        else
        {
            keyboard.SetActive(false);
        }
    }
    
    public void KeyPress(string key)
    {
        if (searchbox.IsActive())
        {
            if (isShift)
            {
                searchbox.text += key.ToUpper();
            }
            else
            {
                searchbox.text += key.ToLower();
            }
        }
        else if (ramSetter.IsActive())
        {
            if (isShift)
            {
                ramSetter.text += key.ToUpper();
            }
            else
            {
                ramSetter.text += key.ToLower();
            }
        }
    }

    public void BackspacePress()
    {
        if (searchbox.IsActive())
        {
            searchbox.text = searchbox.text.Remove(searchbox.text.Length - 1, 1);
        } else if (ramSetter.IsActive())
        {
            ramSetter.text = ramSetter.text.Remove(ramSetter.text.Length - 1, 1);
        }
    }
    
    public void ShiftPress()
    {
        if (isShift)
        {
            isShift = false;
        }
        else
        {
            isShift = true;
        }
    }

    public void SpacePress()
    {
        if (searchbox.IsActive())
        {
            searchbox.text += " ";
        } else if (ramSetter.IsActive())
        {
            ramSetter.text += " ";
        }
    }
}