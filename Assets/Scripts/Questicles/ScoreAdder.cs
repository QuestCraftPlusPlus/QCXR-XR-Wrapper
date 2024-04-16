
using System;
using UnityEngine;

public class ScoreAdder : MonoBehaviour
{
    public Questicles GameManager;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == GameManager.questicle)
        {
            GameManager.score++;
        }
    }
}
