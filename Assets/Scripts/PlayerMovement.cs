using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;

    void FixedUpdate()
    {
        if (grounded)
        {
            // We are grounded, so recalculate movedirection directly from axes
            float direction = Input.GetAxis("Horizontal");
            if (direction < 0.0)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                moveDirection = new Vector3(0, 0, -direction);
            }
            else if (direction > 0.0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                moveDirection = new Vector3(0, 0, direction);
            }
            else
            {
                moveDirection = new Vector3(0, 0, 0);
            }

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        CharacterController controller = (CharacterController)GetComponent(typeof(CharacterController));
        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        grounded = (flags &= CollisionFlags.CollidedBelow) != 0;
    }
}
