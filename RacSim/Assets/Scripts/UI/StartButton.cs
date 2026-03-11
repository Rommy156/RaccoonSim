using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // This function will be called by the UI button
    public void StartScene()
    {
       
        
            SceneManager.LoadScene("Main");
        
    }
}