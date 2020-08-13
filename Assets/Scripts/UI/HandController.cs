using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class HandController : MonoBehaviour
    {
        // TODO - get this data from battle handler instead.
        const int MAX_HAND_SIZE = 10;

        [SerializeField]
        GameObject cardViewPrefab_;

        List<CardView> cardViews_;
        List<Card> model_;

        // Start is called before the first frame update
        void Start()
        {
            cardViews_ = new List<CardView>(MAX_HAND_SIZE);
            model_ = new List<Card>(MAX_HAND_SIZE);

            for (int i = 0; i < MAX_HAND_SIZE; ++i)
            {
                GameObject cardViewObj = Instantiate(cardViewPrefab_, transform);
                cardViews_.Add(cardViewObj.GetComponent<CardView>());
                cardViews_[i].gameObject.SetActive(false);
                RectTransform rectTransform = cardViews_[i].GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector2(-230 + 50 * i, 60);

                // TODO - only do this in debug.
                cardViews_[i].name += " " + i;
            }
        }

        public void Add(IEnumerable<Card> cardsToAdd)
        {
            model_.AddRange(cardsToAdd);
            UpdateView();
        }

        public void Add(Card cardToAdd)
        {
            model_.Add(cardToAdd);
            UpdateView();
        }

        public void Discard()
        {
            model_.Clear();
            UpdateView();
        }

        public void RemoveAt(int index)
        {
            model_.RemoveAt(index);
            UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < cardViews_.Count; i++)
            {
                if (i < model_.Count)
                {
                    cardViews_[i].Cost = model_[i].Cost;
                    cardViews_[i].Name = model_[i].Name;
                    cardViews_[i].gameObject.SetActive(true);
                }
                else
                {
                    cardViews_[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
