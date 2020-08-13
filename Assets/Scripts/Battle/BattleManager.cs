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

    private UI.UIManager uiManager_;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager_ = UI.UIManager.Instance;

        // FOR TESTING
        int count = 10;
        Card[] testingCards = new Card[count];
        for (int i = 0; i < count; i++)
        {
            testingCards[i] = new Card("Testing " + i, i);
        }

        deck_ = new Pile(testingCards);
        discardPile_ = new Pile(new List<Card>());
        hand_ = new Hand();

        uiManager_.SetDrawDeck(testingCards);
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
            List<Card> cardsDiscarded = hand_.Discard();
            discardPile_.Place(cardsDiscarded);
            uiManager_.DiscardHandToDiscardPile(cardsDiscarded);
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
        int totalAmountDrawn = Math.Min(amount, deck_.Count + discardPile_.Count);

        // Early exit if nothing in draw pile or discard pile.
        if (totalAmountDrawn == 0)
            return;

        // TODO: implement max hand size

        // Try to draw from deck if available.
        int firstDrawAmount = Math.Min(amount, deck_.Count);
        if (firstDrawAmount > 0)
        {
            Card[] cardsDrawn = deck_.Draw(firstDrawAmount);
            hand_.Add(cardsDrawn);
            uiManager_.DrawFromDeckToHand(cardsDrawn,
                deck_.Count, firstDrawAmount);
        }

        // Shuffle discard to deck, draw remaining cards
        if (firstDrawAmount < amount)
        {
            totalAmountDrawn = Math.Min(amount, deck_.Count + discardPile_.Count);

            // Early exit if nothing in draw pile or discard pile.
            if (totalAmountDrawn == 0)
                return;

            deck_.Shuffle(discardPile_.Empty());
            uiManager_.ShuffleDiscardPileToDeck(deck_.Cards);

            Card[] cardsDrawn = deck_.Draw(amount - firstDrawAmount);
            hand_.Add(cardsDrawn);
            uiManager_.DrawFromDeckToHand(cardsDrawn,
                deck_.Count, amount - firstDrawAmount);
        }
    }
}
