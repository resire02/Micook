using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 2f;
    public LayerMask interactLayer;

    private Camera cam;
    private InputManager inputManager;

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        // Debug.DrawRay(transform.position, Vector3.forward * interactDistance);
        if(Physics.Raycast(cam.transform.position, cam.transform.forward * interactDistance, out var hitCollider, interactDistance, interactLayer))
        {
            // Debug.Log($"Looking at {hitCollider.transform.gameObject.name}");

            if(inputManager.CheckInteractTriggered())
            {
                Debug.Log("Interacting");
            }
        }
    }
}
