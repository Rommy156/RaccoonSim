using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject trashPrefab;
    private int lastSpawnIndex = -1;
    public TextMeshProUGUI counterText;
    private int counter;
    private HungerSystem hungerSystem;

    void Start()
    {
        counter = 0;
        SpawnTrash();
    }

    void SpawnTrash()
    {
        int newSpawnIndex;
        do
        {
            newSpawnIndex = Random.Range(0, spawnPoints.Length);
        } while (newSpawnIndex == lastSpawnIndex);
        lastSpawnIndex = newSpawnIndex;
        Instantiate(trashPrefab, spawnPoints[newSpawnIndex].position, Quaternion.identity);
    }

    public void OnTrashCollected()
    {
        counter++;
        SpawnTrash();
        GameOver();
        UpdateCounter();
    }


    void UpdateCounter()
    {
        counterText.text = "Counter: " + counter.ToString();
    }

   void GameOver()
    {
        
    }
}