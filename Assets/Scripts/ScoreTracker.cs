using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreTracker
{
    static float highestScore = 0;
    
    public static void AddScore(float score)
    {
        highestScore = Mathf.Max(highestScore, score);

        Debug.Log($"Added a score of {score} to the leaderboards!");
    }

    public static float GetHighScore()
    {
        return highestScore;
    }

}
