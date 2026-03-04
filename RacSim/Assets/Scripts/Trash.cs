using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public float hungerIncrease = 5f;

    private ObjectSpawner spawner;
    public GameObject trashPrefab;
    void Start()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    public void OnTriggerEnter(Collider other)
    {
        HungerSystem hunger = other.GetComponent<HungerSystem>();

        if (other.CompareTag("Player"))
        {
            if (spawner != null)
            {
                spawner.OnTrashCollected();
                Debug.Log("+1");

            }
            gameObject.SetActive(false);

            // Add hunger increase to the player's hunger system
            if (hunger != null)
            {
                hunger.AddHunger(hungerIncrease);
                Destroy(gameObject);
            }
        }
    }
}