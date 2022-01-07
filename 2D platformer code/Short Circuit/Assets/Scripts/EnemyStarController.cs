using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStarController : MonoBehaviour
{
    public float speed;

    public PlayerController player;

    //public GameObject enemyDeathEffect;

    public GameObject impactEffect;

    //public int pointsForKill;

    public int damageToGive;

    public float rotationSpeed;

    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerController>();

        if (player.transform.position.x < transform.position.x)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector3(speed, body.velocity.y, 1);

        body.angularVelocity = rotationSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            //Destroy(other.gameObject);
            //ScoreManager.AddPoints(pointsForKill);
            HealthManager.HurtPlayer(damageToGive);
        }
        else
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
