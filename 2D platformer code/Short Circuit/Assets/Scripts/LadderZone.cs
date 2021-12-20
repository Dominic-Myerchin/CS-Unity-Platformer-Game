using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZone : MonoBehaviour
{
    private PlayerController theplayer;

    // Start is called before the first frame update
    void Start()
    {
        theplayer = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theplayer.onLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theplayer.onLadder = false;
        }
    }
}
    