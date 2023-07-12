using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 2f;
    public LayerMask interactLayer;
    [SerializeField] private TextMeshProUGUI visualText;

    private Camera cam;
    private InputManager inputManager;
    private Interactable interactObject;
    private bool isLookingAtInteractable;
    private bool isInteracting = false;

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        // Debug.DrawRay(transform.position, Vector3.forward * interactDistance);
        
        isLookingAtInteractable = Physics.Raycast(cam.transform.position, cam.transform.forward * interactDistance, out var hitCollider, interactDistance, interactLayer);

        if(isLookingAtInteractable)
        {
            // Debug.Log($"Looking at {hitCollider.transform.gameObject.name}");
            interactObject = hitCollider.collider.GetComponent<Interactable>();
            visualText.text = isInteracting ? interactObject.InteractMessage() : interactObject.ViewMessage();
        }
        else
        {
            //  clears the visual hint
            visualText.text = string.Empty;

            //  ensure the interactObject does not go null when still interacting
            if(!isInteracting) interactObject = null;
            else
            {
                //  cancel interaction if not looking at the object
                isInteracting = false;
                interactObject.BaseCancelInteract();
            }
        }
    }

    public void StartInteract()
    {
        //  check if looking at object and object exists
        if(isLookingAtInteractable && interactObject)
        {
            isInteracting = true;
            interactObject.BaseStartInteract();
        }
    }

    public void CancelInteract()
    {
        //  cancel interaction if iteracting
        if(isInteracting)
        {
            isInteracting = false;
            interactObject.BaseCancelInteract();
        }
    }
}
