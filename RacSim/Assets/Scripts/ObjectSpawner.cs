using UnityEngine;
using TMPro;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TrashTierData
    {
        public string tierName;
        public GameObject trashPrefab;
        [Range(0, 100)] public float spawnChance;
    }

    public Transform[] spawnPoints;
    public TrashTierData[] trashTiers; 

    public TextMeshProUGUI counterText;
    private int counter = 0;
    private int lastSpawnIndex = -1;

    void Start()
    {
        if (trashTiers == null || trashTiers.Length == 0)
        {
            Debug.LogError("ATTENTION: Trash Tiers list is empty! Add prefabs.");
            return;
        }

        for (int i = 0; i < 3; i++) SpawnTrash();
        UpdateCounter();
    }

    public void SpawnTrash()
    {
        if (spawnPoints.Length == 0 || trashTiers.Length == 0) return;

        int newSpawnIndex;
        do
        {
            newSpawnIndex = Random.Range(0, spawnPoints.Length);
        } while (newSpawnIndex == lastSpawnIndex && spawnPoints.Length > 1);

        lastSpawnIndex = newSpawnIndex;
        GameObject selectedPrefab = GetRandomTierPrefab();

        if (selectedPrefab != null)
        {
            Vector3 pos = spawnPoints[newSpawnIndex].position + Vector3.up * 0.5f;
            Instantiate(selectedPrefab, pos, Quaternion.identity);
        }
    }

    GameObject GetRandomTierPrefab()
    {
        float totalChance = 0;
        foreach (var tier in trashTiers) totalChance += tier.spawnChance;
        if (totalChance <= 0) return trashTiers[0].trashPrefab;

        float randomRoll = Random.value * totalChance;
        float cumulative = 0;
        foreach (var tier in trashTiers)
        {
            cumulative += tier.spawnChance;
            if (randomRoll <= cumulative) return tier.trashPrefab;
        }
        return trashTiers[0].trashPrefab;
    }

    public void OnTrashCollected()
    {
        counter++;
        UpdateCounter();
        SpawnTrash();
    }

    void UpdateCounter()
    {
        if (counterText != null)
            counterText.text = "Trash Looted: " + counter.ToString();
    }
}