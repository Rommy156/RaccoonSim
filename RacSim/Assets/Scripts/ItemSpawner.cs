using UnityEngine;
public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject itemPrefab;
    public int maxItems = 10;
    public Vector3 spawnAreaSize = new Vector3(20f, 1f, 20f);
    private int currentItemCount = 0;

    private void Start()
    {
        SpawnInitialItems();
    }

    void SpawnInitialItems()
    {
        for (int i = 0; i < maxItems; i++)
        {
            SpawnItem();
        }
    }
    void SpawnItem()
    {
        Vector3 randomPosition = new Vector3(
        Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
        0f,
        Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        Vector3 spawnPosition = transform.position + randomPosition;
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        currentItemCount++;
    }

    public void itemCollected()
    {
        currentItemCount--;
        if (currentItemCount < maxItems)
        {
            SpawnItem();
        }
    }
    
}