using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public PlayerController player;


    public int startingLives;
    private int lifeCounter;

    private Text theText;

    public string mainMenu;

    public float waitAfterGameOver;
    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<Text>();

        lifeCounter = startingLives;

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter < 0)
        {
            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }
        theText.text = "x" + lifeCounter;

        if(gameOverScreen.activeSelf)
        {
            waitAfterGameOver -= Time.deltaTime;
        }

        if(waitAfterGameOver < 0)
        {
            SceneManager.LoadScene(mainMenu);
        }
    }

    public void giveLife()
    {
        lifeCounter++;
    }

    public void takeLife()
    {
        lifeCounter--;
    }
}
