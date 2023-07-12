using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerObjective : MonoBehaviour
{
    public int foodRemaining = 12;
    public TextMeshProUGUI objectiveText;

    private PlayerPower power;
    private AudioHandler aux;

    private void Start()
    {
        power = GetComponent<PlayerPower>();
        aux = GetComponent<AudioHandler>();

        objectiveText.text = $"Food Remaining: {foodRemaining}";
    }

    private void FixedUpdate() 
    {
        if(foodRemaining == 0)
            TiggerWinGame();
        else if(power.OutOfPower())
            TiggerLoseGame();
    }

    private void TiggerWinGame()
    {
        aux.PlaySound("GameWin");
        objectiveText.text = "Congratulations, you prepared dinner!";
        Invoke(nameof(TransitionScene), 3f);
    }

    private void TiggerLoseGame()
    {
        aux.PlaySound("GameLose");
        objectiveText.text = "Uh oh, you ran out of power!";
        Invoke(nameof(TransitionScene), 3f);
    }

    public void DecrementFoodCount()
    {
        foodRemaining--;
        objectiveText.text = $"Food Remaining: {foodRemaining}";
    }

    private void TransitionScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Menu");
    }
}
