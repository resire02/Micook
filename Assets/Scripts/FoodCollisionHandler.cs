using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FoodCollisionHandler : MonoBehaviour
{
    public GameObject parent;
    public Transform orientation;
    public float foodScale;
    
    GameObject microwaveFood;

    void Update()
    {
        if(microwaveFood != null)
            microwaveFood.transform.rotation = orientation.rotation;

        //  TODO: Remove This
        if(Input.GetKey(KeyCode.Q))
            RemoveFood();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("food"))
        {
            // Debug.Log(other.gameObject.name);

            microwaveFood = (GameObject) Instantiate(
                getObject(other.gameObject.name),
                parent.transform.position,
                Quaternion.identity
            );

            microwaveFood.transform.SetParent(parent.transform);
            microwaveFood.transform.localScale = new Vector3(foodScale, foodScale, foodScale);

            microwaveFood.transform.localPosition = new Vector3(1f, -0.5f, 0f);

            GameObject.Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("plate"))
        {
            RemoveFood();
        }
    }

    private Object getObject(string food)
    {
        return Resources.Load($"Models/Food/{food}");
    }

    private void RemoveFood()
    {
        if(microwaveFood != null) return;

        GameObject.Destroy(microwaveFood);
        microwaveFood = null;
    }
}
