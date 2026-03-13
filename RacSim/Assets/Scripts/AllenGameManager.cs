//Allen Adepoju
//000948096
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllenGameManager : MonoBehaviour
{
    public static AllenGameManager Instance;

    public int collectedItems = 0;
    public int itemsToWin = 10;

    void Awake()
    {
        Instance = this;
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