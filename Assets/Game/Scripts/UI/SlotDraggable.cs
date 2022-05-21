using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class SlotDraggable : MonoBehaviour
{
    [SerializeField] private Image _imageItem;
    [SerializeField] private RectTransform _rectTransform;

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
        _imageItem.sprite = _currentSlot.GetItem().item.Icon;

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
       Item currentItem  = _currentSlot.GetItem();
        Item newItem = _newSlot.GetItem();

        if (_newSlot == null || _newSlot == _currentSlot)
        {
            Clear();
            return;
        }
        InventoryPanelHolderUI currentInventoryPanelHolder = _currentSlot.transform.GetComponentInParent<InventoryPanelHolderUI>();
        InventoryPanelHolderUI newInventoryPanelHolder = _newSlot.transform.GetComponentInParent<InventoryPanelHolderUI>();

        if (currentInventoryPanelHolder == newInventoryPanelHolder)
        {
            Debug.Log(true);
            if (currentItem.item != newItem.item)
            {
                Debug.Log("Same");
                ItemSwitch();
            }
            else
            {
                Debug.Log("Different");
               newItem.amount += currentItem.amount;
                _currentSlot.GetItem().Clear();
            }
           

            currentInventoryPanelHolder.UpdateUI();
        }
        else
        {
            if(currentItem.item == newItem.item)
            {
                float availebleWeight = newInventoryPanelHolder.GetAvailableWeight();

                if (availebleWeight >= currentItem.item.Weight)
                {
                    if (availebleWeight >= currentItem.item.Weight * currentItem.amount)
                    {
                        newItem.amount += currentItem.amount;
                        _currentSlot.GetItem().Clear();
                    }
                    else
                    {
                        int amountItem = (int)(availebleWeight / currentItem.item.Weight);

                        currentItem.amount -= amountItem;
                        newItem.amount += amountItem;
                    }
                }
            }
            else if(currentInventoryPanelHolder.CheckWeight(currentItem, newItem) && newInventoryPanelHolder.CheckWeight(newItem, currentItem))
            {
                    ItemSwitch();            
            }
            else 
            {
                Debug.Log("Can't switch");
            }

            newInventoryPanelHolder.UpdateUI();
            currentInventoryPanelHolder.UpdateUI();
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
        Item currentItem = _currentSlot.GetItem();
        Item newItem = _newSlot.GetItem();

        Item tempItem = new Item();
        tempItem.SetItem(currentItem);
        Debug.Log(tempItem.item);
        currentItem.SetItem(newItem);
        Debug.Log(tempItem.item);
        newItem.SetItem(tempItem);
        Debug.Log(tempItem.item);

        Debug.Log("GG");

    }
}
