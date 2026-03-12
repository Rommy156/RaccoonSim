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
        if (!other.CompareTag("Player")) return;

        if (type == ItemType.Consumable)
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                stats.Eat(hungerBonus, energyBonus);
            }
        }
        else if (type == ItemType.Artifact)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddArtifact();
            }
        }

        Destroy(gameObject);
    }
}