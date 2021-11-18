using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;

    private PlayerController player;

    public GameObject deathParticle;
    public GameObject respawnParticle;

    public int pointsPenaltyOnDeath;

    public float respawnTime;

    public CameraController cameraa;
    private float gravityStore;

    public HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        if (player.dead == true) { }
        else
        {
            StartCoroutine("RespawnPlayerCo");
        }
    }

    public IEnumerator RespawnPlayerCo()
    {
        player.dead = true;
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        cameraa.isFollowing = false;
        ScoreManager.AddPoints(-pointsPenaltyOnDeath);
        Debug.Log("Player Respawn");
        yield return new WaitForSeconds(respawnTime);
        player.transform.position = currentCheckpoint.transform.position;
        player.dead = false;
        player.enabled = true;
        cameraa.isFollowing = true;
        player.GetComponent<Renderer>().enabled = true;
        healthManager.FullHealth();
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
