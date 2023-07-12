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

    private void Start()
    {
        power = GetComponent<PlayerPower>();
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
    
    }

    private void TiggerLoseGame()
    {

    }

    public void DecrementFoodCount()
    {
        foodRemaining--;
        objectiveText.text = $"Food Remaining: {foodRemaining}";
    }
}
