using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
