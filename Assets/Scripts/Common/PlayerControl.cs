using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float timeToJumpApex = 0.5f;

    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;

    private bool isGrounded;
    private float groundCheckDistance = 0.1f;

    void Start()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        velocity = Vector3.zero;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (isGrounded)
        {
            velocity.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpVelocity;
            }
        }

        velocity.y += gravity * Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}


