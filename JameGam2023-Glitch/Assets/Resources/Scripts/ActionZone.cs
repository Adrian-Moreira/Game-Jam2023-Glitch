using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : MonoBehaviour
{
    public static ActionZone instance;

    public AudioClip zoneEntered;

    private void Awake()
    {
        instance = this;
    }

    public void enteredZone(GameObject inZone)
    {
        AudioManager.instance.PlaySFX(zoneEntered, 0.25f);
    }
}
