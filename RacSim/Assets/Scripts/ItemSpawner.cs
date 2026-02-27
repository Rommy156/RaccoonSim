using UnityEngine;
public class ItemSpawner : MonoBehaviour
{
    //spawn settings
    public GameObject itemPrefab;
    public float spawnTime = 5f; //spawn intervsl
    private float timer;
    public float rangeX = 10f; //spawn area width
    public float rangeZ = 10f; //depth


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    void SpawnItem()
    {
        float randomX = Random.Range(-rangeX, rangeX);
        float randomZ = Random.Range(-rangeZ, rangeZ);
        Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ);
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }
}
      