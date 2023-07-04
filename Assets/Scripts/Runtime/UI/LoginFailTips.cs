using System;
using UnityEngine;

public class LoginFailTips : MonoBehaviour
{
    private void OnEnable()
    {
        Loom.QueueOnMainThread(Hide, 2f);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}