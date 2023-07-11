using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlet : Interactable
{
    private PlayerPower power;

    private void Start()
    {
        power = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
    }

    protected override void StartInteract() 
    {
        power.SetChargeStatus(true);
    }

    protected override void CancelInteract() 
    {
        power.SetChargeStatus(false);
    }
}
