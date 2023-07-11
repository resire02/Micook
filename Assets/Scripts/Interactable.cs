using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string viewMessage;
    public string interactMessage;

    public void BaseStartInteract()
    {
        Debug.Log("Start Interaction");
        StartInteract();
    }

    public void BaseCancelInteract()
    {
        Debug.Log("End Interaction");
        CancelInteract();
    }

    protected virtual void StartInteract() {}

    protected virtual void CancelInteract() {}

}
