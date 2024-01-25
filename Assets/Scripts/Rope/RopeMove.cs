using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RopeMove : NetworkBehaviour
{
    private float xMin = -8.61f; //-8.61f
    private float xMax = 11.3f; //11.3f
    [Header("�̵� �ӵ� ������")]
    public float sinMultiplier = 5f; // �̵��ӵ� ������
    [Header("�� ���� ������")]
    public float cosMultiplier = 5f; // �� ���� ������

    private float timeValue = 0f;

    public override void FixedUpdateNetwork()
    {
        float xValue = Mathf.Sin(timeValue * sinMultiplier);
        float yValue = Mathf.Cos(timeValue * sinMultiplier);

        float xPos = xValue * (xMax - xMin) * 0.5f;
        float yPos = yValue * cosMultiplier;

        yPos = Mathf.Clamp(yPos, 0.1f, 10f);

        transform.position = new Vector3(xPos, yPos, 0.0f);

        timeValue += Runner.DeltaTime;
    }

}
