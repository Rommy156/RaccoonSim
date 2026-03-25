//Allen Adepoju
//000948096
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject infoPanel; // UI panel above item
    private bool playerInside = false;
    private FoodItem food;
    private PlayerStats playerStats;
    public float hungerRestored;
    public float energyRestored;


    void Start()
    {
        food = GetComponent<FoodItem>();

        // Find PlayerStats in scene
        playerStats = FindObjectOfType<PlayerStats>();

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (infoPanel != null)
                infoPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (infoPanel != null)
                infoPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Space))
        {
            EatFood();
        }
    }

    void EatFood()
    {
        // Add hunger to PlayerStats
        if (food != null && playerStats != null)
        {
            playerStats.Eat (food.hungerRestored, 0f);
        }

        if (infoPanel != null)
            infoPanel.SetActive(false);

        Destroy(gameObject);
    }
}