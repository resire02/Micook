using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    private string foodName;
    private PlayerMicrowave microwave;
    private AudioHandler aux;

    private void Start()
    {
        foodName = transform.gameObject.name;
        microwave = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMicrowave>();
        aux = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioHandler>();
    }

    protected override void StartInteract()
    {
        if(!microwave.CheckIsMicrowaving())
        {
            microwave.MicrowaveFood(foodName);

            aux.PlaySound("MicrowaveStart");

            Destroy(transform.gameObject);
        }
    }

    public override string ViewMessage() 
    {
        return $"Press E to microwave {foodName}";
    }

    public override string InteractMessage()
    {
        return ViewMessage();
    }

}
