using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectSpawner : MonoBehaviour
{
    //an array to hold variable of all transform spawn points 
    public Transform[] spawnPoints;
    //reference to trash prefab , which is a gameObject our player will be collecting
    public GameObject trashPrefab;
    // -1 means "does not exist" it is -1 because nothing has been spawned yet.
    private int lastSpawnIndex = -1;
    //reference to the UI text for trash counting
    public TextMeshProUGUI counterText;
    //variable to store how much coin has been collected
    private int counter;

    void Start()
    {
        //player has 0 coins at the start of the game
        counter = 0;
        //call SpawTreasure function
        SpawnTrash();
    }

    //This function gets called on Start() to spawn the first item. It will also have to be called in OnTreasureCollected()
    void SpawnTrash()
    {
        //temp variable to store our new spawn point
        int newSpawnIndex;
        do
        {
            //the range of where we want our next spawn points (min,max)
            newSpawnIndex = Random.Range(0, spawnPoints.Length);
        } while (newSpawnIndex == lastSpawnIndex);
        lastSpawnIndex = newSpawnIndex;
        //instantiate the treasurePrefab at the new spawn point's position and rotation
        Instantiate(trashPrefab, spawnPoints[newSpawnIndex].position, Quaternion.identity);
    }

    //A public function can accessed by other scripts. INTERESTING.
    public void OnTrashCollected()
    {
        //increment coin counter if treasureCollected and call UpdateCounter(), SpawnTreasure(), GameOver();
        counter++;
        SpawnTrash();
        GameOver();
        UpdateCounter();
    }


    void UpdateCounter()
    {
        //update counterText UI and display text
        counterText.text = "Counter: " + counter.ToString();
    }

   void GameOver()
    {
       //If the exterminator collides with racoon load game over scene

    }
}