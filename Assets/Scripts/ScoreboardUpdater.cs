using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreboardUpdater : MonoBehaviour
{
    public TMP_Text scoreboardPoints;
    public TMP_Text remainingFoodCount;
    public int score = 10;
    GameEndingHandler gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        scoreboardPoints.text = "Score: ";
        gameEnd = FindObjectOfType<GameEndingHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreboardPoints.text = "Score: " + score.ToString();
        remainingFoodCount.text = $"Food Remaining: {gameEnd.FoodRemaining()}";
        // Debug.Log(gameEnd.FoodRemaining());
    }
}
