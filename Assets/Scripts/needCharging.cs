using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class needCharging : MonoBehaviour
{
    public float minPowerLevel = 15f;
    public Image fadeImg;
    
    private PlayerPower pp;


    // Start is called before the first frame update
    void Start()
    {
        pp = FindObjectOfType<PlayerPower>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pp.PowerLevel() < minPowerLevel){
            float alpha = Mathf.InverseLerp(minPowerLevel, 0f, pp.PowerLevel());
            fadeImg.color = new Color(0f, 0f, 0f, alpha);
        }
        else{
            fadeImg.color = new Color(0f, 0f, 0f, 0f); 
        }
    }
}
