﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeFirstAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerPlatformerController>().TakeDamage(5);
        }
    }
}
