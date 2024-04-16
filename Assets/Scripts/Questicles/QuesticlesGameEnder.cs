using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesticlesGameEnder : MonoBehaviour
{
    public Questicles GameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.questicle)
        {
            GameManager.Gameover();
        }
    }

}
