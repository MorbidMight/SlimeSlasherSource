using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Vector3 moveDirection;
    [SerializeField] float speed;
    public float returnSpeed;
    public float rotateSpeed = 200f;
    Player playerComp;
    public bool returning = false;
    Rigidbody2D rb;
    Renderer s_renderer;
    public float deadSpace = 0.2f;
    Animator animator;
    AudioManager aManager;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerComp = FindObjectOfType<Player>();
        s_renderer = GetComponent<Renderer>();
        animator = gameObject.GetComponent<Animator>();
        aManager = FindObjectOfType<AudioManager>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = (mousePos - transform.position);
        moveDirection.x = moveDirection.x * speed;
        moveDirection.y = moveDirection.y * speed;
        moveDirection.Normalize();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb != null)
        {
            rb.velocity = new Vector3(moveDirection.x * speed, moveDirection.y * speed);
        }
    }

    public void Update()
    {
        if (playerComp.checkObjectOnScreen(gameObject, deadSpace) == false)
        {
            destroySword();
        }
    }

    public void SetReturning(bool x)
    {
        returning = x;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            //rb.velocity = Vector3.zero;

            Destroy(rb);
            if(playerComp.getSwordNinetyNeg())
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90);
            }
            
            Physics2D.IgnoreLayerCollision(9, 10, false);      
        }
        else if(collision.gameObject.layer == 11)
        {
            if (collision.gameObject.tag.Equals("ShooterEnemy"))
            {
                collision.gameObject.GetComponent<ShooterEnemy>().Die();
            }
            Destroy(rb);
            GetComponent<Animator>().SetBool("ReturningToPlayer", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("MeleeEnemy"))
        {
            var enemy = collision.gameObject.transform.parent.gameObject.GetComponent<MeleeEnemy>();
            Destroy(collision.gameObject.GetComponent<CapsuleCollider2D>());
            enemy.Die();
        }
    }
    public void destroySword()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Destroy(gameObject);
        playerComp.ResetSwordVariables();
        aManager.playReturn();
    }

    public void ignoreSwordPlayerCollision()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
    }






}
