using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerControl : NetworkBehaviour
{
    public float jumpHeight = 2f;
    public float timeToJumpApex = 0.5f;

    private float jumpVelocity;
    [SerializeField] private float velocity;

    private bool isGrounded = false;
    private bool jumpPressed = false;

    private Rigidbody _rigidbody;
    private Animator _animator;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return; 
            Jump();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        OnEnterGround();    
    }
    private void OnCollisionExit(Collision collision)
    {
        OnExitGround();    
    }

    private void Jump()
    {
        float gravity = -(2f * jumpHeight) / Mathf.Pow(timeToJumpApex / 2f, 2f);

        if (isGrounded && velocity <= 0)
        {
            if (jumpPressed)
            {
                _animator.SetBool("isJump", true);
                jumpVelocity = -gravity * timeToJumpApex / 2f;
                velocity = jumpVelocity;
            }
        }

        else
        {
            velocity += gravity * Runner.DeltaTime;
        }

        var position = _rigidbody.position;
        position.y += velocity * Runner.DeltaTime;
        _rigidbody.MovePosition(position);
    }

    public void OnEnterGround()
    {
        isGrounded = true;
        _animator.SetBool("isJump", false);
        if (velocity <= 0) velocity = 0;
        Debug.Log(isGrounded);
    }

    public void OnExitGround()
    {
        isGrounded = false;
    }
}


