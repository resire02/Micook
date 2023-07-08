using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollision : MonoBehaviour
{
    public int itemPoints = 10;

    private void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("food")){
            Debug.Log("hit");
        }
    }
}
