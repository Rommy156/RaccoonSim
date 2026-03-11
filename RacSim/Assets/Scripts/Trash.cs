//Allen Adepoju
//000948096
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TrashAmount : MonoBehaviour
{
    public float hungerIncrease = 10f;


    public void OnTriggerEnter(Collider other)
    {
        HungerSystem hunger = other.GetComponent<HungerSystem>();

        if (other.CompareTag("Player"))
        {
            
            // Add hunger increase to the player's hunger system
            if (hunger != null)
            {
                hunger.AddHunger(hungerIncrease);
                Destroy(gameObject);
            }
        }
    }
}