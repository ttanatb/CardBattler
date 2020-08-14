using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    const float STOP_SPEED_THRESHOLD = 0.01f;

    public float FrictionRate { get; set; }
    public float Acceleration { get; set; }
    public float Speed { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Apply friction.
        if (FrictionRate > 0)
            Acceleration += -Speed * FrictionRate;

        // Set velocity to 0.0f if it's close enough.
        if (Mathf.Abs(Speed) < STOP_SPEED_THRESHOLD)
            Speed = 0.0f;

        Speed += Acceleration * Time.fixedDeltaTime;
        transform.position += (Speed * transform.forward * Time.fixedDeltaTime);

        Acceleration = 0.0f;
    }
}
