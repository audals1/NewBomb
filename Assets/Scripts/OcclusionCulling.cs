using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionCulling : MonoBehaviour
{
    private OcclusionArea occlusionArea;

    void Start()
    {
        // OcclusionArea ������Ʈ ã��
        occlusionArea = GetComponent<OcclusionArea>();

        if (occlusionArea == null)
        {
            Debug.LogError("OcclusionArea ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ���������� ������Ʈ�� �����ϴ� �ڷ�ƾ ����
        StartCoroutine(UpdateOcclusionArea());
    }

    IEnumerator UpdateOcclusionArea()
    {
        while (true)
        {
            // OcclusionArea�� ���Ϳ� ũ�� ������Ʈ
            Vector3 newCenter = CalculateNewCenter();
            Vector3 newSize = CalculateNewSize();
            occlusionArea.center = transform.InverseTransformPoint(newCenter);
            occlusionArea.size = newSize;

            // ���� �������� ������Ʈ
            yield return new WaitForSeconds(1.0f);
        }
    }

    Vector3 CalculateNewCenter()
    {
        // ���͸� ����ϴ� ���� �߰�
        return transform.position;
    }

    Vector3 CalculateNewSize()
    {
        // ũ�⸦ ����ϴ� ���� �߰�
        return transform.localScale;
    }
}
