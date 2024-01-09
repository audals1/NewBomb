using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;
    public float offsetX, offsetY, offsetZ;
    public float smoothFactor = 5f;
    void Awake()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다.");
        }
    }

    void FixedUpdate()
    {
        var clampY = Mathf.Clamp(mainCamera.transform.position.y, 1.0f, 20.0f);
        var clampX = Mathf.Clamp(mainCamera.transform.position.x, -15.0f, 15.0f);
        Vector3 targetPosition = new Vector3(clampX + offsetX, clampY + offsetY, mainCamera.transform.position.z + offsetZ);
        //transform.LookAt(targetPosition);
        mainCamera.transform.LookAt(transform.position);
        mainCamera.transform.position = targetPosition;
    }
}
