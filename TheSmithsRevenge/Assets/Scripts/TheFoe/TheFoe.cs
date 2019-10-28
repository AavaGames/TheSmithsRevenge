using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFoe : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameObject playerTrigger;
    public GameObject firstAttack;
    public GameObject secondAttack;


    private GameObject trackedPlayer = null;

    private bool attacking = false;
    public bool isDead = false;

    public int score = 50;
    public bool forest = true;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (isDead == true)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
        else
        {
            if (trackedPlayer != null)
            {
                if (trackedPlayer.transform.position.x < transform.position.x)
                {
                    Vector3 x = transform.localScale;
                    //if in the forest or spaxe
                    if (forest)
                    {
                        x.x = -1f;
                    }
                    else
                    {
                        x.x = 1f;
                    }

                    transform.localScale = x;
                }
                else if (trackedPlayer.transform.position.x > transform.position.x)
                {
                    Vector3 x = transform.localScale;
                    if (forest)
                    {
                        x.x = 1f;
                    }
                    else
                    {
                        x.x = -1f;
                    }
                    transform.localScale = x;
                }
            }
        }

        animator.SetBool("attacking", attacking);
        animator.SetBool("isDead", isDead);
    }

    public void PlayerEntered(GameObject player)
    {
        attacking = true;
        trackedPlayer = player;
    }

    public void ActivateFirstAttack()
    {
        firstAttack.SetActive(true);
    }
    public void DeactivateFirstAttack()
    {
        firstAttack.SetActive(false);
    }
    public void ActivateSecondAttack()
    {
        secondAttack.SetActive(true);
    }
    public void DeactivateSecondAttack()
    {
        secondAttack.SetActive(false);
    }
    public void AttackEnd()
    {
        attacking = false;
    }
}
