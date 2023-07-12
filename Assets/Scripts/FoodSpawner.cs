using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FoodSpawner : MonoBehaviour
{
    // public LayerMask foodLayer;
    public float xOffset, yOffset, zOffset;
    public float collisionRadius;

    private List<Transform> spawnList = new List<Transform>();
    private List<string> foodList  = new List<string>();
    private System.Random rand = new System.Random();
    private readonly string[] foodNameList = {"Apple", "Pizza", "Tuna", "Turkey"};

    readonly Vector3 TRANSFORM_DEFAULT = new Vector3(1f, 1f, 1f);
    Dictionary<string, Vector3> foodScalar = new Dictionary<string, Vector3>();

    void Start()
    {
        //  TODO: add food scalars here
        // foodScalar.Add("Turkey", new Vector3(1f, 1f, 1f));
        foodScalar.Add("Pizza", new Vector3(3f, 3f, 3f));
        foodScalar.Add("Tuna", new Vector3(2.5f, 4f, 2.5f));

        PopulateSpawnList();

        RegisterFoods(foodNameList);

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

            Destroy(spawn.gameObject);
        }
    }

    private void SpawnInitial()
    {
        foreach(Transform loc in spawnList)
        {
            GameObject obj = (GameObject) Instantiate(
                GetObject(foodList[rand.Next(foodList.Count)]),
                new Vector3(
                    loc.position.x + xOffset,
                    loc.position.y + yOffset,
                    loc.position.z + zOffset
                ),
                Quaternion.identity
            );

            obj.layer = LayerMask.NameToLayer("Interactable");

            obj.AddComponent<Food>();

            obj.transform.SetParent(transform);

            obj.transform.localScale = foodScalar.TryGetValue(obj.name, out var scalar) ? scalar : TRANSFORM_DEFAULT;

            SphereCollider sc = obj.AddComponent(typeof(SphereCollider)) as SphereCollider;

            sc.radius = Mathf.Abs(collisionRadius);

            obj.name = obj.name.Replace("(Clone)","");

            obj.tag = "food";
        }

    }

    private void RegisterFoods(string[] foods)
    {
        foreach(string food in foods) foodList.Add(food);
    }

    private UnityEngine.Object GetObject(string food)
    {
        return Resources.Load($"Models/Food/{food}");
    }

}
