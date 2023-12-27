using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform targetPlayer;
    BombGame bombGame;

    void Start()
    {
        bombGame = FindObjectOfType<BombGame>();
        
    }

    void Update()
    {
        // bombGame 또는 playerList가 null인 경우에 대한 예외 처리 추가
        if (bombGame == null)
        {
            Debug.LogError("BombGame이 할당되지 않았습니다.");
            return;
        }

        if (bombGame.playerList == null || bombGame.playerList.Length == 0)
        {
            Debug.LogError("PlayerList가 비어 있거나 null입니다.");
            return;
        }

        int bombHolderIndex = bombGame.GetBombHolderIndex();

        // 범위 체크를 통해 인덱스 에러 예방
        if (bombHolderIndex >= 0 && bombHolderIndex < bombGame.playerList.Length)
        {
            targetPlayer = bombGame.playerList[bombHolderIndex].transform;

            // targetPlayer가 null이 아닌지 체크
            if (targetPlayer != null)
            {
                // 대상 플레이어 방향 계산
                Vector3 relativeDirection = targetPlayer.position - transform.position;
                Debug.DrawRay(transform.position, relativeDirection, Color.magenta);

                // Z 축 회전 각도 계산
                float angle = Mathf.Atan2(relativeDirection.y, relativeDirection.x) * Mathf.Rad2Deg;

                // 화살표 회전 적용
                transform.rotation = Quaternion.Euler(new Vector3(90, 0, angle));
            }
            else
            {
                Debug.LogError("타겟 플레이어가 null입니다.");
            }
        }
        else
        {
            Debug.LogError("올바르지 않은 BombHolder 인덱스입니다.");
        }
    }
}
