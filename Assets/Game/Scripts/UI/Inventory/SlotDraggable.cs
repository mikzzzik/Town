using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class SlotDraggable : MonoBehaviour
{
    [SerializeField] private Image _imageItem;
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private SlotHolder _currentSlot;
    [SerializeField] private SlotHolder _newSlot;

    public static Action<SlotHolder> OnBeginDrag;
    public static Action OnEndDrag;
    public static Action<SlotHolder> OnSetSlot;

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
    
    private void SetSlot(SlotHolder slot)
    {
        _newSlot = slot;
    }
    
    private void BeginDrag(SlotHolder slot)
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
            if (currentItem.ItemObject != newItem.ItemObject)
            {
                Debug.Log("Same");
                ItemSwitch();
            }
            else
            {
                Debug.Log("Different");
               newItem.Amount += currentItem.Amount;
                _currentSlot.GetItem().Clear();
            }
           

            currentInventoryPanelHolder.UpdateUI();
        }
        else
        {
            if(currentItem.ItemObject == newItem.ItemObject)
            {
                float availebleWeight = newInventoryPanelHolder.GetAvailableWeight();

                if (availebleWeight >= currentItem.ItemObject.Weight)
                {
                    if (availebleWeight >= currentItem.ItemObject.Weight * currentItem.Amount)
                    {
                        newItem.Amount += currentItem.Amount;
                        _currentSlot.GetItem().Clear();
                    }
                    else
                    {
                        int amountItem = (int)(availebleWeight / currentItem.ItemObject.Weight);

                        currentItem.Amount -= amountItem;
                        newItem.Amount += amountItem;
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
        currentItem.SetItem(newItem);
        newItem.SetItem(tempItem);

        Debug.Log("GG");

    }
}
