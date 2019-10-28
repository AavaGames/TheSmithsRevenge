using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxMoveSpeed = 5f;
    public float jumpTakeOffSpeed = 10f;
    public float peakAirTime = 2f;
    private bool falling = false;
    private float tempTimer = 0f;

    private int currentlyJumping = -1;
    private bool canJump = false;
    private bool canAttack = false;
    private bool attacking = false;
    public bool isDead = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject playerCamera;
    public GameObject firstAttack;
    public GameObject pausedTMP;

    public int maxHealth = 20;
    public int health = 0;

    private bool invincible = false;
    private bool paused = false;
    private float currentTimeScale = 1f;
    AudioSource deathSound;
    AudioSource damageTakenSound;
    AudioSource attackSound;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = maxHealth;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        deathSound = audioSources[0];
        damageTakenSound = audioSources[1];
        attackSound = audioSources[2];

    }

    protected override void ComputeVelocity()
    {
        if (health <= 0)
        {
            Death();
        }
        Vector2 move = Vector2.zero;

        move.x = xDirection;

        //Add extra if, if you add double jump
        if (canJump == true)
        {
            //Initial Thrust upwards
            if(Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
                //Start Jump Mechanism
                currentlyJumping = 1;
            }
            else if(Input.GetButtonUp("Jump"))
            {
                currentlyJumping = 0;
                tempTimer = 0;
            }

            if (currentlyJumping == 1)
            {
                //Once peak jump is reached y movement pauses
                if(velocity.y < 0)
                {
                    stopYMovement = true;
                }

                //Breaks out of pause after timer or you let go of Jump button (Space)
                tempTimer += Time.deltaTime;
                if (tempTimer > peakAirTime)
                {
                    currentlyJumping = 0;
                    tempTimer = 0;
                }
            }
            else if (currentlyJumping == 0)
            {
                stopYMovement = false;
                if(velocity.y > 0)
                {
                    velocity.y *= 0.5f;
                }
                currentlyJumping = -1;
            }
        }
        if (canAttack == true)
        {
            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                attacking = true;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
            if (paused)
            {
                if(Input.GetKeyDown(KeyCode.R))
                {
                    Pause();
                    Death();
                }
            }
        }

        //DevTools();

        DirFacing(move);

        falling = velocity.y < 0 ? true : false;

        animator.SetBool("grounded", grounded);
        animator.SetBool("falling", falling);
        animator.SetBool("attacking", attacking);
        animator.SetBool("isDead", isDead);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
        animator.SetFloat("velocityY", Mathf.Abs(velocity.y));

        targetVelocity = move * maxMoveSpeed;
    }

    public void Activate()
    {
        isMoving = true;
        StartCoroutine(ActivateTwo());
    }

    private IEnumerator ActivateTwo()
    {
        yield return new WaitForSeconds(0.5f);
        canJump = true;
        canAttack = true;
    }
    public void SwitchDirection()
    {
        if(playerCamera.GetComponent<followPlayer>().done == true)
        {
            movingRight = !movingRight;
            if (movingRight == true)
            {
                playerCamera.GetComponent<followPlayer>().AimDirection(true);
            }
            else if (movingRight == false)
            {
                playerCamera.GetComponent<followPlayer>().AimDirection(false);
            }
        }
    }
    private void DevTools()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            SwitchDirection();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1f;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 0.5f;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 1.5f;
        }
    }

    private void DirFacing(Vector3 move)
    {
        //Flip facing?
        if (move.x < -0.01f)
        {
            Vector3 x = transform.localScale;
            x.x = -1f;
            transform.localScale = x;
        }
        else if (move.x > 0.01f)
        {
            Vector3 x = transform.localScale;
            x.x = 1f;
            transform.localScale = x;
        }
    }

    private IEnumerator Invincibility()
    {
        invincible = true;

        DecreaseOpacity();
        yield return new WaitForSeconds(0.4f);
        ResetOpacity();
        yield return new WaitForSeconds(0.1f);
        DecreaseOpacity();
        yield return new WaitForSeconds(0.4f);
        ResetOpacity();
        yield return new WaitForSeconds(0.1f);
        DecreaseOpacity();
        yield return new WaitForSeconds(0.5f);
        ResetOpacity();

        invincible = false;
    }

    private void DecreaseOpacity()
    {
        Color temp = gameObject.GetComponent<SpriteRenderer>().color;
        temp.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = temp;
    }

    private void ResetOpacity()
    {
        Color temp = gameObject.GetComponent<SpriteRenderer>().color;
        temp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = temp;
    }

    public void TakeDamage(int damagetaken)
    {
        if (invincible == false)
        {
            damageTakenSound.Play();
            health -= damagetaken;
            StartCoroutine(Invincibility());
        }
    }

    public void BunnyBoostStart()
    {
        StartCoroutine(BunnyBoost());
    }
    private IEnumerator BunnyBoost()
    {
        //Play SFX
        health = maxHealth;

        float temp = maxMoveSpeed;
        maxMoveSpeed = 15f;

        yield return new WaitForSeconds(1.5f);

        maxMoveSpeed = temp;
    }

    public void ActivateFirstAttack()
    {
        attackSound.Play();
        firstAttack.SetActive(true);
    }
    public void DeactivateFirstAttack()
    {
        firstAttack.SetActive(false);
    }
    public void AttackEnd()
    {
        attacking = false;
    }
    public void AddHealth(int healthadded)
    {
        health += healthadded;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Death()
    {
        if (!isDead)
        {
            isDead = true;
            canJump = false;
            isMoving = false;
            deathSound.Play();
        }
    }

    private void Pause()
    {
        paused = !paused;
        if (paused == true)
        {
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            pausedTMP.SetActive(true);
        }
        else if (paused == false)
        {
            Time.timeScale = currentTimeScale;
            pausedTMP.SetActive(false);
        }
    }
}

