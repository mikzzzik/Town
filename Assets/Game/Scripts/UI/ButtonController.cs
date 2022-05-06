using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private Button _pickUpButton;
    [SerializeField] private Button _openContainerButton;
    [SerializeField] private Button _inventoryButton;

    [SerializeField] private ContainerUI _containerUI;
    [SerializeField] private InventoryUI _inventoryUI;

    private Action _actionButonAction;
    private BoxType _boxType;
    private Container _container;


    public static Action<Container> OnContainerTriggerEnter;
    public static Action<Action> OnInterectivObjectTriggerEnter;
    public static Action<BoxType> OnBoxTriggerEnter;


    private void OnEnable()
    {
        OnContainerTriggerEnter += SetContainerButton;
        OnInterectivObjectTriggerEnter += SetActionButton;
        OnBoxTriggerEnter += SetPickUpButton;
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        _actionButton.onClick.AddListener(ActionButtonClick);
        _pickUpButton.onClick.AddListener(PickUpButtonClick);
        _openContainerButton.onClick.AddListener(OpenContainerClick);
    }

    private void SetActionButton(Action action)
    {
        if (action == null) _actionButton.gameObject.SetActive(false);
        else _actionButton.gameObject.SetActive(true);

        _actionButonAction = action;
    }

    private void SetContainerButton(Container container)
    {
        if (container == null) _openContainerButton.gameObject.SetActive(false);
        else _openContainerButton.gameObject.SetActive(true);

        _container = container;
    }

    private void SetPickUpButton(BoxType boxType)
    {
        if (boxType == BoxType.none) _pickUpButton.gameObject.SetActive(false);
        else _pickUpButton.gameObject.SetActive(true);

        _boxType = boxType;
    }

    private void OpenContainerClick()
    {
        _containerUI.InitSlots(_container);
    }

    private void PickUpButtonClick()
    {
        CharacterInventory.OnChooseBox(_boxType);
    }

    private void ActionButtonClick()
    {
        _actionButonAction();
    }
}
