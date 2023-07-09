using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingHandler : MonoBehaviour
{
    public TMP_Text textObject;

    public void CheckWin()
    {
        if(transform.gameObject.GetComponentsInChildren<Transform>().Length <= 1)
        {
            EndGame();
        }
    }

    public void LoseGame()
    {
        textObject.SetText("You Ran Out Power!");

        Invoke(nameof(EndText), 5f);

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    private void EndGame()
    {
        textObject.SetText("You\'re Winner!");

        Invoke(nameof(EndText), 5f);

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    private void EndText()
    {
        SceneManager.LoadScene("Menu");

        Cursor.lockState = CursorLockMode.None;
        
        Cursor.visible = true;
    }

}
