using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCThreat : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < detectionRange)
        {
            Debug.Log("NPC sees the player!");
        }
    }
}
