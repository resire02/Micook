using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerspective : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;
    public enum CameraType { FIRST, THIRD };
    private CameraType cameraView = CameraType.FIRST;

    private void Start()
    {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
    }

    public void TogglePerspective()
    {
        switch(cameraView)
        {
            case CameraType.FIRST:
                firstPersonCamera.SetActive(false);
                thirdPersonCamera.SetActive(true);
                cameraView = CameraType.THIRD;
                break;
            case CameraType.THIRD:
                firstPersonCamera.SetActive(true);
                thirdPersonCamera.SetActive(false);
                cameraView = CameraType.FIRST;
                break;
        }
    }
}
