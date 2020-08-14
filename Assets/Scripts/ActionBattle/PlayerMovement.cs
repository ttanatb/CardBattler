using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Physics consts
    const float STOP_SPEED_THRESHOLD = 0.01f;
    const float FLOOR_SNAP_THRESHOLD = 0.01f;
    const float FLOOR_HEIGHT = 0.0f;

    // TODO this shouldn't a const
    const float DIST_TO_ENEMY = 7.5f;

    // TODO this should be set by ActionBattleManager
    [SerializeField]
    private Transform enemyTransform_;

    // Movement/physics fields (for adjustment/tinkering)

    [SerializeField]
    float movementRate_ = 15.0f;

    [SerializeField]
    float immediateTurnVel_ = 2.0f;

    [SerializeField]
    float frictionRate_ = 5.0f;

    [SerializeField]
    float jumpRate_ = 15.0f;

    [SerializeField]
    float immediateJumpVel_ = 2.0f;

    [SerializeField]
    float gravityRate_ = 2.0f;

    // Movement/physics fields (for internal calculations)

    float currAcc_ = 0.0f;
    float currSpeedRad_ = 0.0f;
    float currAngle_ = 0.0f;

    float currAccY_ = 0.0f;
    float currSpeedY_ = 0.0f;
    float currPosY_ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // TEMP input response
        if (Input.GetKeyDown(KeyCode.A))
            TurnLeft();

        if (Input.GetKey(KeyCode.A))
            KeepStrafingLeft();

        if (Input.GetKeyDown(KeyCode.D))
            TurnRight();

        if (Input.GetKey(KeyCode.D))
            KeepStrafingRight();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        // Apply friction.
        currAcc_ += -currSpeedRad_ * frictionRate_;

        // Apply gravity.
        if (currPosY_ > FLOOR_HEIGHT)
            currAccY_ -= gravityRate_;

        // Apply acc to vel, vel to angle.
        currSpeedRad_ += currAcc_ * Time.fixedDeltaTime;
        currSpeedY_ += currAccY_ * Time.fixedDeltaTime;

        currAngle_ += currSpeedRad_ * Time.fixedDeltaTime;
        currPosY_ += currSpeedY_ * Time.fixedDeltaTime;

        // Set velocity to 0.0f if it's close enough.
        if (Mathf.Abs(currSpeedRad_) < STOP_SPEED_THRESHOLD)
            currSpeedRad_ = 0.0f;
        if (Mathf.Abs(currSpeedY_) < STOP_SPEED_THRESHOLD)
            currSpeedY_ = 0.0f;

        // Snap player to floor if close enough.
        if (currPosY_ < FLOOR_SNAP_THRESHOLD)
        {
            currPosY_ = 0.0f;
            currSpeedY_ = 0.0f;
        }

        // All forces have been applied for this frame.
        currAcc_ = 0.0f;
        currAccY_ = 0.0f;

        // Calculate pos from angle.
        Vector3 pos = Vector3.zero;
        pos.x = DIST_TO_ENEMY * Mathf.Cos(currAngle_);
        pos.z = DIST_TO_ENEMY * Mathf.Sin(currAngle_);

        // Apply position/orientation to transform.
        Quaternion orientation = transform.rotation;
        Vector3 lookVec = (enemyTransform_.position - pos).normalized;
        orientation.SetLookRotation(lookVec, Vector3.up);

        // Apply y-positioning last so that look orientation
        // isn't affected by y-position.
        pos.y = currPosY_;

        transform.SetPositionAndRotation(pos, orientation);
    }


    public void KeepStrafingLeft()
    {
        currAcc_ -= movementRate_;
    }

    public void TurnLeft()
    {
        currSpeedRad_ = -immediateTurnVel_;
    }

    public void KeepStrafingRight()
    {
        currAcc_ += movementRate_;
    }
    public void TurnRight()
    {
        currSpeedRad_ = immediateTurnVel_;
    }

    public void Jump()
    {
        currSpeedY_ = immediateJumpVel_;
        currAccY_ += jumpRate_;
    }
}
