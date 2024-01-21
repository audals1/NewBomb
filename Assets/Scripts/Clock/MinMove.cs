using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMove : MonoBehaviour
{
    public float rotationSpeed = 10f;
    void Awake()
    {
        InitRotation();
    }

    // Update is called once per frame
    void Update()
    {
        RotateClockHand();
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
