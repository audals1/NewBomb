using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 랜덤으로 움직이다가 적하고 일정거리이상 가까워지면 적과 반대방향으로 튄다를 구현하려면??
/// 벡터3 디스턴스 값이 일정값보다 작으면으로 분기를 걸까?
/// 나 아닌 다른 사람하고 부딪히면 HasBomb false 닿은놈 other은 true가 된다
/// 폭탄 넘기기를 내가 결정할까 아니면 충돌로 체크할까
/// 1.충돌로 체크할 경우
/// 내가 폭탄을 가졌는지 분기를 나누고 처리
/// 기존룰 0.3초 + 0~1초 랜덤 동안 처리 대기하는 거 처리
/// 
/// 
/// </summary>

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    float minX, maxX, minZ, maxZ;
    public bool hasBomb = false;
    float interval = 1.5f;
    float time;
    Vector3 randomDirection;
    Vector3 againstDirection;
    public GameObject map;
    public Renderer mapRender;
    public BombGame bombGame;


    private void Awake()
    {
        bombGame = FindObjectOfType<BombGame>();
    }

    void Start()
    {
        MapSetting();
        randomDirection = GetRandomDirection();
        againstDirection = GetAgainstBombDirection();
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > interval)
        {
            randomDirection = GetRandomDirection();
            againstDirection = GetAgainstBombDirection();
            time = 0f;
        }
        if (hasBomb)
        {
            MoveRandomly();
        }
        else MoveAgainstBomb();
    }

    public bool HasBomb()
    {
        return hasBomb;
    }

    public void SetBomb(bool value)
    {
        hasBomb = value;
    }

    void MapSetting()
    {
        map = GameObject.Find("Plane");
        mapRender = map.GetComponent<Renderer>();
        Vector3 planeSize = mapRender.bounds.size;

        minX = map.transform.position.x - planeSize.x * 0.5f;
        maxX = map.transform.position.x + planeSize.x * 0.5f;
        minZ = map.transform.position.z - planeSize.z * 0.5f;
        maxZ = map.transform.position.z + planeSize.z * 0.5f;
    }

    Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    Vector3 GetAgainstBombDirection()
    {
        againstDirection = bombGame.playerList[bombGame.GetBombHolderIndex()].transform.position - transform.position;
        return -againstDirection;
    }

    void MoveRandomly()
    {
        Vector3 newPosition = transform.position + randomDirection * moveSpeed * Time.deltaTime;

        
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }

    void MoveAgainstBomb()
    {
        Vector3 position = transform.position + againstDirection * moveSpeed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        transform.position = position;
    }
}
