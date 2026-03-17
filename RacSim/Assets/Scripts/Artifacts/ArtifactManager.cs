using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArtifactManager : MonoBehaviour
{
    public UnityEvent OnArtifactsChanged;

    public int maxArtifacts = 6;

    public List<Artifact> artifacts = new List<Artifact>();

    void Awake()
    {
        if (OnArtifactsChanged == null)
        {
            OnArtifactsChanged = new UnityEvent();
        }

        // ensure it starts empty
        artifacts.Clear();
    }

    public void AddArtifact(Artifact artifact)
    {
        if (artifacts.Count >= maxArtifacts)
        {
            Debug.Log("Artifact slots full");
            return;
        }

        artifacts.Add(artifact);

        Debug.Log("Picked up artifact: " + artifact.name);

        OnArtifactsChanged.Invoke();
    }
}