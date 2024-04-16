
using System;
using UnityEngine;

public class ScoreAdder : MonoBehaviour
{
    public Quester GameManager;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == GameManager.quest)
        {
            GameManager.score++;
        }
    }
}
