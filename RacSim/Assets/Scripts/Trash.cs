//Allen Adepoju
//000948096
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    // call randomObectSpawner class and rename it so we can access any public element of the script
    private ObjectSpawner spawner;
    //reference the gameObject treasurePrefab
    public GameObject treasurePrefab;
    void Start()
    {
        //find randomObjectSpawner and assign it the spawner variable
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    public void OnTriggerEnter(Collider other)
    {
        //check if the collider that entered has the player tag
        if (other.CompareTag("Player"))
        {
            //display a message on debug and notify spawner and destroy gameObject 
            if (spawner != null)
            {
                spawner.OnTreasureCollected();
                Debug.Log("+1");

            }

            gameObject.SetActive(false);


        }
    }
}