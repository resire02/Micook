using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public Slider slider;
    private float xRotation = 0f;
    private PlayerPerspective perspective;

    void Start()
    {
        perspective = GetComponent<PlayerPerspective>();
        slider.value = 60f;
    }

    public void HandleLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * slider.value;
        xRotation = Mathf.Clamp(xRotation, -60f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * slider.value);
    }

}
