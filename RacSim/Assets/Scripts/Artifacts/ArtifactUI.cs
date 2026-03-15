using UnityEngine;
using UnityEngine.UI;

public class ArtifactUI : MonoBehaviour
{
    public ArtifactManager artifactManager;
    public Image[] slots;
    public Sprite emptySlot;

    void Start()
    {
        artifactManager.OnArtifactsChanged.AddListener(UpdateUI);

        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < artifactManager.artifacts.Count)
            {
                slots[i].sprite = artifactManager.artifacts[i].icon;
            }
            else
            {
                slots[i].sprite = emptySlot;
            }
        }
    }
}