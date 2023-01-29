using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public Slider HPBar;


    private int HP = 5;

    private void Awake()
    {
        instance = this;
    }

    public void loseHP()
    {
        HP -= 1;

        HPBar.value = HP;

        if(HP <= 0)
        {
            GameController.instance.stopEndless();

            GameOver.instance.openGameOver();
        }
    }


    public void resetHP()
    {
        HP = 5;

        HPBar.value = HP;
    }
}
