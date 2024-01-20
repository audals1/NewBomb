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
        //RotateClockHand();
        if (minMove.transform.rotation == transform.rotation)
        {
            count++;
            Debug.Log($"ม฿บน{count}");
        }
    }

    void InitRotation()
    {
        Random.InitState(System.DateTime.Now.Millisecond + 1);
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
