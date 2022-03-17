using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlert : MonoBehaviour
{
    public bool alerted;

    public float playerRange;

    public PlayerController player;

    private Pathfinding.AIDestinationSetter DestinationSetter;

    private EnemyPatrol enemyPatrol;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        alerted = false;

        enemyPatrol = GetComponent<EnemyPatrol>();
        DestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        DestinationSetter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange) 
            || (player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange))
        {
            alerted = true;
        }
        else
        {
            alerted = false;
        }

        if (alerted)
        {
            enemyPatrol.enabled = false;
            DestinationSetter.enabled = true;
        }
        else
        {
            enemyPatrol.enabled = true;
            DestinationSetter.enabled = false;
        }
    }
}
