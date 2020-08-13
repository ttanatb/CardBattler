using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI name_;


        [SerializeField]
        TextMeshProUGUI cost_;

        public string Name
        {
            set { name_.text = value; }
        }

        public int Cost
        {
            set { cost_.text = value.ToString(); }
        }

    }
}