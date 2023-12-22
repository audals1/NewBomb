using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectUnit : MonoBehaviour
{
    List<Player> unitList = new List<Player>();
    public List<Player> UnitList { get { return unitList; } }


    public Player GetMonsterNearest(Vector3 myPos) //가장 가까운 다른 캐릭터를 리스트에서 찾음
    {
        if (unitList.Count == 0) return null;

        float maxDist = (myPos - unitList[0].transform.position).sqrMagnitude;
        int index = 0;
        for (int i = 1; i < unitList.Count; i++)
        {
            var dist = (myPos - unitList[i].transform.position).sqrMagnitude;
            if (maxDist < dist)
            {
                maxDist = dist;
                index = i;
            }
        }
        return unitList[index];
    }

    

    void OnTriggerEnter(Collider other)
    {
        //Player contactUnit = other.attachedRigidbody.GetComponent<Player>();
        unitList.Add(other.gameObject.GetComponent<Player>());
        Debug.Log(unitList.Count);
        
    }
    void OnTriggerExit(Collider other)
    {
        unitList.Remove(other.gameObject.GetComponent<Player>());
        Debug.Log(unitList.Count);
    }
}
