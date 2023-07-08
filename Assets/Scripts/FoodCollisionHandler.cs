using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class FoodCollisionHandler : MonoBehaviour
{
    [Header("Options")]
    public GameObject parent;
    public Transform orientation;
    public float foodScale;
    
    public float cookDuration;
    float foodTimer;
    bool timerIsRunning;
    public TMP_Text timerText;
    readonly Vector3 TRANSFORM_DEFAULT = new Vector3(0f, -0.3f, 0f);
    
    GameObject microwaveFood;
    Dictionary<string, Vector3> foodScalar = new Dictionary<string, Vector3>();

    void Start()
    {
        timerIsRunning = false;

        timerText.SetText("");

        //  TODO: add food scalars here
        foodScalar.Add("Turkey", new Vector3(-.05f, -0.3f, 0f));

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
        if(other.gameObject.CompareTag("food") && !timerIsRunning)
        {
            // Debug.Log(other.gameObject.name);

            microwaveFood = (GameObject) Instantiate(
                getObject(other.gameObject.name),
                parent.transform.position,
                Quaternion.identity
            );

            microwaveFood.transform.SetParent(parent.transform);
            microwaveFood.transform.localScale = new Vector3(foodScale, foodScale, foodScale);

            microwaveFood.transform.localPosition = foodScalar.TryGetValue(other.gameObject.name, out var scalar) ? scalar : TRANSFORM_DEFAULT;

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

        // Debug.Log(foodTimer);

        timerText.SetText($"{Mathf.Floor(foodTimer)}");

        if(foodTimer < 0f)
        {
            timerIsRunning = false;

            Debug.Log("Food is Ready!");

            timerText.SetText("");

            RemoveFood();
        }
    }
}
