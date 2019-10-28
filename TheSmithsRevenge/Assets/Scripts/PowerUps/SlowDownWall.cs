using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownWall : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D other) {
        triggered = true;
    }

    private void Update() {
        if(triggered == true)
        {
            Time.timeScale -= Time.deltaTime;
            if (Time.timeScale <= 0.5f)
            {
                triggered = false;
                Time.timeScale = 0.5f;
            }
        }
    }
}
