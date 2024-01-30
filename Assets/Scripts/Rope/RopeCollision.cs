using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.Addons.KCC;

public class RopeCollision : KCCProcessor
{
    public override void OnEnter(KCC kcc, KCCData data)
    {
        if (kcc.IsInFixedUpdate == false) return;

        Debug.Log("Ãæµ¹!!!!!!!!!!!!!!!!!!!!");
    }
}
