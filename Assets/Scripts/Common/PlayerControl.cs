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
        // ���� ��� �ִ��� üũ
        isGrounded = Physics.CheckSphere(transform.position, 0.1f);
        Debug.Log(isGrounded);
        // ���� �Է� ó��
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        // ���� �ÿ��� y �������� ���� ����
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}


