using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectUnit : MonoBehaviour
{
    List<Player> unitList = new List<Player>();
    BombGame bombGame;

    void Start()
    {
        bombGame = FindObjectOfType<BombGame>();    
    }


    public Player GetNearestPlayer()
    {
        if (unitList.Count == 0) return null;

        Player nearestPlayer = unitList[0];
        float minDistance = Vector3.SqrMagnitude(transform.position - nearestPlayer.transform.position);

        foreach (Player player in unitList)
        {
            float distance = Vector3.SqrMagnitude(transform.position - player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPlayer = player;
            }
        }

        return nearestPlayer;
    }

     void OnTriggerEnter(Collider other)
    {
        Player otherPlayer = other.GetComponent<Player>();
        Player currentPlayer = transform.parent.GetComponent<Player>();
        if (otherPlayer != null && !unitList.Contains(otherPlayer) && currentPlayer.HasBomb())
        {
            unitList.Add(otherPlayer);
            Debug.Log($"{other.name} ���� ��ź ����");
            otherPlayer.SetBomb(true);
            currentPlayer.SetBomb(false);

            //��ź ���� �÷��̾� �̵� �ӵ� ��

            otherPlayer.moveSpeed *= 1.5f;
            currentPlayer.moveSpeed = 5f;

            // �浹 �� ���� ����� �÷��̾ �ٽ� ���
            Player nearestPlayer = GetNearestPlayer();
            if (nearestPlayer != null)
            {
                Debug.Log($"���� ����� �÷��̾�: {nearestPlayer.name}");
            }
        }
    }

     void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && unitList.Contains(player))
        {
            unitList.Remove(player);

            // �÷��̾ �����鼭 ���� ����� �÷��̾ �ٽ� ���
            Player nearestPlayer = GetNearestPlayer();
            if (nearestPlayer != null)
            {
                Debug.Log($"���� ����� �÷��̾�: {nearestPlayer.name}");
            }
        }
    }
}