using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        HandController handController_;

        [SerializeField]
        CardPileController deckController_;

        [SerializeField]
        CardPileController discardPileController_;

        BattleManager battleManager_;

        private void Start()
        {
            battleManager_ = BattleManager.Instance;
        }

        public void DrawFromDeckToHand(IEnumerable<Card> cardsDrawn, int indexToRemove, int countToRemove)
        {
            handController_.Add(cardsDrawn);
            deckController_.Remove(indexToRemove, countToRemove);
        }

        public void DiscardHandToDiscardPile(IEnumerable<Card> cardsDiscarded)
        {
            handController_.Discard();
            discardPileController_.AddCards(cardsDiscarded);
        }

        public void ShuffleDiscardPileToDeck(IEnumerable<Card> cardsShuffled)
        {
            discardPileController_.Clear();
            SetDrawDeck(cardsShuffled);
        }

        public void SetDrawDeck(IEnumerable<Card> deck)
        {
            deckController_.AddCards(deck);
        }
    }
}
