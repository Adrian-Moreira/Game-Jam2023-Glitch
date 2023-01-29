using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public static Narrator instance;
    private string startMsg = "Hey, welcome to %GAME_NAME%! I’ll be your guide through this tutorial. Don’t worry, just relax, I’ll show you all the tips and tricks that you’ll need.";
    private bool dialogBoxStatus = false;

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("mew");
            if(!dialogBoxStatus)
            {
                DialogueManager.instance.DisplayDialogBox(true);  // display box
                Speak(startMsg);
                dialogBoxStatus = true; // remove this for funny text
            } else
            {
                DialogueManager.instance.DisplayDialogBox(false);  // hide box
                dialogBoxStatus = false;
            }
        }
    }

    void Speak(object lines) {

        if(lines.GetType() == typeof(string)) { 
                DialogueManager.instance.StartDialogue(startMsg); // how to call function from another script
        } else { //if(lines.GetType() == typeof(AudioClip)){
            // play audio
            Debug.Log("suppose to play audio");
        }

    }

}
