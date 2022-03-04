using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int enemyHealth;

    public GameObject deathEffect;

    public int pointsOnDeath;

    public bool shocked;

    public bool shocking;

    public float shockTime;

    private Rigidbody2D enemyBody;

    private Transform Player;

    private Pathfinding.AIPath AIpath;

    
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        shocking = false;
        anim = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody2D>();
        shocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
        }

        if (shocked)
        {
            //enemyBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            //anim.SetBool("Shocked", true);
            //enemyBody.Sleep();
            ShockEnemy();

        }
    }

    public void ShockEnemy()
    {
        if (shocking == true) { }
        else
        {
            StartCoroutine("ShockEnemyCo");
        }
    }

    public IEnumerator ShockEnemyCo()
    {
        shocking = true;
        if (enemyBody.Equals(null))
        {
            AIpath = GetComponent<Pathfinding.AIPath>();
            AIpath.canMove = false;
        }
        else
        {
            enemyBody.constraints = RigidbodyConstraints2D.FreezeAll;
            enemyBody.gravityScale = 0;
            anim.SetBool("Shocked", true);
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        //Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        

        Debug.Log("Player Shocked");

        yield return new WaitForSeconds(shockTime);

        shocking = false;
        shocked = false;
        if (enemyBody.Equals(null))
        {
            AIpath.canMove = true;
        }
        else
        {
            enemyBody.constraints = RigidbodyConstraints2D.None;
            enemyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            enemyBody.gravityScale = 1;
            GetComponent<CircleCollider2D>().isTrigger = false;
            anim.SetBool("Shocked", false);
        }
    }

    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
        GetComponent<AudioSource>().Play();
    }
}
