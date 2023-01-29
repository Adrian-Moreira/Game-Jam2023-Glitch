using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public static GameOver instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void openGameOver()
    {
        gameObject.SetActive(true);
    }


    public void closeGameOver()
    {
        gameObject.SetActive(false);
    }

    public void quitGame()
    {
        TitleScreen.instance.resetTitle();
    }


    public void restartGame()
    {
        PlayerStats.instance.resetHP();

        GameController.instance.startEndless();

        closeGameOver();
    }
}
