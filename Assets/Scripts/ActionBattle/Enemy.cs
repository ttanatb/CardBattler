using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform_;

    [SerializeField]
    private GameObject projectilePrefab_;

    [SerializeField]
    private float projectileIntervalSec_ = 5.0f;
    private float projectileTimer_ = 0.0f;


    [SerializeField]
    private float projectileFrictionRate = 2.0f;
    [SerializeField]
    private float projectileAcceleration = 15.0f;
    [SerializeField]
    private float projectileSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateProjectile();

        // Apply position/orientation to transform.
        Quaternion orientation = transform.rotation;
        Vector3 lookVec = playerTransform_.position - transform.position;
        lookVec.y = 0;

        orientation.SetLookRotation(lookVec.normalized, Vector3.up);
        transform.SetPositionAndRotation(transform.position, orientation);
    }

    private void UpdateProjectile()
    {
        projectileTimer_ += Time.deltaTime;
        if (projectileTimer_ > projectileIntervalSec_)
        {
            projectileTimer_ -= projectileIntervalSec_;

            GameObject projectileObj = Instantiate(projectilePrefab_,
                transform.position, transform.rotation);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            projectile.Speed = projectileSpeed;
            projectile.Acceleration = projectileAcceleration;
            projectile.FrictionRate = projectileFrictionRate;
        }
    }
}
