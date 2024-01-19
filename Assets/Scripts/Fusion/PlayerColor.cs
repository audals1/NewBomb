using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerColor : NetworkBehaviour
{
    public MeshRenderer _meshRenderer;
    private ChangeDetector _changeDetector;

    [Networked]
    public Color NetworkedColor { get; set; }

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }

    void Update()
    {
        if (HasStateAuthority)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            }

            foreach (var change in _changeDetector.DetectChanges(this))
            {
                switch (change)
                {
                    case nameof(NetworkedColor):
                        _meshRenderer.material.color = NetworkedColor;
                        break;
                }
            }
        }
        
    }
}
