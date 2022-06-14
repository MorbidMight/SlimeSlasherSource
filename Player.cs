using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Health")]
    [SerializeField] int startingHealth;
    public int health;
    private bool isRed = false;

    [Header("Movement")]
    public float speed;
    bool facingRight = true;

    [Header("Dashing")]
    public float dashForce;

    [Header("Jumping")]
    public float jumpForce;
    public float postInitialJumpForce;
    bool grounded = false;
    public int numberOfJumps;
    public int jumpsLeft;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Sword")]
    [SerializeField] GameObject swordPrefab;
    [SerializeField] GameObject gun;
    [SerializeField] float swordCooldown;
    private GameObject sword;
    private Sword swordComponent;
    public float swordBounce;
    private bool hasThrownSword = false;
    private bool swordNinetyNeg = false;
    private bool swordTimerStarted = false;

    Animator animator;
    AudioManager aManager;

    void Start()
    {
        //set health and jumps
        health = startingHealth;
        jumpsLeft = numberOfJumps;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        aManager = FindObjectOfType<AudioManager>();
    }


    void FixedUpdate()
    {
        //run move if the player isn't in death animation
        if(animator.GetBool("isDead") != true)
            Move();
    }
    void Update()
    {
        //run player methods if not in death animation
        if(animator.GetBool("isDead") != true)
        {
            CheckHealth();
            Jump();
            BetterJump();
            Dash();
            CheckSwordThrow();
        }
    }

    void CheckHealth()
    {
        //if player is in damage animation start the redden coroutine
        if (isRed == true)
        {
            StartCoroutine("redden");
        }
        //check if the player has died
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }
    void CheckSwordThrow()
    {
        //if player has pressed the shoot button throw sword
        if (Input.GetButtonDown("Fire1") && !hasThrownSword && Time.timeScale != 0)//the time.timeScale line is to ensure that the sword doesn't instantiate when the game is paused
        {
            Vector3 mousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            var instantiationAngle = Utilities.getAngle(mousePos, gameObject.transform.position) - 90;
            //Instantiation angle is the angle in which the sword will be instantiated

            //if the player attempts to throw the sword in a different direction to which they are facing flip them
            if (mousePos.x < gameObject.transform.position.x)
            {
                if (facingRight)
                {
                    flip();
                }
            } else
            {
                if (!facingRight)
                {
                    flip();
                }
            }
            Physics2D.IgnoreLayerCollision(9, 10, true);
            if (facingRight)
            {
                sword = Instantiate(swordPrefab, gun.transform.position, Quaternion.Euler(0, 0, instantiationAngle));
                swordNinetyNeg = false;
            }
            else
            {
                sword = Instantiate(swordPrefab, gun.transform.position, Quaternion.Euler(0, 0, instantiationAngle));
                swordNinetyNeg = true;
            }
            aManager.playThrow();
            hasThrownSword = true;
        }
        if(Input.GetButtonDown("Fire2") && sword != null)
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
            swordComponent = FindObjectOfType<Sword>();
            swordComponent.GetComponent<Animator>().SetBool("ReturningToPlayer", true);
        }
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x < 0 && facingRight == true)
        {
            flip();
        }
        if (x > 0 && facingRight == false)
        {
            flip();
        }
        float moveBy = x * speed * Time.fixedDeltaTime;
        aManager.playMove();
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        facingRight = !facingRight;
    }

    void Jump()
    {
        if (animator.GetBool("isJumping") == true)
        {
            animator.SetBool("isJumping", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            animator.SetBool("isJumping", true);
            if (jumpsLeft < numberOfJumps)
            {
                rb.velocity = new Vector2(rb.velocity.x, postInitialJumpForce);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            grounded = false;
            jumpsLeft--;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    void BetterJump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(dashForce, rb.velocity.y);
        }
    }

    //Coroutine to keep player red
    IEnumerator redden()
    {

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        isRed = false;
    }


    /*void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            grounded = true;
            Debug.Log("Grounded!");
            jumpsLeft = numberOfJumps;
        }
        else
        {
            grounded = false;  
        }
    }*/

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            jumpsLeft = numberOfJumps;
            grounded = true;
        }
        if(collision.gameObject.layer == 9)
        {
                animator.SetBool("isJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, swordBounce);
                jumpsLeft = numberOfJumps;
                grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("loseCollider"))
        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            health = 0;
        }

        if (collision.gameObject.layer == 12)
        {
            aManager.playHurt();
            health = health - 1;
            isRed = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag.Equals("MeleeEnemy"))
        {
            var enemy = collision.gameObject.transform.parent.gameObject.GetComponent<MeleeEnemy>();
            Destroy(collision.gameObject.GetComponent<CapsuleCollider2D>());
            enemy.Die();  
            health = health - 1;
            isRed = true;
        }
        if (collision.gameObject.tag.Equals("Sign"))
        {
            collision.gameObject.GetComponent<Sign>().Activate();
        }
        if(collision.gameObject.tag.Equals("Fountain"))
        {
            collision.gameObject.GetComponent<Fountain>().changeState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Sign"))
        {
            collision.gameObject.GetComponent<Sign>().Deactivate();
        }
    }
    public void ResetSwordVariables()
    {
       if(swordTimerStarted == false)
       {
            StartCoroutine("SwordTimer");
       }
    }

    IEnumerator SwordTimer()
    {
        swordTimerStarted = true;
        yield return new WaitForSeconds(swordCooldown);
        hasThrownSword = false;
        swordTimerStarted = false;
    }

    public bool checkObjectOnScreen(GameObject obj, float deadSpace)
    {
        Vector3 screenPoint = FindObjectOfType<Camera>().WorldToViewportPoint(obj.transform.position);
        if ((screenPoint.x > 0 - deadSpace && screenPoint.x < 1 + deadSpace && screenPoint.y > 0 - deadSpace && screenPoint.y < 1 + deadSpace))
        {
            return true;
        }
        return false;
    }

    public bool getThrownSword() {return hasThrownSword;}
    public bool getSwordNinetyNeg() {return swordNinetyNeg;}
    public int getHealth() {return health;}
    public void setHealth(int h) {health = h;}
}
