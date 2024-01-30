using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Example;

public class PlayerSpawner : NetworkBehaviour
{
    public GameObject PlayerPrefab;
    [Networked] public int NetworkedPlayerIndex { get; set; } = -1;

    public override void FixedUpdateNetwork()
    {
        if (Runner.IsServer == true)
        {
            // With Client-Server topology only the Server spawn player objects.
            // PlayerManager is a special helper class which iterates over list of active players (NetworkRunner.ActivePlayers) and call spawn/despawn callbacks on demand.
            PlayerManager<PlayerControl>.UpdatePlayerConnections(Runner, SpawnPlayer, DespawnPlayer);
            
        }
    }

    private void SpawnPlayer(PlayerRef playerRef)
    {
        SpawnPoint[] spawnPoints = Runner.SimulationUnityScene.GetComponents<SpawnPoint>(false);

        Transform spawnPoint = spawnPoints[NetworkedPlayerIndex].transform;

        NetworkObject player = Runner.Spawn(PlayerPrefab, spawnPoint.position, spawnPoint.rotation, playerRef);

        Runner.SetPlayerObject(playerRef, player);

        Runner.SetPlayerAlwaysInterested(playerRef, player, true);

        Debug.Log($"Player ID : {playerRef.PlayerId}");
    }

    private void DespawnPlayer(PlayerRef playerRef, PlayerControl player)
    {
        Object.ReleaseStateAuthority();
        Runner.Despawn(player.Object);
    }

    public override void Spawned()
    {
        if (Runner.GameMode == GameMode.Shared)
        {
            NetworkedPlayerIndex++;
            SpawnPlayer(Runner.LocalPlayer);
            Debug.Log($"SpwanIndex : {NetworkedPlayerIndex}");
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        NetworkedPlayerIndex--;
        var player = PlayerPrefab.GetComponent<PlayerControl>();
        DespawnPlayer(Runner.LocalPlayer, player);
        Debug.Log($"SpwanIndex : {NetworkedPlayerIndex}");
    }
}
