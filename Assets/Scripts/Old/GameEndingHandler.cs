using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingHandler : MonoBehaviour
{
    public TMP_Text textObject;
    private int foodRemaining;

    void Start()
    {
        //foodRemaining = transform.gameObject.GetComponentsInChildren<Transform>().Length;
        foodRemaining = 12;
    }

    public void CheckWin()
    {
        if(FoodRemaining() <= 0)
        {
            EndGame();
        }
    }

    public int FoodRemaining()
    {
        return foodRemaining;
    }

    public void DecrementFoodCount()
    {
        foodRemaining--;
    }

    void Update()
    {
        // Debug.Log(foodRemaining);
    }

    public void LoseGame()
    {
        AudioHandler aux = FindObjectOfType<AudioHandler>();
        aux.PlaySound("GameLose");
        textObject.SetText("You Ran Out Power!");

        Invoke(nameof(EndText), 5f);

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    private void EndGame()
    {
        AudioHandler aux = FindObjectOfType<AudioHandler>();
        aux.PlaySound("GameWin");
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
