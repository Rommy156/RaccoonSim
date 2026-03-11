using UnityEngine;
public class ItemEffect : MonoBehaviour
{
    public enum ItemType { Consumable, Artifact }
    public ItemType type;
    public string itemName;
    //stat rewards 
    public float hungerBonus = 20f;
    public float energyBonus = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
        }
    }

    void ApplyEffect(GameObject player)
    {
        if (type == ItemType.Consumable)
        {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.Eat(hungerBonus, energyBonus);
                Debug.Log(itemName + " consumed! +Hunger, +Energy");
            }
        }
        else if (type == ItemType.Artifact)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddArtifact();
                Debug.Log(itemName + " has been added to the journal!");
            }
        }

        Destroy(gameObject);

    }

}