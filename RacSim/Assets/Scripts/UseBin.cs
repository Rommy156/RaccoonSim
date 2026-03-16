//Allen Adepoju
//000948096
using UnityEngine;

public class UseBin : MonoBehaviour
{
    private GameObject OB;

    public GameObject objToActivate;
    public GameObject[] items;

    private bool inReach;
    private bool hasSpawned = false;

    void Awake()
    {
        OB = this.gameObject;

        if (objToActivate != null)
            objToActivate.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            objToActivate.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            objToActivate.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E) && !hasSpawned)
        {
            hasSpawned = true;

            OB.GetComponent<Animator>().SetBool("isOpen", true);

            for (int i = 0; i < 5; i++)
            {
                int randomIndex = Random.Range(0, items.Length);

                Vector3 spawnPosition = transform.position + transform.up;

                GameObject item = Instantiate(items[randomIndex], spawnPosition, Random.rotation);

                Rigidbody rb = item.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 force =
                        transform.forward * Random.Range(1f, 3f) +
                        Vector3.up * Random.Range(-2f, 3f) +
                        transform.right * Random.Range(-3f, 3f);

                    rb.AddForce(force, ForceMode.Impulse);
                }
            }

            objToActivate.SetActive(false);

            OB.GetComponent<BoxCollider>().enabled = false;
        }
    }
}