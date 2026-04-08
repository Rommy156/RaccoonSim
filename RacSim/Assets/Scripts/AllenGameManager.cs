//Allen Adepoju
//000948096
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllenGameManager : MonoBehaviour
{
    [Header("Artifact System")]
    public ArtifactManager artifactManager;
    public GameObject artifactItems;

    public static AllenGameManager Instance;

    public int collectedItems = 0;
    public int itemsToWin = 6;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (Transform child in artifactItems.transform)
        {
            ArtifactPickup artifact = child.GetComponent<ArtifactPickup>();

            artifact.OnArtifactCollected.AddListener(artifactManager.AddArtifact);
        }

    }
    public void AddItem()
    {
        collectedItems++;

        if (collectedItems >= itemsToWin)
        {
            SceneManager.LoadScene("Win");
        }
    }
}