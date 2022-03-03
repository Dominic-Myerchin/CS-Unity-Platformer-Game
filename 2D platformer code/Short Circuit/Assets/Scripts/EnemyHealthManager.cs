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
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            Destroy(gameObject);
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
        enemyBody.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetBool("Shocked", true);
        //Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        GetComponent<CircleCollider2D>().radius = 0;

        Debug.Log("Player Shocked");

        yield return new WaitForSeconds(shockTime);

        shocking = false;
        shocked = false;
        enemyBody.constraints = RigidbodyConstraints2D.None;
        enemyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim.SetBool("Shocked", false);
    }

    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
        GetComponent<AudioSource>().Play();
    }
}
