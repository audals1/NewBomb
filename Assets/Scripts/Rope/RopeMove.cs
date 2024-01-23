using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMove : MonoBehaviour
{
    public GameObject rope;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotate = new Vector3(rotationSpeed * Time.deltaTime, 0f, 0f);
        rope.transform.Rotate(rotate);    
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"충돌 오브젝트{other.name}");

    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"나간 오브젝트{other.name}");
    }
}
