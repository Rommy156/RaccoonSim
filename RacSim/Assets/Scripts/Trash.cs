using UnityEngine;

public class Trash : MonoBehaviour
{
    public GameObject artifactPrefab; 
    public GameObject foodPrefab;       
    [Range(0, 100)] public float artifactDropChance = 15f; 
    private ObjectSpawner spawner;
    private bool isLooted = false;

    void Start()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLooted)
        {
            isLooted = true;
            Loot();
        }
    }

    void Loot()
    {
        float roll = Random.Range(0f, 100f);
        GameObject itemToSpawn = (roll <= artifactDropChance) ? artifactPrefab : foodPrefab;

        if (itemToSpawn != null)
        {
            Instantiate(itemToSpawn, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        if (spawner != null)
        {
            spawner.OnTrashCollected();
        }

        Debug.Log(itemToSpawn.name + " spawned from trash!");

        Destroy(gameObject);

    }

}