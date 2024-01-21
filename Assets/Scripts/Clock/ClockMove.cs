using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ClockMove : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public MinMove minMove;
    int count;
    void Start()
    {
        InitRotation();
    }

    // Update is called once per frame
    void Update()
    {
        RotateClockHand();
        if (minMove.transform.rotation == transform.rotation)
        {
            count++;
            Debug.Log($"ม฿บน{count}");
        }
    }

    void InitRotation()
    {
        float random = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, random, 0f);
        transform.rotation = randomRotation;
    }

    void RotateClockHand()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationAmount, 0f);
    }
}
