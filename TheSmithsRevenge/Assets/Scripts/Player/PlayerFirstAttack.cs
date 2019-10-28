using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstAttack : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<TheFoe>().isDead = true;
            GameManagment.totalScore += collision.GetComponent<TheFoe>().score;
        }
    }
    public void Flip()
    {
        Vector3 x = transform.position;
        x.x *= -1f;
        transform.position = x;
    }
}
