using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int enemyHealth;

    public GameObject deathEffect;

    public int pointsOnDeath;

    private Rigidbody2D enemyBody;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
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
    }

    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
        GetComponent<AudioSource>().Play();
    }

    public void freeze()
    {
        enemyBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        enemyBody.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public void unfreeze()
    {
        enemyBody.constraints = RigidbodyConstraints2D.None;
        enemyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
