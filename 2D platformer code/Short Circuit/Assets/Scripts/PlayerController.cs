using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float sprintSpeed;
    private float moveVelocity;
    public float jumpHeight;
    public bool sliding;
    private float slideVelocity; 
    
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

    //knockback vars
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private Rigidbody2D body;

    //Ladder Variables
    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;
    private float gravityStore;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        dead = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer) Debug.LogError("Can't Find the Sprite Renderer");
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        gravityStore = body.gravityScale;
        onLadder = false;
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

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        if (Input.GetButtonDown("Jump") && !grounded && !doubleJumped)
        {
            Jump();
            doubleJumped = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            sliding = true;
            anim.SetBool("sliding", true);
            moveSpeed = 15;
        }
        else
        {
            sliding = false;
            anim.SetBool("sliding", false);
            moveSpeed = 10;
        }

        //LEFT - RIGHT CONTROLS
        //moveVelocity = 0f;
        moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");
        if (knockbackCount <= 0) { body.velocity = new Vector2(moveVelocity, body.velocity.y); }
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
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            ShotDelayCounter = shotDelay;
        }
        if (Input.GetButton("Fire1"))
        {
            ShotDelayCounter -= Time.deltaTime; //count down the time since the last frame, makles it count down for n seconds onwards

            if (ShotDelayCounter <= 0)
            {
                ShotDelayCounter = shotDelay;
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            }
        }

        //SWORD CODE
        if (anim.GetBool("Sword"))
                {
                    anim.SetBool("Sword", false);
                }
                if (Input.GetButton("Fire2"))
                {
                    anim.SetBool("Sword", true);

                }
        if (anim.GetBool("Shockwave"))
        {
            anim.SetBool("Shockwave", false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("Shockwave", true);

        }

        // WALKING ANIMATION
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        
        //PLAYER FLIPPING
        if(GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } 
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        //LADDER CODE
        if (onLadder)
        {
            body.gravityScale = 0f;

            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");

            body.velocity = new Vector2(body.velocity.x, climbVelocity);
        }

        if(!onLadder)
        {
            body.gravityScale = gravityStore;
        }
        
    }
    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }
    public void freezePosition()
    {
        body.constraints = RigidbodyConstraints2D.FreezePositionX;
        body.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public void unfreezePosition()
    {
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
