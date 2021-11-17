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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        cameraa.isFollowing = false;
        //gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
        //player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        ScoreManager.AddPoints(-pointsPenaltyOnDeath);
        Debug.Log("Player Respawn");
        yield return new WaitForSeconds(respawnTime);
        //player.GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        cameraa.isFollowing = true;
        player.GetComponent<Renderer>().enabled = true;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
