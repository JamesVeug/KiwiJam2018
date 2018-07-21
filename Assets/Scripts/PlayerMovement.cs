﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private bool movingOnLadder = false;
    private Ladder attachedLadder = null;
    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector2 axis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movingOnLadder)
        {
            if (axis.x != 0.0)
            {
                movingOnLadder = false;
                MoveHorizontally(axis * jumpSpeed);
            }
            else
            {
                moveDirection = new Vector3(0, axis.y, 0);
                moveDirection *= speed;
                MoveInDirection(moveDirection);
            }
        }
        else if(attachedLadder != null && axis.y != 0.0 && axis.x == 0.0)
        {
            movingOnLadder = true;

            Vector3 newPosition = ClosestPointOnLine(attachedLadder.BottomClimbPoint.position, attachedLadder.TopClimbPoint.position, transform.position);
            newPosition.y = transform.position.y;
            newPosition.z = transform.position.z;
            gameObject.transform.position = newPosition;

            MoveInDirection(moveDirection);
        }

        if (!movingOnLadder)
        {
            if (grounded)
            {
                // We are grounded, so recalculate movedirection directly from axes
                MoveHorizontally(axis);

                // Jump
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;
            }

            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            MoveInDirection(moveDirection);
        }
    }

    private void MoveHorizontally(Vector3 axis)
    {
        // We are grounded, so recalculate movedirection directly from axes
        if (axis.x < 0.0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (axis.x > 0.0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        Vector3 dir = new Vector3(0, 0, Mathf.Abs(axis.x));
        moveDirection = transform.TransformDirection(dir);
        moveDirection *= speed;
    }
    
    private void MoveInDirection(Vector3 direction)
    {
        CollisionFlags flags = characterController.Move(direction * Time.deltaTime);
        grounded = (flags &= CollisionFlags.CollidedBelow) != 0;
    }

    private Vector2 GetMovement()
    {
        Vector2 direction = new Vector2();

        if (attachedLadder == null)
        {
            // Can only go left and right When NOT on a ladder
            direction.x = Input.GetAxis("Horizontal");
            if (direction.x < 0.0)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else if (direction.x > 0.0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
        else if (attachedLadder != null)
        {
            // Can only go up and down when on a ladder
            direction.y = Input.GetAxis("Vertical");
        }

        return direction;
    }

    public void DetachLadder(Ladder ladder)
    {
        if (attachedLadder == ladder)
        {
            attachedLadder = null;
            movingOnLadder = false;
        }
    }

    public void AttachLadder(Ladder ladder)
    {
        if (attachedLadder != null)
        {
            return;
        }

        attachedLadder = ladder;
        moveDirection = Vector3.zero;
    }

    private Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
    {
        var vVector1 = vPoint - vA;
        var vVector2 = (vB - vA).normalized;

        var d = Vector3.Distance(vA, vB);
        var t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return vA;

        if (t >= d)
            return vB;

        var vVector3 = vVector2 * t;

        var vClosestPoint = vA + vVector3;

        return vClosestPoint;
    }
}