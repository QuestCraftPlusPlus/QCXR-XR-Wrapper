using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyParser : MonoBehaviour
{
    public KeyboardHandler keyboardHandler;
    
    public void GetKey()
    {
        string key = gameObject.name;
        keyboardHandler.KeyPress(key);
    }
}