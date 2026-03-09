//check again
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SleepZone : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerStats playerStats;
    public float energyRestoreAmount = 100f;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Rest();
        }
    }

    void Rest()
    {
        if (playerStats != null)
        {
            playerStats.RestoreEnergy(energyRestoreAmount); //for restore energy
        }

        if (gameManager != null)
        {
            gameManager.AdvanceDay();
        }

        Debug.Log("You rested. Next day!");

    }
}
