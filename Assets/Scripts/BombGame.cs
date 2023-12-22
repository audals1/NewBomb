using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ź������
/// 5 ���� ĳ���͸� a,b,c,d,e ��� ����.
/// ��ź�� ���ʿ� a ���� ���޵ǰ�, a�� ��ź�� ������ ���� ���������� �����ϰ� ��ź�� �����Ѵ�.
/// ������ ���� Ÿ�̸Ӵ� 5���̴�.
/// ��ź�� Ÿ�̸Ӱ� 0�� �Ǹ� ��ź�� ������, ��ź�� ���� ĳ���ʹ� �����Ѵ�
/// ��ź�� �������� �ѱ� ĳ���ʹ� �¸��Ѵ�.
/// ��ź�� ���� ĳ���ʹ� 0.3�� ���Ŀ� �߰��� 0.0 ~ 1.0 ������ ������ �ʰ� ����� �˿� ��ź�� �ѱ��.
/// </summary>

public class BombGame : MonoBehaviour
{
    public Player[] playerList;
    public Bomb bomb;
    public Text timeText;
    public Text winnerText;
    public Text loserText;
    public float time;
    int winnerIndex = -1;
    int currentIndex = 0;
    float timeLimit = 5.0f;

    void Start()
    {
        bomb = FindObjectOfType<Bomb>();
        playerList = FindObjectsOfType<Player>();
        InitGame();
    }

    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString();   
    }

    async void BombPass()
    {
        int randomDelay = Random.Range(0, 4000);
        Debug.Log("�����");
        await Task.Delay(randomDelay);
        Debug.Log("��ⳡ");
        var randomIndex = -1;
        currentIndex = GetBombHolderIndex();
        winnerIndex = currentIndex;
        do
        {randomIndex = Random.Range(0, playerList.Length);
        } while (currentIndex == randomIndex);
        currentIndex = randomIndex;
        playerList[winnerIndex].SetBomb(false);
        playerList[currentIndex].SetBomb(true);
        var setTransform = playerList[currentIndex].transform.GetChild(0);
        bomb.transform.position = setTransform.position;
        Debug.Log($"�¸�����{winnerIndex}");
        Debug.Log($"���翹��{currentIndex}");
    }

    int GetBombHolderIndex()
    {
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].HasBomb())
            {
                return i;
            }
        }
        return -1;
    }

    void InitGame()
    {
        for (int i = 0; i < playerList.Length; i++)
        {
            playerList[i].SetBomb(false);
        }
        time = timeLimit;
        bomb.transform.position = playerList[0].transform.GetChild(0).position;
        playerList[0].SetBomb(true);
    }
}
