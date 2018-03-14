using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerAxisMovement : Movement
{
    public VirtualJoystick joystickMovement;
    public VirtualJoystick joystickRotation;
    public bool isRotationDragged { get; private set; }

    new Rigidbody2D rigidbody2D;

    public override void OnStart()
    {
        speedEffects = new List<SpeedEffect>();

        rigidbody2D = GetComponent<Rigidbody2D>();

        isRotationDragged = false;
        movementDirection = Vector2.zero;

        maxSpeed = Profile.current.baseAttributtes.mobility / 2;
    }

    void Update()
    {
        #region movement
        movementDirection = new Vector2(joystickMovement.Horizontal(), joystickMovement.Vertical());
        movementDirection.Normalize();
        if (joystickMovement.Horizontal() == 0f && joystickMovement.Vertical() == 0f)
        {
            rigidbody2D.velocity *= 0.9f;
        }
        else
        {
            rigidbody2D.velocity = movementDirection * maxSpeed * GetSpeedMultiplier();
        }
        #endregion

        #region rotation
        if (joystickRotation.Horizontal() != 0f || joystickRotation.Vertical() != 0f)
        {
            float angle = Mathf.Atan2(joystickRotation.Vertical(), joystickRotation.Horizontal()) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            isRotationDragged = true;
        }
        else if (joystickMovement.Horizontal() != 0f || joystickMovement.Vertical() != 0f)
        {
            float angle = Mathf.Atan2(joystickMovement.Vertical(), joystickMovement.Horizontal()) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (joystickRotation.Horizontal() == 0f && joystickRotation.Vertical() == 0f)
        {
            isRotationDragged = false;
        }
        #endregion
    }
}
