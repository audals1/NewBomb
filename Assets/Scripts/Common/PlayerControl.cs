using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float jumpForce = 10f;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 땅에 닿아 있는지 체크
        isGrounded = Physics.CheckSphere(transform.position, 0.1f);
        Debug.Log(isGrounded);
        // 점프 입력 처리
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        // 점프 시에만 y 방향으로 힘을 가함
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}


