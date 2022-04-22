using UnityEngine;
using System;

public enum BoxType
{
    small,
    medium,
    big,
    none
}
public class CharacterInteractive : MonoBehaviour
{
    [SerializeField] private ButtonController _buttonController;

    public static Action<Shelf> OnShelfTriggerEnter;
    public static Action<Action> OnInterectivObjectTriggerEnter;
    public static Action<BoxType> OnBoxTriggerEnter;

    private Action _interactionAction;
    private Shelf _shelf;

    private void OnEnable()
    {
        OnInterectivObjectTriggerEnter += InteractiveObjectTrigger;
        OnShelfTriggerEnter += ShelfTrigger;
        OnBoxTriggerEnter += BoxTrigger;
    }

    private void OnDisable()
    {
        OnInterectivObjectTriggerEnter -= InteractiveObjectTrigger;
        OnShelfTriggerEnter -= ShelfTrigger;
        OnBoxTriggerEnter -= BoxTrigger;
    }

    private void ShelfTrigger(Shelf shelf)
    {
        _shelf = shelf;
    }

    private void BoxTrigger(BoxType boxType)
    {
        _buttonController.SetPickUpButton(boxType);
    }

    private void InteractiveObjectTrigger(Action action)
    {
        _buttonController.SetActionButton(action);
    }
}
