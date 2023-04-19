using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyParser : MonoBehaviour
{
    public void GetKey()
    {
        string key = gameObject.name;
        KeyboardHandler.KeyPress(key);
    }
}