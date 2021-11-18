using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarController : MonoBehaviour
{
    public float speed;

    public PlayerController player;

    public GameObject enemyDeathEffect;

    public GameObject impactEffect;

    public int pointsForKill;

    public int damageToGive; 

    public float rotationSpeed;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
       GetComponent<Rigidbody2D>().velocity = new Vector3(speed, GetComponent<Rigidbody2D>().velocity.y, 1);

        GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            //Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            //Destroy(other.gameObject);
            //ScoreManager.AddPoints(pointsForKill);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
