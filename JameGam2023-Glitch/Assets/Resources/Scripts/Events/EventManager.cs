using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public Image splat;
    public AudioClip splatSFX;

    public List<Transform> children;

    public float volume = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        children = new List<Transform>();
        int childrenCount = transform.childCount;

        for (int i = 0; i < childrenCount; i++)
        {
            Transform child = transform.GetChild(i);

            children.Add(child);
        }

        if (playSfx == null)
            playSfx = StartCoroutine(_playSFX());
    }

    // Update is called once per frame
    void Update()
    {
        int childCount = transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            child.Translate(Vector3.forward * Time.deltaTime * child.GetComponent<Event>().speed);
        }


        if (splat.gameObject.activeInHierarchy)
        {
            if (!splat.isActiveAndEnabled)
            {
                splat.gameObject.SetActive(false);
            }
        }
    }


    Coroutine playSfx = null;
    IEnumerator _playSFX()
    {
        while (true)
        {
            foreach(Transform item in children)
            {
                GameObject child = item.gameObject;
                if(child.GetComponent<Event>().sfx != null)
                    AudioManager.instance.PlaySFX(child.GetComponent<Event>().sfx, child, volume);
            }

            yield return new WaitForSecondsRealtime(0.5f);
        }
    }


    public void doAction(GameObject theEvent)
    {
        Event.EVENT_TYPE eventType = theEvent.GetComponent<Event>().eventType;

        float destroyDelay = 0f;

        switch (eventType)
        {
            case Event.EVENT_TYPE.jump:
                jump();
                destroyDelay = 1f;
                break;
            case Event.EVENT_TYPE.duck:
                break;
            case Event.EVENT_TYPE.enemy:
                kill();
                break;
        }

        children.Remove(theEvent.GetComponent<Transform>());
        Destroy(theEvent, destroyDelay);
    }

    public void jump()
    {
        CameraController.instance.jump();
    }

    public void kill()
    {
        AudioManager.instance.PlaySFX(splatSFX);
        splat.gameObject.SetActive(true);
    }
}