using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FoodSpawner : MonoBehaviour
{
    public LayerMask foodLayer;
    private List<Transform> spawnList = new List<Transform>();
    private List<UnityEngine.Object> foodList = new List<UnityEngine.Object>();
    private System.Random rand = new System.Random();

    void Start()
    {
        PopulateSpawnList();

        PopulateFoodList();

        SpawnInitial();
    }

    private void PopulateSpawnList()
    {
        Transform[] transformList = transform.GetComponentsInChildren<Transform>(false);

        if(transformList == null) throw new Exception("No Objects to Spawn Food");

        foreach(Transform spawn in transformList)
        {
            if(GameObject.ReferenceEquals(transform.gameObject, spawn.gameObject))
                continue;

            spawnList.Add(spawn);

            // Debug.Log(spawn.gameObject.name);
        }
    }
    
    private void PopulateFoodList()
    {
        UnityEngine.Object[] foods = Resources.LoadAll("Models/Food");
        
        if(foods.Length == 0) throw new Exception("Resources were not found.");

        foreach(UnityEngine.Object obj in foods)
            foodList.Add(obj);
    }

    private void SpawnInitial()
    {
        foreach(Transform loc in spawnList)
        {
            GameObject obj = (GameObject) Instantiate(
                foodList[rand.Next(foodList.Count)],
                loc.position,
                Quaternion.identity
            );

            obj.layer = foodLayer;
        }

    }
}
