using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteractionHandler : MonoBehaviour
{
    [Header("Key Binds")]
    public KeyCode interactKey = KeyCode.E;

    [Header("Functionality")]
    public LayerMask foodLayer;
    public LayerMask powerLayer;
    public float interactDistance;
    public TMP_Text visualHint;
    bool isLookingAtFood;
    bool isLookingAtPower;
    PowerUpdater powerScript;

    private void Start() 
    {
        isLookingAtFood = false;
        isLookingAtPower = false;

        visualHint.SetText("");

        powerScript = FindObjectOfType<PowerUpdater>();
    }

    private void Update()
    {
        isLookingAtFood = Physics.Raycast(transform.position, transform.forward, out var foodTarget, interactDistance, foodLayer);
        isLookingAtPower = Physics.Raycast(transform.position, transform.forward, out var powerTarget, interactDistance, powerLayer);
        
        if(isLookingAtFood)
        {
            // Debug.Log("Looking At Food");
            string FoodName = foodTarget.collider.gameObject.name;
            visualHint.SetText(FoodName);
        }
        else if(isLookingAtPower)
        {
            if(Input.GetKey(interactKey))
            {
                // Debug.Log("Charging");
                visualHint.SetText("Charging");
                powerScript.SetChargingStatus(true);
            }
            else
            {
                // Debug.Log("Looking At Power");
                visualHint.SetText($"Hold {interactKey} to Charge");
                powerScript.SetChargingStatus(false);
            }
        }
        else
        {
            visualHint.SetText("");
            powerScript.SetChargingStatus(false);
        }
    }

}
