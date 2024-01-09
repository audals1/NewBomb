using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMove : NetworkBehaviour
{
    public float moveSpeed = 2f;
    Vector3 direction;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;
        Move();
    }

    bool IsMove()
    {
        if (direction != Vector3.zero) return true;
        else return false;
    }

    void MoveAnimationSet()
    {
        animator.SetBool("IsMove", IsMove());
    }

    void Move()
    {
        MoveAnimationSet();
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Runner.DeltaTime * moveSpeed;
        transform.position += direction;
    }
}
