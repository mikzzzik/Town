using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private Button _pickUpButton;
    [SerializeField] private Button _openShelfButton;

    private Action _actionButonAction;
    private BoxType _boxType;
    private Shelf _shelf;

    void Start()
    {
        _actionButton.onClick.AddListener(ActionButtonClick);
        _pickUpButton.onClick.AddListener(PickUpButtonClick);
    }

    public void SetActionButton(Action action)
    {
        if (action == null) _actionButton.gameObject.SetActive(false);
        else _actionButton.gameObject.SetActive(true);

        _actionButonAction = action;
    }

    public void SetPickUpButton(Shelf shelf)
    {
        if (shelf == null) _openShelfButton.gameObject.SetActive(false);
        else _openShelfButton.gameObject.SetActive(true);

        _shelf = shelf;
    }

    public void SetPickUpButton(BoxType boxType)
    {
        if (boxType == BoxType.none) _pickUpButton.gameObject.SetActive(false);
        else _pickUpButton.gameObject.SetActive(true);

        _boxType = boxType;
    }

    private void OpenShelfClick()
    {
        
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
