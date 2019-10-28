using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTrigger : MonoBehaviour
{
    public int damageDealt = 5;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPlatformerController>().TakeDamage(damageDealt);
        }
    }
}
