using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject player;


    public int startingLives;
    private int lifeCounter;

    private Text theText;
    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<Text>();

        lifeCounter = startingLives;

        player = FindObjectOfType<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter < 0)
        {
            gameOverScreen.SetActive(true);
        }
        theText.text = "x" + lifeCounter;
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
