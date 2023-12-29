using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;
    public float offsetX, offsetY, offsetZ;
    void Start()
    {
        // ���� ī�޶� ����
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("���� ī�޶� ã�� �� �����ϴ�. ��ũ��Ʈ�� ������ ������Ʈ�� ���� ī�޶� �߰��ϼ���.");
        }
    }

    void Update()
    {
        // ���� ī�޶� ���ٸ� ������Ʈ�� �ߴ�
        if (mainCamera == null)
            return;

        // ������ ����
        var clampY = Mathf.Clamp(mainCamera.transform.position.y, 1.0f, 20.0f);
        Vector3 targetPosition = new Vector3(mainCamera.transform.position.x, clampY, mainCamera.transform.position.z);
        //transform.position = targetPosition;
        transform.LookAt(targetPosition);
        mainCamera.transform.LookAt(transform.position);
        mainCamera.transform.position = targetPosition;
        Debug.Log(mainCamera.transform.position.y);
        //����ī�޶��� x y z�� Clamp�� �̵������� ����
    }
}
