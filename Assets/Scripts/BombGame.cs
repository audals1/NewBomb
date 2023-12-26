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
    public DetectUnit[] detectUnits;
    float time = 0.0f;
    float timeLimit = 5.0f;
    public int winnerIndex = -1;
    public int currentIndex = 0;
    

    void Start()
    {
        bomb = FindObjectOfType<Bomb>();
        detectUnits = FindObjectsOfType<DetectUnit>();
        playerList = FindObjectsOfType<Player>();
        InitGame();
    }

    void Update()
    {
        Timer();
        BombPass();
    }


    /*async void BombPass()
    {
        int randomDelay = Random.Range(0, 300);
        Debug.Log("�����");
        await Task.Delay(randomDelay);
        Debug.Log("��ⳡ");
        currentIndex = GetBombHolderIndex();
        winnerIndex = currentIndex;
        
        Debug.Log($"�¸�����{winnerIndex}");
        Debug.Log($"���翹��{currentIndex}");
    }*/
    void Timer()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString();
        if (time <= 0.0f) time = timeLimit;
    }
    void BombPass()
    {
        currentIndex = GetBombHolderIndex();
        SetBombPos(currentIndex);
        Debug.Log(currentIndex);
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
        int randomIndex = Random.Range(0, playerList.Length);
        Debug.Log($"random index : {randomIndex}");
        SetBombPos(randomIndex);
        playerList[randomIndex].SetBomb(true);
    }

    void SetBombPos(int index)
    {
        bomb.transform.position = playerList[index].transform.GetChild(0).position;
        bomb.transform.SetParent(playerList[index].transform);
    }
}
