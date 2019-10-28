using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBunnyTrigger : MonoBehaviour
{
    private int numTriggered = 0;
    public int score = 200;
    public int healthgained = 20;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            numTriggered += 1;
            if (numTriggered == 1)
            {
                animator.SetBool("playerMeeting", true);
            }
            if (numTriggered == 2)
            {
                animator.SetBool("playerPassed", true);
            }
            if (numTriggered == 3)
            {
                other.GetComponent<PlayerPlatformerController>().BunnyBoostStart();
                other.GetComponent<PlayerPlatformerController>().AddHealth(healthgained);
                GameManagment.totalScore += score;
                numTriggered = 2;
            }
        }
    }
}
