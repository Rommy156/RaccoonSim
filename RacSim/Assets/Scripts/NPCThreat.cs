using UnityEngine;

public class NPCThreat : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float npcDrainMultiplier = 2f;
    private PlayerStats playerStats;

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < detectionRange)
        {
            if (playerStats == null)
                playerStats = player.GetComponent<PlayerStats>();

            if (playerStats != null)
                playerStats.SetNPCMultiplier(npcDrainMultiplier);
        }
        else
        {
            if (playerStats != null)
                playerStats.SetNPCMultiplier(1f);
        }
    }
}