using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionCulling : MonoBehaviour
{
    private OcclusionArea occlusionArea;

    void Start()
    {
        // OcclusionArea 컴포넌트 찾기
        occlusionArea = GetComponent<OcclusionArea>();

        if (occlusionArea == null)
        {
            Debug.LogError("OcclusionArea 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // 정기적으로 업데이트를 수행하는 코루틴 시작
        StartCoroutine(UpdateOcclusionArea());
    }

    IEnumerator UpdateOcclusionArea()
    {
        while (true)
        {
            // OcclusionArea의 센터와 크기 업데이트
            Vector3 newCenter = CalculateNewCenter();
            Vector3 newSize = CalculateNewSize();
            occlusionArea.center = transform.InverseTransformPoint(newCenter);
            occlusionArea.size = newSize;

            // 일정 간격으로 업데이트
            yield return new WaitForSeconds(1.0f);
        }
    }

    Vector3 CalculateNewCenter()
    {
        // 센터를 계산하는 로직 추가
        return transform.position;
    }

    Vector3 CalculateNewSize()
    {
        // 크기를 계산하는 로직 추가
        return transform.localScale;
    }
}
