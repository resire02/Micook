using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteractionHandler : MonoBehaviour
{
    [Header("Key Binds")]
    public KeyCode interactKey = KeyCode.E;

    [Header("Functionality")]
    public LayerMask foodTag;
    public float interactDistance;
    public TMP_Text visualHint;
    bool isLookingAtFood;

    private void Start() 
    {
        isLookingAtFood = false;

        visualHint.SetText("");
    }

    private void Update()
    {
        isLookingAtFood = Physics.Raycast(transform.position, transform.forward, out var hit, interactDistance, foodTag);
        
        if(isLookingAtFood)
        {
            string FoodName = hit.collider.gameObject.name;
            visualHint.SetText(FoodName);
            ReadPlayerInput(FoodName);
        }
        else
        {
            visualHint.SetText("");
        }
    }

    private void ReadPlayerInput(string FoodName)
    {
        if(Input.GetKey(interactKey))
        {
            visualHint.SetText("Ate " + FoodName);
        }
    }
}
