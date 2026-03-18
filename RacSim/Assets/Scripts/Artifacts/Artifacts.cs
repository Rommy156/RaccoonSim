using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts/Artifact")]
public class Artifact : ScriptableObject
{
    public string artifactName;
    public Sprite icon;

    [Range(0f, 1f)]
    public float dropRate;

    [TextArea]
    public string description;
}