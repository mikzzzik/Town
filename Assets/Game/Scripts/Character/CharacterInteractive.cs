using UnityEngine;
using System;

public class CharacterInteractive : MonoBehaviour
{
    public static Action<Action> OnInterectivObjectTriggerEnter;

    [SerializeField] private GameObject _interactiveObjectButton;

    private Action _interactionAction;

    private void OnEnable()
    {
        OnInterectivObjectTriggerEnter += InteractiveObjectTrigger;
    }

    private void OnDisable()
    {
        OnInterectivObjectTriggerEnter -= InteractiveObjectTrigger;
    }

    private void InteractiveObjectTrigger(Action action)
    {
        if(action == null)
        {
            _interactiveObjectButton.SetActive(false);
        }
        else
        {
            _interactiveObjectButton.SetActive(true);
            _interactionAction = action;
        }
    }
}
