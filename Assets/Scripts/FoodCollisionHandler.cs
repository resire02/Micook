using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FoodCollisionHandler : MonoBehaviour
{
    public GameObject parent;
    public Transform orientation;
    public float foodScale;
    
    public float cookDuration;
    float foodTimer;
    bool timerIsRunning;
    
    GameObject microwaveFood;

    void Start()
    {
        timerIsRunning = false;
    }

    void Update()
    {
        if(microwaveFood != null)
            microwaveFood.transform.rotation = orientation.rotation;

        //  TODO: Remove This
        if(Input.GetKey(KeyCode.Q))
            RemoveFood();

        if(timerIsRunning)
            checkTimer();
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

            createTimer();
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

    private void createTimer()
    {
        foodTimer = cookDuration;
        timerIsRunning = true;
    }

    private void checkTimer()
    {
        foodTimer -= Time.deltaTime;

        Debug.Log(foodTimer);

        if(foodTimer < 0f)
        {
            timerIsRunning = false;
            Debug.Log("Food is Ready!");
        }
    }
}
