using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameActive = true;
    public bool hasWon = false;
    public int currentDay = 1;

    public int artifactsInJournal = 0;
    public int artifactsHeldThisNight = 0;
    public int totalArtifactsRequired = 10;

    public PlayerStats playerStats;
    public Transform spawnPoint;
    public GameObject playerObject;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!isGameActive) return;

        if (playerStats != null && playerStats.IsDead())
        {
            HandlePlayerDeath();
        }
    }

    public void HandlePlayerDeath()
    {
        Debug.Log("Player died. Artifacts lost.");

        artifactsHeldThisNight = 0;
        ResetPlayerToSpawn();
    }

    public void EndNight(bool savedArtifacts)
    {
        if (savedArtifacts)
        {
            artifactsInJournal += artifactsHeldThisNight;
        }

        artifactsHeldThisNight = 0;
        currentDay++;

        ResetPlayerToSpawn();
    }

    void ResetPlayerToSpawn()
    {
        if (playerObject == null || spawnPoint == null) return;

        Rigidbody rb = playerObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        playerObject.transform.position = spawnPoint.position;

        if (playerStats != null)
            playerStats.ResetStats();
    }

    public void AddArtifact()
    {
        artifactsHeldThisNight++;
        Debug.Log("Artifact collected. Total this night: " + artifactsHeldThisNight);
    }
}