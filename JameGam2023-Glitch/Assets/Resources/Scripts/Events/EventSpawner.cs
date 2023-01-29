using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    public static EventSpawner instance;

    public List<GameObject> events;

    public Transform defaultCameraPosition;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }


    public void spawnEvent(int index = -1)
    {
        GameObject toSpawn;

        if(index < 0)
        {
            index = Random.Range(0, events.Count);

            toSpawn = Instantiate(events[index]);

        }
        else
        {
            toSpawn = Instantiate(events[index]);
        }

        toSpawn.transform.SetParent(this.transform);

        Vector3 cameraPos = new Vector3(defaultCameraPosition.position.x, 1f, defaultCameraPosition.position.z);
        toSpawn.transform.LookAt(cameraPos);

        if (Glitches.instance.textureGlitching)
        {
            Glitches.instance.textureGlitch();
            Glitches.instance.textureGlitch();

        }

        EventManager.instance.children.Add(toSpawn.transform);
        
    }
}
