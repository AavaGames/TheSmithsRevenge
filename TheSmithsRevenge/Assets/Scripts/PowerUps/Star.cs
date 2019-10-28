using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int score = 50;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            GameManagment.totalScore += score;
            DestroySelf();
        }
    }
    private void DestroySelf()
    {
        //SFX
        Destroy(gameObject);
    }
}
