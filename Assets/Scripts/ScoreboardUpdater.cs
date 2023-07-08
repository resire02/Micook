using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreboardUpdater : MonoBehaviour
{
    public TMP_Text scoreboardPoints;
    public int score = 10;

    // Start is called before the first frame update
    void Start()
    {
        scoreboardPoints.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        scoreboardPoints.text = "Score: " + score.ToString();
    }
}
