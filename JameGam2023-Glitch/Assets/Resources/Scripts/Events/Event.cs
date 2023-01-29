using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    //ENUM
    public enum EVENT_TYPE { jump, duck, enemy, grab, pet, ignore};
    public EVENT_TYPE eventType;

    /// <summary>
    /// Public instance vars
    /// </summary>
    public float speed;
    public Sprite sprite;
    public AudioClip sfx;

    /// <summary>
    /// Private instance vars
    /// </summary>
    private bool inTrigger = false;
    private bool keyEntered = false;
    private KeyCode correctKey;
    private SpriteRenderer image;

    private void Awake()
    {
        image = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = sprite;

        switch(eventType)
        {
            case EVENT_TYPE.jump:
                correctKey = KeyCode.J;
                break;
            case EVENT_TYPE.duck:
                correctKey = KeyCode.D;
                break;
            case EVENT_TYPE.enemy:
                correctKey = KeyCode.A;
                break;
            case EVENT_TYPE.grab:
                correctKey = KeyCode.G;
                break;
            case EVENT_TYPE.pet:
                correctKey = KeyCode.P;
                break;
            case EVENT_TYPE.ignore:
                correctKey = KeyCode.I;
                break;
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(correctKey) && inTrigger)
        {
            keyEntered = true;
            EventManager.instance.doAction(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;

        ActionZone.instance.enteredZone(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;

        if(!keyEntered)
        {
            EventManager.instance.children.Remove(this.transform);

            Destroy(gameObject, 1f);
        }
    }


    public void setSprite(Sprite newImage)
    {
        image.sprite = newImage;
    }


    public void resetSprite()
    {
        image.sprite = sprite;
    }


    public void turnOnCapsule()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void turnOffCapsule()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
