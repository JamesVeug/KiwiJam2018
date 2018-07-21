using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = 20.0f;
    public float jumpSpeed = 8.0f;

    public CharacterController characterController;
    public Rigidbody rigidBody;
    public Animator animator;


    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private bool movementEnabled = true;

    void FixedUpdate()
    {
        if (!movementEnabled)
        {
            return;
        }

        if (grounded)
        {
            // We are grounded, so recalculate movedirection directly from axes
            moveDirection = new Vector3(0, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        CollisionFlags flags = characterController.Move(moveDirection * Time.deltaTime);
        grounded = (flags &= CollisionFlags.CollidedBelow) != 0;
    }

    public void TogglePhysics(bool active)
    {
        rigidBody.detectCollisions = active;
        rigidBody.isKinematic = !active;
    }

    public void ToggleAI(bool active)
    {
        movementEnabled = active;
        characterController.enabled = active;
    }
}
