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
        Random.InitState(System.DateTime.Now.Millisecond);
        float random = Random.Range(0f, 360f);
        //Debug.Log($"random : {random}");
        Quaternion randomRotation = Quaternion.Euler(0f, random, 0f);
        transform.rotation = randomRotation;
        //Debug.Log(transform.rotation.y);
    }

    void RotateClockHand()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationAmount, 0f);
    }
}
