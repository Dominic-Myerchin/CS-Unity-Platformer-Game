using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float sprintSpeed;
    private float moveVelocity;
    public float jumpHeight;

    //Variables for finding the ground
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    public bool dead;
    private bool doubleJumped;

    //player flipping vars
    private Animator anim;
    public LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

    //projectile variables
    public Transform firePoint;
    public GameObject ninjaStar;
    public float shotDelay;
    private float ShotDelayCounter;
    // Start is called before the first frame update

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        dead = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer) Debug.LogError("Can't Find the Sprite Renderer");
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        if (colliders.Length > 0) { grounded = true; }
        else {grounded = false;}
    }

    // Update is called once per frame
    void Update()
    {
        //RESPAWN KEY
        if (Input.GetKeyDown(KeyCode.R))
        {
            levelManager.RespawnPlayer();
        }
        //JUMP CODE
        if (grounded) 
        {
            doubleJumped = false;
        }

        anim.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !grounded && !doubleJumped)
        {
            Jump();
            doubleJumped = true;
        }

        //LEFT - RIGHT CONTROLS

        moveVelocity = 0f;
        if (Input.GetKey(KeyCode.D)){ moveVelocity = moveSpeed; }
        if (Input.GetKey(KeyCode.A)){ moveVelocity = -moveSpeed; }
        if (knockbackCount <= 0) { body.velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y); }
        else 
        {
            if(knockFromRight)
            {
                body.velocity = new Vector2(-knockback, knockback);
            }
            else
            {
                body.velocity = new Vector2(knockback, knockback);
            }
            knockbackCount -= Time.deltaTime;
        }
        

        //SHOOTING CODE
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            ShotDelayCounter = shotDelay;
        }
        if (Input.GetKey(KeyCode.Return))
        {
            ShotDelayCounter -= Time.deltaTime; //count down the time since the last frame, makles it count down for n seconds onwards

            if (ShotDelayCounter <= 0)
            {
                ShotDelayCounter = shotDelay;
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            }
        }
        // WALKING ANIMATION
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        
        //PLAYER FLIPPING
        if(GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        } 
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        

    }
    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

    public void moveHoriz(float moveSpeedVar)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeedVar, GetComponent<Rigidbody2D>().velocity.y);
    }

}
