using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyMovement : Movement
{
    const int ANGLE_BETWEEN_MOVEMENT_AND_ROTATION_DIRECTIONS = 90;

    EnemyStatistics ES;

    new Rigidbody2D rigidbody2D;

    public override void OnStart()
    {
        speedEffects = new List<SpeedEffect>();
        ES = GetComponent<EnemyStatistics>();
        maxSpeed = ES.speed;
        movementDirection = Vector2.zero;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (ES.player != null && ES.playerDead == false)
        {
            movementDirection = new Vector2(ES.player.transform.position.x - transform.position.x, ES.player.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg + ANGLE_BETWEEN_MOVEMENT_AND_ROTATION_DIRECTIONS;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            movementDirection = Vector2.zero;
        }

        rigidbody2D.velocity = movementDirection.normalized * GetSpeedMultiplier() * maxSpeed;
    }
}
