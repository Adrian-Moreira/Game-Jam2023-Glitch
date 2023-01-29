using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitches : MonoBehaviour
{
    public Camera mainCamera;

    public Sprite missing;
    public Material missingMat;
    public Material groundMat;

    public MeshRenderer ground;

    public AudioClip whiteNoise;

    [HideInInspector]
    public bool textureGlitching = false;
    public bool isUpsideDown = false;
    public bool volumeGlitching = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            textureGlitch();

        if (Input.GetKeyDown(KeyCode.U))
            upsideDown();

        if (Input.GetKeyDown(KeyCode.C))
            TransitionController.instance.changeEyeState();

        if (Input.GetKeyDown(KeyCode.V))
            playLouderAudio();

    }


    public void textureGlitch()
    {
        if (!textureGlitching)
        {
            List<Transform> events = EventManager.instance.children;

            foreach (Transform child in events)
            {
                child.GetComponent<Event>().setSprite(missing);
                child.GetComponent<Event>().turnOnCapsule();
            }

            ground.material = missingMat;
            textureGlitching = true;
        }
        else
        {
            List<Transform> events = EventManager.instance.children;

            foreach (Transform child in events)
            {
                child.GetComponent<Event>().resetSprite();
                child.GetComponent<Event>().turnOffCapsule();
            }

            ground.material = groundMat;
            textureGlitching = false;
        }
    }

    public void upsideDown()
    {
        float offset = 180;
        Quaternion rotation = mainCamera.transform.rotation;

        if (isUpsideDown)
            offset = 0;

        mainCamera.transform.rotation = new Quaternion(rotation.x, rotation.y, offset, rotation.w);

        isUpsideDown = !isUpsideDown;
    }


    private void changeVolume()
    {
        if (!volumeGlitching)
            EventManager.instance.volume = 0.1f;
        else
            EventManager.instance.volume = 0.5f;
    }


    public void playLouderAudio()
    {
        if (!volumeGlitching)
            AudioManager.instance.PlaySong(whiteNoise);
        else
            AudioManager.instance.PlaySong(null);

        changeVolume();

        volumeGlitching = !volumeGlitching;
    }
}
