using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //game state
    public bool isGameActive = true;
    public bool hasWon = false;
    //day and night loop
    public int currentDay = 1;
    public float dayTimer = 0f;
    public float maxDayDuration = 300f; //5 minutes
    public int artifactsInJournal = 0; 
    public int artifactsHeldThisNight = 0; 
    public int totalArtifactsRequired = 10;
    public float finalScore = 0f;
    
    public PlayerStats playerStats;
    public SkillTree skillTree;
    public Transform spawnPoint; //racoon starting point
    public GameObject playerObject;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!isGameActive || hasWon) return;
        //energy will deplete faster as the night progresses
        dayTimer += Time.deltaTime;

        CheckStats();
        if (dayTimer >= maxDayDuration)
        {
            EndNight(true); 
        }
    }

    void CheckStats()
    {
        if (playerStats != null && playerStats.IsDead())
        {
            HandlePlayerDeath();
        }
    }

    public void HandlePlayerDeath()
    {
        Debug.Log("Energy/Hunger Depleted! Artifacts lost for this night.");

        artifactsHeldThisNight = 0;
        EndNight(false); 
    }

    public void EndNight(bool savedArtifacts)
    {
        if (savedArtifacts)
        {
            artifactsInJournal += artifactsHeldThisNight;
            Debug.Log("Night ended safely. Artifacts saved to journal.");
        }

        artifactsHeldThisNight = 0;
        dayTimer = 0f;
        currentDay++;

        // reward skill points on day advance
        if (skillTree != null) skillTree.AddSkillPoint(1);

        //return to spawn
        ResetPlayerToSpawn();

        if (artifactsInJournal >= totalArtifactsRequired)
        {
            WinGame();
        }
    }

    void ResetPlayerToSpawn()
    {
        playerObject.transform.position = spawnPoint.position;
        playerStats.ResetStats(); 
        Debug.Log("Returned to spawn. Day: " + currentDay);
    }

    public void AddArtifact()
    {
        artifactsHeldThisNight++;
        Debug.Log("Artifact found! Total this night: " + artifactsHeldThisNight);
    }

    void WinGame()
    {
        hasWon = true;
        isGameActive = false;
        finalScore = 1000f / currentDay;

        Debug.Log("WIN! Score: " + finalScore + " Nights: " + currentDay);
    }
}