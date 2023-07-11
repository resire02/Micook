using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    public float chargeCapacity = 100f;
    public float powerDrainingRate = 0.5f;
    public float powerChargingRate = 10f;
    private float power;
    private float rate;

    private void Start()
    {
        power = chargeCapacity;
        rate = powerDrainingRate;
    }

    private void Update()
    {
        power -= rate * Time.deltaTime;
        power = Mathf.Clamp(power, 0, chargeCapacity);

        Debug.Log((int) power);
    }

    public void ToggleChargeStatus(bool status)
    {
        if(status)
            rate = -powerChargingRate;
        else
            rate = powerDrainingRate;
    }
    
}
