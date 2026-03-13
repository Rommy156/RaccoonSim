//Allen Adepoju
//000948096
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject infoPanel; // UI panel above item
    private bool playerInside = false;
    private FoodItem food;
    private HungerSystem hungerSystem;

    void Start()
    {
        food = GetComponent<FoodItem>();

        // Find HungerSystem in scene
        hungerSystem = FindObjectOfType<HungerSystem>();

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
        // Add hunger to HungerSystem
        if (food != null && hungerSystem != null)
        {
            hungerSystem.AddHunger(food.hungerRestored);
        }

        if (infoPanel != null)
            infoPanel.SetActive(false);

        Destroy(gameObject);
    }
}