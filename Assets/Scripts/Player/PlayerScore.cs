using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [HideInInspector] public enum CookStatus { DISPOSED, UNCOOKED, COOKED, BURNT };
    [SerializeField] private float startingScore = 0f;
    public TextMeshProUGUI scoreText;

    [Header("Score Configuration")]
    public float disposedScore = -5;
    public float uncookedScore = 1;
    public float cookedScore = 5;
    public float burntScore = 2;
    
    private void Start()
    {
        scoreText.text = $"Score: {startingScore}";
    }

    public void CalculateCookScore(CookStatus status)
    {
        switch(status)
        {
            case CookStatus.DISPOSED:
                startingScore += disposedScore;
                break;
            case CookStatus.UNCOOKED:
                startingScore += uncookedScore;
                break;
            case CookStatus.COOKED:
                startingScore += cookedScore;
                break;
            case CookStatus.BURNT:
                startingScore += burntScore;
                break;
        }

        startingScore = Mathf.Max(startingScore, 0);
        scoreText.text = $"Score: {startingScore}";
    }

    public float GetScore()
    {
        return startingScore;
    }

}
