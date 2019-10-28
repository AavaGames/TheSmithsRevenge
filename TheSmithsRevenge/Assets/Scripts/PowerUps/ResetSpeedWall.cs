using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSpeedWall : MonoBehaviour
{
    private bool triggered = false;
    private int timeFastOrSlow= -1;
    private void OnTriggerEnter2D(Collider2D other) {
        triggered = true;
        if (Time.timeScale == 1.5f)
        {
            timeFastOrSlow = 0;
        }
        else if (Time.timeScale == 0.5f)
        {
            timeFastOrSlow = 1;
        }
    }

    private void Update() {
        if(triggered == true)
        {
            if (timeFastOrSlow == 0)
            {
                Time.timeScale -= Time.deltaTime;
                if (Time.timeScale <= 1f)
                {
                    triggered = false;
                    Time.timeScale = 1f;
                }
            }
            else if (timeFastOrSlow == 1)
            {
                Time.timeScale += Time.deltaTime;
                if (Time.timeScale >= 1f)
                {
                    triggered = false;
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
