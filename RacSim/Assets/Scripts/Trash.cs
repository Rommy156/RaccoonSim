using UnityEngine;

public class Trash : MonoBehaviour
{
    public float hungerIncrease = 5f;

    private ObjectSpawner spawner;
    public GameObject LootPlaceholderPrefab;

    private void Start()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        HungerSystem hunger = other.GetComponent<HungerSystem>();

        if (spawner != null)
        {
            spawner.OnTrashCollected();
            Debug.Log("+1");
        }

        if (hunger != null)
        {
            hunger.AddHunger(hungerIncrease);
        }

        if (LootPlaceholderPrefab != null)
        {
            GameObject spawnedLoot = Instantiate(
            LootPlaceholderPrefab,
            transform.position + Vector3.up * 2f,
            Quaternion.identity
        );

        Rigidbody rb = spawnedLoot.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 randomSide = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized;

            Vector3 launchVelocity = Vector3.up * 5f + randomSide * 2f;
            rb.velocity = launchVelocity;
        }
        
        }
        else
        {
            Debug.LogWarning("LootPlaceholderPrefab is not assigned on " + gameObject.name);
        }

        Destroy(gameObject);
    }
}