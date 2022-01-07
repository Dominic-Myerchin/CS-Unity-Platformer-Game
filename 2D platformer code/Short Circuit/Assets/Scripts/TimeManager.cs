using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float startingTime;

    private PauseMenu thePauseMenu;

    private Text theText;

    public GameObject gameOverScreen;

    //public PlayerController player;

    private float countingTime;

    private HealthManager theHealth;

    // Start is called before the first frame update
    void Start()
    {
        countingTime = startingTime;
        theText = GetComponent<Text>();

        thePauseMenu = FindObjectOfType<PauseMenu>();

        theHealth = FindObjectOfType<HealthManager>();

        //player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePauseMenu.isPaused)
            return;

        countingTime -= Time.deltaTime;

        if (countingTime <= 0)
        {
            //gameOverScreen.SetActive(true);
            //player.gameObject.SetActive(false);
            //this wont work lololol
            theHealth.KillPlayer();
        }
        theText.text = "" + Mathf.Round(countingTime);
    }

    public void ResetTime()
    {
        countingTime = startingTime;
    }
}
