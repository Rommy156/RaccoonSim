using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform spawnPoint;
    public PlayerStats stats;

    void Update()
    {
        if (stats.IsDead())
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = spawnPoint.position;

        stats.hunger = stats.maxHunger;
        stats.energy = stats.maxEnergy;

        Debug.Log("Player Respawned");
    }
}
