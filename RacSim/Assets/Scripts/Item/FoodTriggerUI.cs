//Allen Adepoju
//000948096
using UnityEngine;
using TMPro;

public class FoodTriggerUI : MonoBehaviour
{
    public GameObject infoPanel;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI instructionText;

    private bool playerInside = false;
    private FoodItem food;

    void Start()
    {
        infoPanel.SetActive(false);
        food = GetComponent<FoodItem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            titleText.text = food.foodName;
            hungerText.text = "Hunger Restored: " + food.hungerRestored;
            instructionText.text = "Press SPACE to Eat";

            infoPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
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
        Debug.Log("Food eaten");

        infoPanel.SetActive(false);

        Destroy(gameObject);
    }
}