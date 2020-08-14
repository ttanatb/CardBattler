using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UI
{
    public class CardPileController : MonoBehaviour
    {
        [SerializeField]
        CardPileView view_;

        List<Card> model_;

        private void Awake()
        {
            model_ = new List<Card>();
        }

        private void Start()
        {
            UpdateView();
        }

        public void Remove(int index, int count)
        {
            model_.RemoveRange(index, count);
            UpdateView();
        }

        public void Clear()
        {
            model_.Clear();
            UpdateView();
        }

        public void AddCards(IEnumerable<Card> cardsToAdd)
        {
            model_.AddRange(cardsToAdd);
            UpdateView();
        }

        private void UpdateView()
        {
            view_.CardCount = model_.Count;
        }

        private void Update()
        {
            // Quick print debug.
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Build string based off of all the cards' ToString()'s.
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < model_.Count; i++)
                {
                    stringBuilder.Append(model_[i].ToString());

                    if (i != model_.Count - 1)
                        stringBuilder.Append(", ");
                }

                Debug.Log("Card Pile Controller (" + name + "): " +
                    stringBuilder.ToString());
            }
        }
    }
}