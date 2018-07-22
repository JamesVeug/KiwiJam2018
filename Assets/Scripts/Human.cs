using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour, IUsable
{
    public float speed = 6.0f;
    public float gravity = 20.0f;
    public float jumpSpeed = 8.0f;

    public float minFollowDistance = 1.0f;

    public CharacterController characterController;
    public Rigidbody rigidBody;
    public Animator animator;


    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private bool movementEnabled = true;
    private PlayerMovement playerToFollow;

    public PushableZone PushableSection = null;

    void FixedUpdate()
    {
        if (!movementEnabled)
        {
            return;
        }

        if (grounded && playerToFollow != null)
        {
            // We are grounded, so recalculate movedirection directly from axes
            FacePlayer();
            moveDirection = GetFollowPlayerDirection();
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        CollisionFlags flags = characterController.Move(moveDirection * Time.deltaTime);
        grounded = (flags &= CollisionFlags.CollidedBelow) != 0;
    }

    private void FacePlayer()
    {
        if (playerToFollow == null)
        {
            return;
        }

        if (playerToFollow.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (playerToFollow.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    private Vector3 GetFollowPlayerDirection()
    {
        Vector3 angle = playerToFollow.transform.position - transform.position;
        float distance = (angle).magnitude;
        if (distance <= minFollowDistance)
        {
            return Vector3.zero;
        }

        angle.y = 0;
        angle.z = 0;

        if (playerToFollow.transform.position.x > transform.position.x)
        {
            angle.x *= -1;
        }

        return angle.normalized;
    }

    public void TogglePhysics(bool active)
    {
        rigidBody.useGravity = !active;
    }

    public void ToggleAI(bool active)
    {
        movementEnabled = active;
        characterController.enabled = active;
    }

    public void FollowPlayer(PlayerMovement player)
    {
        playerToFollow = player;
    }

    public void StopFollowingPlayer()
    {
        playerToFollow = null;
    }

    public void Use(PlayerMovement player)
    {
        if (playerToFollow != null)
        {
            StopFollowingPlayer();
        }
        else
        {
            FollowPlayer(player);
        }
    }

    public void Push()
    {
        if (PushableSection != null)
        {
            ToggleAI(false);
            TogglePhysics(false);
            PushableSection.PlayPushAnimation(transform);
            PushableSection = null;
        }
    }
}
