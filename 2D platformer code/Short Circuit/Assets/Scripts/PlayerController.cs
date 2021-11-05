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

    private bool doubleJumped;

    private Animator anim;

    public LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
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

        if (Input.GetKey(KeyCode.D))
        {
            moveVelocity = moveSpeed;

                //moveHoriz(moveSpeed);
            
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVelocity = -moveSpeed;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed -= sprintSpeed;
                //moveHoriz(-moveSpeed);
            }
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

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
