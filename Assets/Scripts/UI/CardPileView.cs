using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardPileView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI cardCountText_;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public int CardCount { set { cardCountText_.text = value.ToString(); } }

    }
}
