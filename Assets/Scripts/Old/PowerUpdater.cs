using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PowerUpdater : MonoBehaviour
{
    [Header("Power Options")]
    public TMP_Text powerText;
    public float startingPower;
    public float powerDrainingRate;
    public float powerChargingRate;
    float currentPower;
    bool isCharging;
    GameEndingHandler gameEnd;

    void Start()
    {
        gameEnd = FindObjectOfType<GameEndingHandler>();

        if(startingPower <= 0) 
            throw new ArgumentException("Power cannot be zero or a negative number");
        if(powerDrainingRate < 0)
            throw new ArgumentException("Power Draining Rate cannot be a negative number");

        currentPower = startingPower;

        powerText.SetText($"Power: {(int) currentPower}%");
    }

    void FixedUpdate()
    {
        if(isCharging)
            currentPower += powerChargingRate * Time.deltaTime;
        else
            currentPower -= powerDrainingRate * Time.deltaTime;

        currentPower = Mathf.Clamp(currentPower, 0f, startingPower);

        powerText.SetText($"Power: {(int) currentPower}%");

        if(currentPower == 0f)
        {
            gameEnd.LoseGame();
        }
    }

    public void SetChargingStatus(bool status)
    {
        isCharging = status;
    }

}
