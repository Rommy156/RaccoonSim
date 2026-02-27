using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemSpawner : MonoBehaviour
{
    //an array to hold variable of all transform spawn points 
    public Transform[] spawnPoints;
    //reference to treasure prefab , which is a gameObject our player will be collecting
    public GameObject treasurePrefab;
    //reference to the UI text for coin coounting
    public TextMeshProUGUI counterText;
    // -1 means "does not exist" it is -1 because nothing has been spawned yet.
    private int lastSpawnIndex = -1;
    //variable to store how much coin has been collected
    private int counter;

    void Start()
    {
        //player has 0 coins at the start of the game
        counter = 0;
        //call updateCounter function
        UpdateCounter();
        //call SpawTreasure function
        SpawnTreasure();
    }

    //This function gets called on Start() to spawn the first item. It will also have to be called in OnTreasureCollected()
    void SpawnTreasure()
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
        Instantiate(treasurePrefab, spawnPoints[newSpawnIndex].position, Quaternion.identity);
    }

    //A public function can accessed by other scripts. INTERESTING.
    public void OnTreasureCollected()
    {
        //increment coin counter if treasureCollected and call UpdateCounter(), SpawnTreasure(), GameOver();
        counter++;
        UpdateCounter();
        SpawnTreasure();
        GameOver();

    }

    void UpdateCounter()
    {
        //update counterText UI and display text
       // counterText.text = "Counter: " + counter.ToString();
    }

    void GameOver()
    {
        //if the counter reaches 10 load the gameOver scene
        if (counter == 10)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

}