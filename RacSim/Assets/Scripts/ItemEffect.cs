using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public enum ItemType { Consumable, Artifact }
    public ItemType type;
    public string itemName;
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
                //dynamically showing values - green color
                Debug.Log($"<color=green>[ITEM]</color> {itemName} consumed! +{hungerBonus} Hunger, +{energyBonus} Energy.");
            }
        }
        else if (type == ItemType.Artifact)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddArtifact();
                //artifacts - gold color
                Debug.Log($"<color=gold>[ARTIFACT]</color> {itemName} has been added to the journal!");
            }
        }
        //clean up the object from the scene
        Destroy(gameObject);

    }

}