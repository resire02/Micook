using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System;

public class FoodCollisionHandler : MonoBehaviour
{
    //  Properties
    [Header("Setup")]
    public GameObject parent;
    public Transform orientation;
    public float foodScale;
    public TMP_Text visualHint;
    
    [Header("Cooking Process")]
    public int cookDuration;   //  this is how long it takes to cook
    public int burnDuration;      //  this is how much longer it takes to burn
    float foodTimer;
    bool timerIsRunning;
    float totalCookingTime;

    [Header("Scores")]
    public int rawFoodScore;
    public int cookedFoodScore;
    public int burntFoodScore;

    [Header("Debug")]
    public TMP_Text timerText;
    readonly Vector3 TRANSFORM_DEFAULT = new Vector3(0f, -0.3f, 0f);
    
    GameObject microwaveFood;
    Dictionary<string, Vector3> foodScalar = new Dictionary<string, Vector3>();
    AudioHandler aux;
    GameEndingHandler gameEnd;

    void Start()
    {
        aux = FindObjectOfType<AudioHandler>();
        gameEnd = FindObjectOfType<GameEndingHandler>();
        
        timerIsRunning = false;

        timerText.SetText("");

        totalCookingTime = cookDuration + burnDuration;

        if(cookDuration <= 0 || burnDuration <= 0) throw new ArgumentException("Neither cookDuration nor burnDuration can be less than or equal to zero");

        //  TODO: add food scalars here
        foodScalar.Add("Turkey", new Vector3(-.05f, -0.3f, 0f));
    
    }

    void Update()
    {
        if(microwaveFood != null)
            microwaveFood.transform.rotation = orientation.rotation;

        // //  TODO: Remove This
        // if(Input.GetKey(KeyCode.Q))
        //     RemoveFood();

        if(timerIsRunning)
            checkTimer();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("food") && !timerIsRunning)
        {
            // Debug.Log(other.gameObject.name);

            aux.PlaySound("MicrowaveStart");
            aux.PlaySound("MicrowaveAmbient", true);

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
        else if(other.gameObject.CompareTag("plate") && timerIsRunning)
        {
            RemoveFood();
            RegisterScore();
        }
    }

    private UnityEngine.Object getObject(string food)
    {
        return Resources.Load($"Models/Food/{food}");
    }

    private void RegisterScore()
    {
        int time = (int) Mathf.Floor(foodTimer);

        // Debug.Log(time);

        aux.StopSound("MicrowaveAmbient");
        aux.PlaySound("MicrowaveEnd");

        ScoreboardUpdater sb = FindObjectOfType<ScoreboardUpdater>();

        if(time < cookDuration)
        {
            Debug.Log("Food is Raw");
            sb.score += rawFoodScore;
        }
        else if(time < totalCookingTime)
        {
            Debug.Log("Food is Cooked");
            sb.score += cookedFoodScore;
        }
        else
        {
            Debug.Log("Food is Burnt");
            sb.score += burntFoodScore;
        }

        gameEnd.DecrementFoodCount();
        gameEnd.CheckWin();
    }

    private void RemoveFood()
    {
        if(microwaveFood == null) return;

        GameObject.Destroy(microwaveFood);
        microwaveFood = null;
        timerText.SetText("");
        timerIsRunning = false;
    }

    private void createTimer()
    {
        foodTimer = 0;
        timerIsRunning = true;
    }

    private void checkTimer()
    {
        foodTimer += Time.deltaTime;
        foodTimer = Mathf.Clamp(foodTimer, 0.0f, totalCookingTime);

        // Debug.Log(foodTimer);

        timerText.SetText($"{Mathf.Floor(foodTimer)}");

        ////////////////////////
        //  TODO: add event system to trigger model change
        ////////////////////////

    }

    private void ClearModel()
    {
        Debug.Log("Cleared Model");
    }
}