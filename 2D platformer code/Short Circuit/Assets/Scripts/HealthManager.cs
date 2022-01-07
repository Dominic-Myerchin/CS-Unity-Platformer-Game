using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxPlayerHealth;
    private static int playerHealth;

    Text text1;
    private LevelManager levelManager;


    public bool isDead;

    private TimeManager theTime;
    // Start is called before the first frame update
    void Start()
    {
        text1 = GetComponent<Text>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        isDead = false;

        theTime = FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            theTime.ResetTime();
        }

        text1.text = "" + playerHealth;
    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

    public void KillPlayer()
    {
        playerHealth = 0;
    }
}
