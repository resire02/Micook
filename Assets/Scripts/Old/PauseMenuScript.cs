using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public KeyCode pauseKey;
    public GameObject pauseMenu;
    bool isPaused;

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if(Input.GetKey(pauseKey) && !isPaused)
        {
            Pause();
        }
    }

    private void Pause()
    {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
    }

    public void Resume()
    {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

}
