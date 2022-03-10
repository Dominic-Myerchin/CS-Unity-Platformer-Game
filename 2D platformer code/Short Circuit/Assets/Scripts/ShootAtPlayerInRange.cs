﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayerInRange : MonoBehaviour
{
    public bool alerted;

    public float playerRange;

    public GameObject enemyStar;

    public PlayerController player;

    public Transform launchPoint;

    public float waitBetweenShots;

    private float shotCounter;

    private Pathfinding.AIDestinationSetter DestinationSetter;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        shotCounter = waitBetweenShots;

        alerted = false;

        DestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        DestinationSetter.
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(new Vector3(transform.position.x - playerRange, transform.position.y, transform.position.z), new Vector3(transform.position.x + playerRange, transform.position.y, transform.position.z));
        
        if((player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange) || (player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange))
        {
            alerted = true;
        }
        else
        {
            alerted = false;
        }

        if(alerted)
        {
            DestinationSetter.gameObject.SetActive(true);
        }

        /*
        shotCounter -= Time.deltaTime;
        while (shotCounter < 0)
        {
            if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange)
            {
                Instantiate(enemyStar, launchPoint.position, launchPoint.rotation);
            }

            if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange)
            {
                Instantiate(enemyStar, launchPoint.position, launchPoint.rotation);
            }
            shotCounter = waitBetweenShots;
        }*/
    }
}
