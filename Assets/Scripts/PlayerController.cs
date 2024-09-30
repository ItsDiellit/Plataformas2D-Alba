using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;

    public static Animator characterAnimator;

    private float horizontalInput;

    [SerializeField]private float characterSpeed = 4.5f;
    [SerializeField]private float jumpForce = 10;
    
    [SerializeField]private int healthPoints = 5;

    private bool isAttacking;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
    }

    void Update()
    {
        Movement();
      
        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1") && GroundSensor.isGrounded && !isAttacking)
        {
            Attack();
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(isAttacking)
        {
             characterRigidbody.velocity = new Vector2(0, characterRigidbody.velocity.y);

        }
        else
        {
            characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
        }

    }
    
    
    
     void Movement()
    {
          horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < 0)
        {
            if(!isAttacking)
            {
                 transform.rotation = Quaternion.Euler(0, 180, 0);
            }
           
            characterAnimator.SetBool("IsRunning", true);
        }

        else if(horizontalInput > 0)
        {
            if(!isAttacking)
            {
                 transform.rotation = Quaternion.Euler(0, 0, 0);
            }
           
            characterAnimator.SetBool("IsRunning", true);
        }

        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }
       
    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        characterAnimator.SetBool("IsJumping", true);

    }

    void Attack()
    {
        StartCoroutine(AttackAnimation());
        characterAnimator.SetTrigger("Attack");

    }

    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.3f);

        isAttacking = false;
    }

    void TakeDamage()
    {
        healthPoints--;
       
        if(healthPoints <= 0)
        {
            Die();
        }
        else
        {
             characterAnimator.SetTrigger("IsHurt");

        }
    }

    void Die()
        {
            characterAnimator.SetTrigger("IsDead");
            Destroy(gameObject, 0.45f);

        }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.layer == 8)
         {
            TakeDamage();
         }
    }

   
}
