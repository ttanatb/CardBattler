using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Hand
{
    List<Card> cards_;

    public Hand()
    {
        cards_ = new List<Card>();
    }

    public void Add(IEnumerable<Card> cardsToAdd)
    {
        cards_.AddRange(cardsToAdd);
    }

    public void Add(Card cardToAdd)
    {
        cards_.Add(cardToAdd);
    }

    public List<Card> Discard()
    {
        List<Card> copyOfHand = new List<Card>(cards_);
        cards_.Clear();
        return copyOfHand;
    }

    // TODO think about having pile and hand inheriting from the same thing.
    public override string ToString()
    {
        // Build string based off of all the cards' ToString()'s.
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < cards_.Count; i++)
        {
            stringBuilder.Append(cards_[i].ToString());

            if (i != cards_.Count - 1)
                stringBuilder.Append(", ");
        }

        return stringBuilder.ToString();

    }
}
