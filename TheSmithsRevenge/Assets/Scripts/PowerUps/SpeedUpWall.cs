using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpWall : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D other) {
        triggered = true;
    }

    private void Update() {
        if(triggered == true)
        {
            Time.timeScale += Time.deltaTime;
            if (Time.timeScale >= 1.5f)
            {
                triggered = false;
                Time.timeScale = 1.5f;
            }
        }
    }
}
