using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� �����̴ٰ� ���ϰ� �����Ÿ��̻� ��������� ���� �ݴ�������� Ƥ�ٸ� �����Ϸ���??
/// ����3 ���Ͻ� ���� ���������� ���������� �б⸦ �ɱ�?
/// �� �ƴ� �ٸ� ����ϰ� �ε����� HasBomb false ������ other�� true�� �ȴ�
/// ��ź �ѱ�⸦ ���� �����ұ� �ƴϸ� �浹�� üũ�ұ�
/// 1.�浹�� üũ�� ���
/// ���� ��ź�� �������� �б⸦ ������ ó��
/// ������ 0.3�� + 0~1�� ���� ���� ó�� ����ϴ� �� ó��
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
