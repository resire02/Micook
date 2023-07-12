using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public virtual string ViewMessage() 
    {
        return string.Empty;
    }

    public virtual string InteractMessage() 
    {
        return string.Empty;
    }

    public void BaseStartInteract()
    {
        // Debug.Log("Start Interaction");
        StartInteract();
    }

    public void BaseCancelInteract()
    {
        // Debug.Log("End Interaction");
        CancelInteract();
    }

    protected virtual void StartInteract() {}

    protected virtual void CancelInteract() {}

}
