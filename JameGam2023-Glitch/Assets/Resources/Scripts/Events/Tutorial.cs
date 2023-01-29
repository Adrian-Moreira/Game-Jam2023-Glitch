using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public static Tutorial instance;

    private static float[] delay = new float[100];
    public static bool runningTutorial, ranTutorial;
    private static int i = 0;

    void Start()
    {
        instance = this;
        delay[0] = 1;
        delay[1] = 1;
        Debug.Log(delay[0] + "" + delay[1]);
        runningTutorial = false;
        ranTutorial = false;
    }

    void Update()
    {
        if(runningTutorial && !ranTutorial){
            
            if(delay[i] > 0)
            {
                delay[i] -= Time.deltaTime;
                if(delay[i] <= 0)
                {
                    if(i == 0){
                        DialogueManager.instance.StartDialogue("Hi!");
                        EventSpawner.instance.spawnEvent();
                        i++;
                    }
                    else if(i == 1){
                        runningTutorial = false;
                        ranTutorial = true;
                    }


                }
            }

            
        }
    }

    public static void runTutorial(){
        runningTutorial = true;
    }
    

}
