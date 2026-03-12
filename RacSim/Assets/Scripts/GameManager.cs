using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameActive = true;
    public bool hasWon = false;
    public int currentDay = 1;

    public float dayTimer = 0f;
    public float maxDayDuration = 300f;

    public int artifactsInJournal = 0;
    public int artifactsHeldThisNight = 0;
    public int totalArtifactsRequired = 10;
    public float finalScore = 0f;

    public PlayerStats playerStats;
    public SkillTree skillTree;
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
        if (!isGameActive || hasWon) return;

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
        Debug.Log("Energy/Hunger depleted! Artifacts lost.");

        artifactsHeldThisNight = 0;

        EndNight(false);
    }

    public void EndNight(bool savedArtifacts)
    {
        if (savedArtifacts)
        {
            artifactsInJournal += artifactsHeldThisNight;
            Debug.Log("Artifacts saved to journal.");
        }

        artifactsHeldThisNight = 0;

        dayTimer = 0f;
        currentDay++;

        if (skillTree != null)
            skillTree.AddSkillPoint(1);

        ResetPlayerToSpawn();

        if (artifactsInJournal >= totalArtifactsRequired)
        {
            WinGame();
        }
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

        Debug.Log("YOU WIN! Score: " + finalScore);
    }
}