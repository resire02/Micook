using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerspective : MonoBehaviour
{
    [SerializeField] private Camera firstPerson;
    [SerializeField] private Camera thirdPerson;

    private enum PlayerView { FirstPerson, ThirdPerson };
    private PlayerView currentView = PlayerView.FirstPerson;
    private AudioHandler aux;

    private void Start()
    {
        SwitchToPerspective(currentView);
        aux = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioHandler>();
    }

    public void TogglePerspective()
    {
        if(currentView == PlayerView.FirstPerson)
            currentView = PlayerView.ThirdPerson;
        else
            currentView = PlayerView.FirstPerson;

        Debug.Log($"Switched to {currentView.ToString()}");

        SwitchToPerspective(currentView);

        aux.PlaySound("CameraClick");
    }

    private void SwitchToPerspective(PlayerView view)
    {
        if(view == PlayerView.FirstPerson)
        {
            thirdPerson.gameObject.SetActive(false);
            firstPerson.GetComponent<AudioListener>().enabled = true;
            thirdPerson.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            thirdPerson.gameObject.SetActive(true);
            firstPerson.GetComponent<AudioListener>().enabled = false;
            thirdPerson.GetComponent<AudioListener>().enabled = true;
        }
    }

    public bool InSpectatorView()
    {
        return currentView == PlayerView.ThirdPerson;
    }
}
