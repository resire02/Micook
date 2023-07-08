using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEventHandler : MonoBehaviour
{
    public void SwitchToScene(string sceneName)
    {
       SceneManager.LoadScene(sceneName); 

       Debug.Log("Loaded New Scene");
    }

    public void QuitApp()
    {
        Application.Quit();

        Debug.Log("Quitting Game");
    }
}
