using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.InputSystem;



public class SlotDraggable : MonoBehaviour
{
    [SerializeField] private Image _imageItem;

    [SerializeField] private Slot _currentSlot;
    [SerializeField] private Slot _newSlot;

    public static Action<Slot> OnBeginDrag;
    public static Action OnEndDrag;
    public static Action<Slot> OnSetSlot;

    private bool _status = false;

    private void OnEnable()
    {
        OnBeginDrag += BeginDrag;
        OnEndDrag += EndDrag;
        OnSetSlot += SetSlot;
    }

    private void OnDisable()
    {
        OnBeginDrag -= BeginDrag;
        OnEndDrag -= EndDrag;
        OnSetSlot -= SetSlot;
    }
    
    private void SetSlot(Slot slot)
    {
        _newSlot = slot;
    }
    
    private void BeginDrag(Slot slot)
    {
        _currentSlot = slot;

        _imageItem.gameObject.SetActive(true);
        _imageItem.sprite = _currentSlot.GetItem().ItemObject.Icon;

        _status = true;
    }

    private void Update()
    {
        if (_status)
            Drag();
    }

    private void Drag()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();

        _imageItem.transform.position = mousePos;
    }

    private void EndDrag()
    {
        if (_currentSlot == null || _newSlot == null || _newSlot == _currentSlot || _newSlot.GetSlotType() == SlotType.Workbench)
        {
            Clear();
            return;
        }

        Item currentItem = _currentSlot.GetItem();
        Item newItem = _newSlot.GetItem();

        PanelHolderUI curentPanelHolder = _currentSlot.transform.GetComponentInParent<PanelHolderUI>();
        PanelHolderUI newPanelHolder;

        if (_currentSlot.GetSlotType() != _newSlot.GetSlotType())
            newPanelHolder = _newSlot.transform.GetComponentInParent<PanelHolderUI>();
        else newPanelHolder = curentPanelHolder;

        if (_currentSlot.GetSlotType() == SlotType.Workbench)
        {
            newItem.SetItem(currentItem);

            newPanelHolder.UpdateUI();

            _currentSlot.ClearSlot();
        }
        else if (_currentSlot.GetSlotType() == _newSlot.GetSlotType())
        {
            if (newItem.ItemObject == null )
            {
                ItemSwitch();
            }
            else if (currentItem.ItemObject == newItem.ItemObject)
            {
                int availebleAmount = newItem.ItemObject.MaxAmount - newItem.Amount;

                int amount = availebleAmount > currentItem.Amount ? currentItem.Amount : availebleAmount;

                currentItem.Amount -= amount;
                newItem.Amount += amount;

                if (currentItem.Amount <= 0)
                    currentItem.Clear();
            }

            if(_currentSlot.GetSlotType() != SlotType.Workbench)
                curentPanelHolder.UpdateUI();
            newPanelHolder.UpdateUI();
        }

        Clear();
    }

    private void Clear()
    {
        _imageItem.gameObject.SetActive(false);
        _status = false;
      
    }

    private void ItemSwitch()
    {
        Item currentItemTemp = _currentSlot.GetItem();
        Item newItemTemp = _newSlot.GetItem();

        Item tempItem = new Item();

        tempItem.SetItem(currentItemTemp);
        currentItemTemp.SetItem(newItemTemp);
        newItemTemp.SetItem(tempItem);
    }
}
    