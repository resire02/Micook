using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPower : MonoBehaviour
{
    public float chargeCapacity = 100f;
    public float powerDrainingRate = 0.5f;
    public float powerChargingRate = 10f;
    public TextMeshProUGUI powerText;

    private float power;
    private bool isCharging;

    private void Start()
    {
        power = chargeCapacity;
    }

    private void Update()
    {
        if(isCharging)
            power += (powerChargingRate * Time.deltaTime);
        else
            power -= (powerDrainingRate * Time.deltaTime);

        power = Mathf.Clamp(power, 0, chargeCapacity);

        powerText.text = $"Power: {(int) power}%";
    }

    public void SetChargeStatus(bool status)
    {
        isCharging = status;
    }
    
    public bool OutOfPower()
    {
        return power == 0;
    }
}
