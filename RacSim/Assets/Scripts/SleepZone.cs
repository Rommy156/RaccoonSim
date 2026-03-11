//Check again
using UnityEngine;

public class SleepZone : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance; 
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            TrySleep();
        }
    }

    void TrySleep()
    {
        if (gameManager == null) return;

        Debug.Log("You entered the sleep zone. Saving artifacts and ending the night.");

        gameManager.EndNight(true);
    }
}