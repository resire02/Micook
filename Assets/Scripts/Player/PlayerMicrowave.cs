using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMicrowave : MonoBehaviour
{
    public float cookThreshold = 20f;
    public float burnThreshold = 40f;

    private bool isMicrowaving = false;
    private GameObject microwavedFood = null;
    private readonly Vector3 SCALAR_DEFAULT = new Vector3(.25f, .25f, .25f);
    private readonly Vector3 TRANSFORM_DEFAULT = new Vector3(0.2f, -.6f, .1f);
    private float microwaveTimer = 0;
    private PlayerScore score;
    private PlayerObjective objective;
    private AudioHandler aux;

    private void Start()
    {
        score = GetComponent<PlayerScore>();
        objective = GetComponent<PlayerObjective>();
        aux = GetComponent<AudioHandler>();
    }

    private void FixedUpdate()
    {
        if(isMicrowaving && microwavedFood)
        {
            //  rotates the food like a microwave :D
            microwavedFood.transform.Rotate(Vector3.up);

            microwaveTimer += Time.deltaTime;
            microwaveTimer = Mathf.Clamp(microwaveTimer, 0, burnThreshold);

            if(microwaveTimer < cookThreshold)
            {
                aux.PlaySound("MicrowaveAmbient", true);
            }
            else if(microwaveTimer < burnThreshold)
            {
                aux.PlaySound("Cooked", true);
            }
            else
            {
                aux.PlaySound("Burning", true);
            }

        }
    }

    public void MicrowaveFood(string foodName)
    {
        isMicrowaving = true;

        microwavedFood = (GameObject) Instantiate(
            LoadObjectFromResources(foodName),
            transform.position,
            Quaternion.identity,
            transform
        );

        microwavedFood.transform.localScale = SCALAR_DEFAULT;
        microwavedFood.transform.localPosition = TRANSFORM_DEFAULT;
    }

    public bool CheckIsMicrowaving()
    {
        return isMicrowaving;
    }

    private UnityEngine.Object LoadObjectFromResources(string food)
    {
        return Resources.Load($"Models/Food/{food}");
    }

    public void SubmitFood()
    {
        if(!isMicrowaving) return;
        
        aux.StopSound("Cooked");
        aux.StopSound("Burning");
        aux.StopSound("MicrowaveAmbient");
        aux.PlaySound("MicrowaveEnd");

        if(microwaveTimer < cookThreshold)
            score.CalculateCookScore(PlayerScore.CookStatus.UNCOOKED);
        else if(microwaveTimer < burnThreshold)
            score.CalculateCookScore(PlayerScore.CookStatus.COOKED);
        else
            score.CalculateCookScore(PlayerScore.CookStatus.BURNT);

        objective.DecrementFoodCount();

        DestroyFood();
    }

    public void DisposeFood()
    {
        if(!isMicrowaving) return;
        
        aux.StopSound("Cooked");
        aux.StopSound("Burning");
        aux.StopSound("MicrowaveAmbient");
        aux.PlaySound("Disposed");

        score.CalculateCookScore(PlayerScore.CookStatus.DISPOSED);

        objective.DecrementFoodCount();

        DestroyFood();
    }

    private void DestroyFood()
    {
        Destroy(microwavedFood);
        isMicrowaving = false;
        microwavedFood = null;
        microwaveTimer = 0;
    }
}
