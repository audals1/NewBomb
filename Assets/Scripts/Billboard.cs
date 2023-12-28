using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;
    public float offsetX, offsetY, offsetZ;
    void Start()
    {
        // 메인 카메라 참조
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다. 스크립트를 적용한 오브젝트에 메인 카메라를 추가하세요.");
        }
    }

    void Update()
    {
        // 메인 카메라가 없다면 업데이트를 중단
        if (mainCamera == null)
            return;

        // 빌보드 설정
        Vector3 targetPosition = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //transform.position = targetPosition;
        transform.LookAt(targetPosition);
        mainCamera.transform.LookAt(transform.position);
        Mathf.Clamp(mainCamera.transform.position.y, 1.0f, 20.0f);
        //메인카메라의 x y z를 Clamp로 이동제한을 걸자
    }
}
