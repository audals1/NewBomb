using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float timeToJumpApex = 0.5f;

    private float jumpVelocity;
    [SerializeField] private float velocity;

    private bool isGrounded = false;

    private Rigidbody GetRigidbody;


    void Awake()
    {
        GetRigidbody = GetComponent<Rigidbody>();    
    }
    void Update()
    {
        float gravity = -(2f * jumpHeight) / Mathf.Pow(timeToJumpApex / 2f, 2f);

        if (isGrounded && velocity <= 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpVelocity = -gravity * timeToJumpApex / 2f;
                velocity = jumpVelocity;
            }
        }

        else
        {
            velocity += gravity * Time.deltaTime;
        }

        var position = GetRigidbody.position;
        position.y += velocity * Time.deltaTime;
        GetRigidbody.MovePosition(position);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnEnterGround();    
    }
    void OnCollisionExit(Collision collision)
    {
        OnExitGround();    
    }

    public void OnEnterGround()
    {
        Debug.Log("Enter Ground : " + velocity);
        isGrounded = true;
        if (velocity <= 0)
        {
            velocity = 0;
        }
    }

    public void OnExitGround()
    {
        Debug.Log("Exit Ground");
        isGrounded = false;
    }
}


