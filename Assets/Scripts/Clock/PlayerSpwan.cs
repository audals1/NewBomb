using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpwan : MonoBehaviour
{
    public List<SpwanPoint> SpwanPoints = new List<SpwanPoint>();
    public List<PlayerControl> Players = new List<PlayerControl>();
    void Awake()
    {
        InitSpwan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitSpwan()
    {
        SpwanPoint[] points = FindObjectsOfType<SpwanPoint>();
        foreach (SpwanPoint item in points)
        {
            SpwanPoints.Add(item);
        }

        PlayerControl[] players = FindObjectsOfType<PlayerControl>();
        foreach (PlayerControl player in players)
        {
            Players.Add(player);
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].transform.position = SpwanPoints[i].transform.position;
        }
    }
}
