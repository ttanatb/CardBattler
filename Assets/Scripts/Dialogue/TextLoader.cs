using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLoader : Singleton<TextLoader>
{
    public string[] GetDialogues(int dialogueID)
    {
        if (dialogueID == 0)
        {

            string[] dialogues = new string[4];
            dialogues[0] = "Sample text blah blah blah blah";
            dialogues[1] = "Hello hello this is the second speech bubble now";
            dialogues[2] = "oh wow tanat why don't you make a unit test";
            dialogues[3] = "how tf do i unit test in unity";
            return dialogues;
        }
        else
        {
            string[] dialogues = new string[2];
            dialogues[0] = "Howdy-doo-da-dee-da! Okay i'll never say that ever again.";
            dialogues[1] = "I swear I'm not weird, even my mom said so.";
            return dialogues;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: read from a file and store into dictionary
    }


    // TODO: loaad and unload text asyncly?

    // Update is called once per frame
    void Update()
    {

    }
}
