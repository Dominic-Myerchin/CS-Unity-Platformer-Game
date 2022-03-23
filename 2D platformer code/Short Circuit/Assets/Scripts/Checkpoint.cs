using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public LevelManager levelManager;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelManager.currentCheckpoint.Equals(gameObject))
        {
            anim.SetBool("Activated", false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shockwave")
        {
            levelManager.currentCheckpoint = gameObject;
            Debug.Log("Activated Checkpoint " + transform.position);
            anim.SetBool("Activated", true);
        }
    }
}

