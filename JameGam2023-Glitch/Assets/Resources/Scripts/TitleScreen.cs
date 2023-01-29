using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{

    //[SerializeField] private AudioClip bell;

    [SerializeField] private CanvasGroup titleLeft, titleRight, titleBack;

    private bool animate = false;
    private bool sound = true;

    private bool part1, part2;
    private float delay1, delay2;

    public void animateTitle()
    {
        animate = true;
        delay1 = 1;
        delay2 = 1;
        part1 = true;
        part2 = true;
    }

    private void Update()
    {
        if(animate)
        {
            if(sound)
            {
                //AudioManager.instance.PlaySFX(bell);
                sound = false;
            }
            if(part1 && titleLeft.alpha < 1)
            {
                titleLeft.alpha += Time.deltaTime;
                if(titleLeft.alpha >= 1)
                {
                    titleLeft.alpha = 1;
                    part1 = false;
                }
            }
            else if(delay1 > 0)
            {
                delay1 -= Time.deltaTime;
            }
            else if(part2 && titleRight.alpha < 1)
            {
                titleRight.alpha += Time.deltaTime;
                if(titleRight.alpha >= 1)
                {
                    titleRight.alpha = 1;
                    part2 = false;
                    titleBack.alpha = 0;
                }
            }
            else if(delay2 > 0)
            {
                delay2 -= Time.deltaTime;
            }
            else if(titleLeft.alpha > 0)
            {
                titleLeft.alpha -= Time.deltaTime;
                titleRight.alpha -= Time.deltaTime;
            }
            else
            {
                animate = false;
            }
        }
    }

}
