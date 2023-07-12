using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Interactable
{
    private PlayerMicrowave microwave;

    private void Start()
    {
        microwave = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMicrowave>();
    }

    public override string ViewMessage()
    {
        return "Press E to serve food.";
    }

    public override string InteractMessage()
    {
        return ViewMessage();
    }

    protected override void StartInteract() 
    {
        microwave.SubmitFood();
    }
}
