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
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }


    public void restartGame()
    {
        PlayerStats.instance.resetHP();

        GameController.instance.startEndless();

        closeGameOver();
    }
}
