using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    private bool AtEdge;
    public Transform edgeCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, wallCheckRadius, whatIsWall);
        if (colliders.Length > 0) { hittingWall = true; }
        else { hittingWall = false; }

        colliders = Physics2D.OverlapCircleAll(edgeCheck.position, wallCheckRadius, whatIsWall);
        if (colliders.Length > 0) { AtEdge = false; }
        else
        { AtEdge = true; }
        

            if (hittingWall || AtEdge)
        {
            moveRight = !moveRight;
        }
        if (moveRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else 
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        
    }
}
