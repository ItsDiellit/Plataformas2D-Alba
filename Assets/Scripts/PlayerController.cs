using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;

    public static Animator characterAnimator;

    private float horizontalInput;

    [SerializeField]private float characterSpeed = 4.5f;
    [SerializeField]private float jumpForce = 10;
    
 public int _currentHealth {get; private set;} = 5;
public int _maxHealth {get; private set;} = 5;

    

    private bool isAttacking;
   

    [SerializeField] private Transform attackHitBox;
    [SerializeField] private float attackRadius = 1;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
        _currentHealth = _maxHealth;

        GameManager.instance.SetHealthBar(_maxHealth);
    }

    void Update()
    {
        Movement();
      
        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
            Jump();
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource ,SoundManager.instance.jumpAudio);
        }

        if(Input.GetButtonDown("Fire1") && GroundSensor.isGrounded && !isAttacking)
        {
           //Attack();
           StartAttack();
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource ,SoundManager.instance.AttackAudio);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            
                SoundManager.instance.PlaySFX(SoundManager.instance._audioSource ,SoundManager.instance.PauseAudio);
                 GameManager.instance.Pause();


        }

        


    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(isAttacking)
        {
            if(horizontalInput == 0)
            {
             characterRigidbody.velocity = new Vector2(0, characterRigidbody.velocity.y);

            }

        }
        else
        {
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource ,SoundManager.instance.StepsAudio);
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
    

    /*void Attack()
    {
         if(horizontalInput == 0)
        {

         characterAnimator.SetTrigger("Attack");

        }
        else
        
        characterAnimator.SetTrigger("Attack2");
        StartCoroutine(AttackAnimation());
       

        
    }
    

    IEnumerator AttackAnimation()
    {
        isAttacking = true;

       
        yield return new WaitForSeconds(0.2f);



         Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                 
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                Enemies EnemyScript = enemy.GetComponent<Enemies>();
                EnemyScript.EnemyTakeDamage();
            }
        }

      

        yield return new WaitForSeconds(0.3f);

        isAttacking = false;
    }*/


    void StartAttack()
    {
        isAttacking = true;
        characterAnimator.SetTrigger("Attack");
    }

    void Attack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                 
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                Enemies EnemyScript = enemy.GetComponent<Enemies>();
                EnemyScript.EnemyTakeDamage();
            }
        }
    }

    void EndAttack()
    {
        isAttacking = false;
    }


    void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        GameManager.instance.UpdateHealthBar(_currentHealth);
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource ,SoundManager.instance.HurtAudio);
        if(_currentHealth <= 0)
        {
            Die();

        }
        else
        {
             characterAnimator.SetTrigger("IsHurt");

        }
    }

    public  void Health(int AddHealth)
    {
        _currentHealth += AddHealth;
        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        GameManager.instance.UpdateHealthBar(_currentHealth);
            
    }

    void Die()
        {
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource ,SoundManager.instance.DeadAudio);
            characterAnimator.SetTrigger("IsDead");
            BGMManager.instance.PlayBGM(BGMManager.instance.GameOver);
            Destroy(gameObject, 0.45f);

            SceneManager.LoadScene("Game Over");

        }


    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.layer == 8)
         {
            TakeDamage(1);
         }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }

   
}
