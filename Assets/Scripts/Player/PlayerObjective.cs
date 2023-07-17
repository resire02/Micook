using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerObjective : MonoBehaviour
{
    public int foodRemaining = 12;
    public TextMeshProUGUI objectiveText;

    private bool gameEnded = false;
    private PlayerPower power;
    private AudioHandler aux;
    private PlayerScore score;

    private void Start()
    {
        power = GetComponent<PlayerPower>();
        aux = GetComponent<AudioHandler>();
        score = GetComponent<PlayerScore>();

        objectiveText.text = $"Food Remaining: {foodRemaining}";
    }

    private void FixedUpdate() 
    {
        if(foodRemaining == 0 && !gameEnded)
            TiggerWinGame();
        else if(power.OutOfPower() && !gameEnded)
            TiggerLoseGame();
    }

    private void TiggerWinGame()
    {
        gameEnded = true;
        aux.PlaySound("GameWin", false);
        objectiveText.text = "Congratulations, you prepared dinner!";
        ScoreTracker.AddScore(score.GetScore());
        Invoke(nameof(TransitionScene), 4f);
    }

    private void TiggerLoseGame()
    {
        gameEnded = true;
        aux.PlaySound("GameLose", false);
        objectiveText.text = "Uh oh, you ran out of power!";
        Invoke(nameof(TransitionScene), 4f);
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
