using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueSequencer : Singleton<DialogueSequencer>
{
    SpeechBubbleController testBubble_;

    Node currNode_;

    TextLoader textLoader_;

    UnityEvent mainSpeechBubbleCompleted_;

    // Start is called before the first frame update
    void Start()
    {
        textLoader_ = TextLoader.Instance;
        mainSpeechBubbleCompleted_ = new UnityEvent();
        mainSpeechBubbleCompleted_.AddListener(ProceedToNextNode);

        if (testBubble_ == null)
        {
            testBubble_ = GameObject.FindObjectOfType<SpeechBubbleController>();
        }

        Dialogue dialogue1 = new Dialogue(textLoader_.GetDialogues(1), mainSpeechBubbleCompleted_,
            5.0f, 40.0f, Dialogue.BubbleSize.Small, 8.0f);
        Dialogue dialogue2 = new Dialogue(textLoader_.GetDialogues(0), mainSpeechBubbleCompleted_);

        DialogueNode node2 = new DialogueNode(dialogue1, null);
        currNode_ = new DialogueNode(dialogue2, node2);
        ProcessDialogueNode();
    }

    void ProceedToNextNode()
    {
        currNode_ = currNode_.Next;
        ProcessDialogueNode();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ProcessDialogueNode()
    {
        if (currNode_ is DialogueNode node)
        {
            testBubble_.ShowDialogue(node.Dialogue);
        }
    }

}
