using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesterGameEnder : MonoBehaviour
{
    public Quester GameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.quest)
        {
            GameManager.Gameover();
        }
    }

}
