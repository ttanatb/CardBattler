using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    // TODO: keep track of the pool of all cards in play
    private Pile deck_;
    private Pile discardPile_;
    private Hand hand_;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // FOR TESTING
        int count = 9;
        Card[] testingCards = new Card[count];
        for (int i = 0; i < count; i++)
        {
            testingCards[i] = new Card("Testing " + i, i);
        }

        deck_ = new Pile(testingCards);
        discardPile_ = new Pile(new List<Card>());
        hand_ = new Hand();
    }

    // Update is called once per frame
    void Update()
    {
        // FOR TESTING

        // Draw 5 cards.
        if (Input.GetKeyDown(KeyCode.E))
        {
            DrawToHand(3);
            Log();
        }

        // Discard hand.
        if (Input.GetKeyDown(KeyCode.D))
        {
            discardPile_.Place(hand_.Discard());
            Log();
        }

        // Quick print debug.
        if (Input.GetKeyDown(KeyCode.R))
        {
            Log();
        }
    }

    private void Log()
    {
        Debug.Log("Deck: " + deck_.ToString());
        Debug.Log("Hand: " + hand_.ToString());
        Debug.Log("Discard Pile: " + discardPile_.ToString());
    }

    private void DrawToHand(int amount)
    {
        // TODO: Better way to structure this function?


        // Early exit if nothing in draw pile or discard pile.
        if (deck_.Count + discardPile_.Count == 0)
            return;

        int actualDrawAmount = Math.Min(amount, deck_.Count);
        if (actualDrawAmount > 0)
            hand_.Add(deck_.Draw(actualDrawAmount));

        // If deck < drawAmount, shuffle discard into deck, and draw up to 5.
        if (actualDrawAmount < amount)
        {
            deck_.Shuffle(discardPile_.Empty());

            // TODO: remove this later
            // shuffle > draw -- covered
            // shulffe > draw (not enough drawn) -- function covers this
            // shuffle > nothing (deck + discard = 0)
            hand_.Add(deck_.Draw(amount - actualDrawAmount));
        }
    }
}
