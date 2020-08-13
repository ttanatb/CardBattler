using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    protected Node next_;
    public Node Next { get { return next_; } set { next_ = value; } }

    public Node(Node next)
    {
        next_ = next;
    }

}

public class DialogueNode : Node
{
    public Dialogue Dialogue { get; set; }

    public DialogueNode(Dialogue dialogue, DialogueNode next) : base(next)
    {
        Dialogue = dialogue;
    }
}

public class EventNode : Node
{

    public int EventID { get; set; }

    public EventNode(int eventID, DialogueNode next) : base(next)
    {
        EventID = eventID;
    }
}

