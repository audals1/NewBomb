using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Addons.KCC;


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

    public KCC KCC;
    


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;
        if (GetInput(out BasicInput input) == false) return;
        if (input.Jump && KCC.Data.IsGrounded) KCC.Jump(Vector3.up * 6f);
    }

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        jumpPressed = true;
    //    }
    //}

    public override void Render()
    {
        if (GetInput(out BasicInput input) == true) _animator.SetBool("isJump", true);
        else _animator.SetBool("isJump", false);
    }


    private void Jump()
    {
        float gravity = -(2f * jumpHeight) / Mathf.Pow(timeToJumpApex / 2f, 2f);

        if (isGrounded && velocity <= 0)
        {
            if (jumpPressed)
            {
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
        if (velocity <= 0) velocity = 0;
        Debug.Log(isGrounded);
    }

    public void OnExitGround()
    {
        isGrounded = false;
    }
}


