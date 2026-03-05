using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepZone : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerStats playerStats;
    public float energyRestoreAmount = 100f;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Rest();
            }
        }
    }
    void Rest()
    {
        playerStats.RestoreEnergy(energyRestoreAmount); //for restore energy
        gameManager.AdvanceDay();
        Debug.Log("You rested.Next day!");
    }
}
