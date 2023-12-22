using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectUnit : MonoBehaviour
{
    List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> UnitList { get { return unitList; } }


    public GameObject GetMonsterNearest(Vector3 target) //가장 가까운 다른 캐릭터를 리스트에서 찾음
    {
        if (unitList.Count == 0) return null;

        float maxDist = (target - unitList[0].transform.position).sqrMagnitude;
        int index = 0;
        for (int i = 1; i < unitList.Count; i++)
        {
            var dist = (target - unitList[i].transform.position).sqrMagnitude;
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
        if (other.CompareTag("OtherPlayer"))
        {
            unitList.Add(other.gameObject);
            for (int i = 0; i < unitList.Count; i++)
            {
                Debug.Log(unitList[i].name);
            }
        }
    }
}
