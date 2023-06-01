using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyParser : MonoBehaviour
{
    public KeyboardHandler keyboardHandler;
    
    public void GetKey()
    {
        GameObject wack = GameObject.Find(EventSystem.current.currentSelectedGameObject.name);
        string key = wack.name;
        keyboardHandler.KeyPress(key);
    }
}