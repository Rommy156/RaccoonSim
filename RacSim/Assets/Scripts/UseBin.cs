using UnityEngine;

public class UseBin : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject artifactPrefab;

    [Range(0f, 1f)]
    public float artifactDropChance = 0.2f;

    private bool inReach;
    private bool opened = false;

    public GameObject interactUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;

            if (interactUI != null)
                interactUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;

            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E) && !opened)
        {
            opened = true;

            SpawnLoot();

            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }

    void SpawnLoot()
    {
        GameObject item;

        if (Random.value < artifactDropChance)
            item = artifactPrefab;
        else
            item = foodPrefab;

        Instantiate(item, transform.position + Vector3.up * 1f, Quaternion.identity);
    }
}