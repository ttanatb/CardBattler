using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    // SPRITE_ID? CARD_ID? (to identify for UI)
    // CARD DESCRIPTION

    public int Cost { get; set; }

    // TODO: Use string reference instead of passing strings around.
    public string Name { get; set; }

    public Card(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }

    public override string ToString()
    {
        //return "name: " + Name + " cost: " + Cost;
        return Name;
    }
}
