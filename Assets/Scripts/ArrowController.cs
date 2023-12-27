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
        // bombGame �Ǵ� playerList�� null�� ��쿡 ���� ���� ó�� �߰�
        if (bombGame == null)
        {
            Debug.LogError("BombGame�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        if (bombGame.playerList == null || bombGame.playerList.Length == 0)
        {
            Debug.LogError("PlayerList�� ��� �ְų� null�Դϴ�.");
            return;
        }

        int bombHolderIndex = bombGame.GetBombHolderIndex();

        // ���� üũ�� ���� �ε��� ���� ����
        if (bombHolderIndex >= 0 && bombHolderIndex < bombGame.playerList.Length)
        {
            targetPlayer = bombGame.playerList[bombHolderIndex].transform;

            // targetPlayer�� null�� �ƴ��� üũ
            if (targetPlayer != null)
            {
                // ��� �÷��̾� ���� ���
                Vector3 relativeDirection = targetPlayer.position - transform.position;
                Debug.DrawRay(transform.position, relativeDirection, Color.magenta);

                // Z �� ȸ�� ���� ���
                float angle = Mathf.Atan2(relativeDirection.y, relativeDirection.x) * Mathf.Rad2Deg;

                // ȭ��ǥ ȸ�� ����
                transform.rotation = Quaternion.Euler(new Vector3(90, 0, angle));
            }
            else
            {
                Debug.LogError("Ÿ�� �÷��̾ null�Դϴ�.");
            }
        }
        else
        {
            Debug.LogError("�ùٸ��� ���� BombHolder �ε����Դϴ�.");
        }
    }
}
