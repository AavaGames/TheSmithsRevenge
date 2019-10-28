using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int healthgained = 10;
    public int score = 50;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPlatformerController>().AddHealth(healthgained);
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
