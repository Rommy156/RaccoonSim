using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // This function will be called by the UI button
    public void RestartScene()
    {
        // Optional, affects current scene briefly
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("Start");
        
    }
}