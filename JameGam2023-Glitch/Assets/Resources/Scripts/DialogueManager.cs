using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // references TextMeshPro - Text(UI)

public class DialogueManager : MonoBehaviour
{
  
    public static DialogueManager instance;
    // public Animator animator;

    void Awake(){
        instance = this;
    }

    public DialogueManager(){}

    public TextMeshProUGUI textComponent; // holds reference
    private const int NUMLINES = 1; //
    private string[] lines = new string[NUMLINES];  // holds dialog
    public float textSpeed;

    // private string startMsg = "Hey, welcome to %GAME_NAME%! I’ll be your guide through this tutorial. Don’t worry, just relax, I’ll show you all the tips and tricks that you’ll need.";

    private int index = 0; // tracks which conversation we're on

    // Start is called before the first frame update
    void Start()
    {
        DisplayDialogBox(true);
        // textComponent.text = string.Empty;
        // StartDialogue(startMsg);
    }

    public void StartDialogue(string newLine)
    {

        textComponent.text = string.Empty; // remove this and "dialogBoxStatus = true; // remove this for funny text" in Narrarotor for funny
        lines[0] = newLine;
        StartCoroutine(TypeLine());
    }

    // public float delay = 10;
    // float timer;
    // void Update()
    // {
    //     timer += Time.deltaTime;
    //     if (timer > delay)
    //     {
    //         StartDialogue("stop talking!");
    //     }
    // }

    IEnumerator TypeLine()
    {

        string word = "";
        // Type each character 1 by 1
        foreach(char c in lines[index].ToCharArray())
        {

            // Debug.Log("");
            textComponent.text += c;

            if(c != ' ')
            {
                word += c;
            } else // we're still building a word
            {
                word = ""; // reset word when next char 
            }

            yield return new WaitForSeconds(textSpeed);
            
             if(textComponent.isTextOverflowing) // if the letter we added overflowed
            {
                textComponent.text = string.Empty; // clear textbox
                // if(c != ' '){
                //     textComponent.text += word;
                // }
                textComponent.text += c; // add letter back and continue as normal
            } 

        }

    }

    public void DisplayDialogBox(bool changeDisplay)
    {
        gameObject.SetActive(changeDisplay); // hide dialog box
        // animator.SetBool("isOpen", changeDisplay);

    }

}
