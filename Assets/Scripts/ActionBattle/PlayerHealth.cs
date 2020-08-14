using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    LayerMask collisionMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionMask == (collisionMask | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Oof Ouch Owwie");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionMask == (collisionMask | (1 << other.gameObject.layer)))
        {
            Debug.Log("Oof Ouch Owwie");
        }
    }
}
