using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HurtPlayerOnContact : MonoBehaviour
{
    public int damageToGive;
    private EnemyHealthManager enemyHealth;
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealthManager>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (enemyHealth.shocked) { }
            else
            {
                HealthManager.HurtPlayer(damageToGive);

                other.GetComponent<AudioSource>().Play();

                var player = other.GetComponent<PlayerController>();
                player.knockbackCount = player.knockbackLength;

                if (other.transform.position.x < transform.position.x) { player.knockFromRight = true; }
                else { player.knockFromRight = false; }
            }
        }
    }
}
