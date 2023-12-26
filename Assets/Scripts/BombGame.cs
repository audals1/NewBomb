using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 폭탄돌리기
/// 5 명의 캐릭터를 a,b,c,d,e 라고 하자.
/// 폭탄은 최초에 a 에게 전달되고, a는 폭탄이 터지기 전에 누군가에게 랜덤하게 폭탄을 전달한다.
/// 폭턴의 최초 타이머는 5초이다.
/// 폭탄의 타이머가 0이 되면 폭탄은 터지고, 폭탄을 받은 캐릭터는 폭사한다
/// 폭탄을 마지막에 넘긴 캐릭터는 승리한다.
/// 폭탄을 가진 캐릭터는 0.3초 이후에 추가로 0.0 ~ 1.0 사이의 랜덤한 초가 경과한 훙에 폭탄을 넘긴다.
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
        Debug.Log("대기중");
        await Task.Delay(randomDelay);
        Debug.Log("대기끝");
        currentIndex = GetBombHolderIndex();
        winnerIndex = currentIndex;
        
        Debug.Log($"승리예정{winnerIndex}");
        Debug.Log($"폭사예정{currentIndex}");
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
