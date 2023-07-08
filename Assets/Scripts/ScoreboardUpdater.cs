using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreboardUpdater : MonoBehaviour
{
    public int score = 0;
    private Text scoreboardPoints;

    // Start is called before the first frame update
    void Start()
    {
        //scoreboardPoints = GetComponent("Text") as Text;
    }

    // Update is called once per frame
    void Update()
    {
        //scoreboardPoints.text = "Score: " + score.ToString();
    }
}
