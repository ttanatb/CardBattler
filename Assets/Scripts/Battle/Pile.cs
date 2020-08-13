using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using UnityEngine;

//TODO make unit test
public class Pile
{
    public Pile(List<Card> cards)
    {
        cards_ = cards;
    }

    public Pile(Card[] cards)
    {
        // TODO: should i really be calling new here
        cards_ = new List<Card>(cards);
    }

    private readonly List<Card> cards_;

    public int Count { get { return cards_.Count; } }

    public Card[] Draw(int count)
    {
        Card[] drawnCards = null;

        // Limit max cards to draw.
        if (count > cards_.Count)
            count = cards_.Count;

        // Early exit for invalid count.
        if (count <= 0)
            return drawnCards;

        // Copy cards over and remove from deck.
        // TODO: should i really be calling new here
        drawnCards = new Card[count];
        int startingIndex = cards_.Count - count;
        cards_.CopyTo(startingIndex, drawnCards, 0, count);
        cards_.RemoveRange(startingIndex, count);

        return drawnCards;
    }

    public List<Card> Empty()
    {
        List<Card> copyOfPile = new List<Card>(cards_);
        cards_.Clear();
        return copyOfPile;
    }

    public void Place(List<Card> cardsToPlace)
    {
        cards_.AddRange(cardsToPlace);
    }

    public void Shuffle()
    {
        for (int i = cards_.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            if (randomIndex == i)
                continue;

            Card temp = cards_[i];
            cards_[i] = cards_[randomIndex];
            cards_[randomIndex] = temp;
        }
    }

    public void Shuffle(List<Card> cardsToShuffleIntoDeck)
    {
        Place(cardsToShuffleIntoDeck);
        Shuffle();
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
