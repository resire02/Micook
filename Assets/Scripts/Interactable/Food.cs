using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    private string foodName;
    private PlayerMicrowave microwave;

    private void Start()
    {
        foodName = transform.gameObject.name;
        microwave = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMicrowave>();
    }

    protected override void StartInteract()
    {
        if(!microwave.CheckIsMicrowaving())
        {
            microwave.MicrowaveFood(foodName);

            Destroy(transform.gameObject);
        }
    }

    public override string ViewMessage() 
    {
        return $"Press E to microwave {foodName}";
    }

}
