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
        // 줄을 회전시키는 코드
        RotateRope();

        // 줄을 그리는 코드
        DrawRope();
    }

    void RotateRope()
    {
        // 줄을 회전
        angle += rotationSpeed * Time.deltaTime;
        if (angle > 360f)
        {
            angle -= 360f;
        }
    }

    void DrawRope()
    {
        // 줄의 두 점을 계산하고 Line Renderer로 그림
        float x1 = Mathf.Cos(angle) * ropeLength;
        float y1 = Mathf.Sin(angle) * ropeLength;

        float x2 = Mathf.Cos(angle + 180f) * ropeLength;
        float y2 = Mathf.Sin(angle + 180f) * ropeLength;

        lineRenderer.SetPosition(0, new Vector3(x1, y1, 0f));
        lineRenderer.SetPosition(1, new Vector3(x2, y2, 0f));
    }
}
