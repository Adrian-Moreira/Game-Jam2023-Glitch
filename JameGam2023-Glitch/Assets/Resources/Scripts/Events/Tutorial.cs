using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public static Tutorial instance;

    private static float[] delay = new float[100];
    public static bool runningTutorial, ranTutorial;
    private static int i = 0;

    [SerializeField] private AudioClip[] audio;

    void Start()
    {
        instance = this;
        delay[0] = 1;
        delay[1] = 10;
        delay[2] = 10;
        delay[3] = 10;
        delay[4] = 5;
        delay[5] = 5;
        delay[6] = 5;
        delay[7] = 5;
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
                        DialogueManager.instance.StartDialogue("Hello, and welcome to relax! I will be your guide through this tutorial. Don’t worry, just relax. I’ll show you all of the tips and tricks that you’ll be needing.");
                        // EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[0], 1);
                        i++;
                    }
                    else if(i == 1){
                        DialogueManager.instance.StartDialogue("Woah, look out! There’s an enemy approaching you!");
                        EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[1], 1);
                        i++;
                    }
                    else if(i == 2){
                        DialogueManager.instance.StartDialogue("Uhh… *mic ruffling* Dillan? The test user just entered in *that* code *silence* yeah that one *silence* what do we- uhh… wait, give me a second *silence followed by keyboard clacks and mouse clicks* Welp… *expletive bleep* Uhm, okay. Sorry about that, just stand still and do what I tell you while we work on a fix…");
                        // EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[2], 1);
                        i++;
                    }
                    else if(i == 3){
                        DialogueManager.instance.StartDialogue("Welcome to the relax guided audio meditation. relax, the game where you just relax. First close your eyes…");
                        EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[3], 1);
                        i++;
                    }
                    else if(i == 4){
                        DialogueManager.instance.StartDialogue("Press a to attack.");
                        EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[4], 1);
                        i++;
                    }
                    else if(i == 5){
                        DialogueManager.instance.StartDialogue("Press c to calm down.");
                        EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[5], 1);
                        i++;
                    }
                    else if(i == 6){
                        DialogueManager.instance.StartDialogue("Press c to close your eyes.");
                        EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[6], 1);
                        i++;
                    }
                    else if(i == 7){
                        DialogueManager.instance.StartDialogue("");
                        EventSpawner.instance.spawnEvent();
                        AudioManager.instance.PlaySFX(audio[11], 1);
                        i++;
                    }
                    else if(i == 8){
                        runningTutorial = false;
                        ranTutorial = true;
                        GameController.instance.startEndless();
                    }


                }
            }

            
        }
    }

    public static void runTutorial(){
        runningTutorial = true;
    }
    

}
