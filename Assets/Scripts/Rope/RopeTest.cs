using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTest : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float ropeLength = 5f;

    private LineRenderer lineRenderer;
    private float angle = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // ���� ȸ����Ű�� �ڵ�
        RotateRope();

        // ���� �׸��� �ڵ�
        DrawRope();
    }

    void RotateRope()
    {
        // ���� ȸ��
        angle += rotationSpeed * Time.deltaTime;
        if (angle > 360f)
        {
            angle -= 360f;
        }
    }

    void DrawRope()
    {
        // ���� �� ���� ����ϰ� Line Renderer�� �׸�
        float x1 = Mathf.Cos(angle) * ropeLength;
        float y1 = Mathf.Sin(angle) * ropeLength;

        float x2 = Mathf.Cos(angle + 180f) * ropeLength;
        float y2 = Mathf.Sin(angle + 180f) * ropeLength;

        lineRenderer.SetPosition(0, new Vector3(x1, y1, 0f));
        lineRenderer.SetPosition(1, new Vector3(x2, y2, 0f));
    }
}
