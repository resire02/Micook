using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject fp;
    public GameObject tp;

    void Update()
    {
        if(Input.GetButtonDown("1Key")){
            fp.SetActive(true);
            tp.SetActive(false);
        }
        if(Input.GetButtonDown("2Key")){
            fp.SetActive(false);
            tp.SetActive(true);
        }
    }
}
