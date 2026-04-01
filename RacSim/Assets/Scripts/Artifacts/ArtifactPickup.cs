using UnityEngine;
using UnityEngine.Events;

public class ArtifactPickup : MonoBehaviour
{
    public Artifact artifact;

    public UnityEvent<Artifact> OnArtifactCollected;

    void Awake()
    {
        if (OnArtifactCollected == null)
        {
            OnArtifactCollected = new UnityEvent<Artifact>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnArtifactCollected.Invoke(artifact);

            Destroy(gameObject);
        }
    }
}