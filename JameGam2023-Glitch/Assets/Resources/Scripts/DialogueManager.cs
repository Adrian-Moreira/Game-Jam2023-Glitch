using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // references TextMeshPro - Text(UI)

public class DialogueManager : MonoBehaviour
{
  
    public TextMeshProUGUI textComponent; // holds reference
    private const int NUMLINES = 2;
    private string[] lines = new string[NUMLINES];  // holds dialog
    public float textSpeed;

    private int index = 0; // tracks which conversation we're on

    // Start is called before the first frame update
    void Start()
    {
        initLines();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initLines(){
        lines[0] = "Hey, welcome to %GAME_NAME%! I’ll be your guide through this tutorial. Don’t worry, just relax, I’ll show you all the tips and tricks that you’ll need.";
        lines[1] = "other line";
    }

    void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

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

}
