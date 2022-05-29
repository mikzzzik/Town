using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class SlotHolder : Slot, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _equipText;

    private bool _equipStatus;

    public void OnPointerDown(PointerEventData eventData)
    {
        ContextMenuUI.OnHide();

        if (_nowItem.ItemObject != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right && _slotType == SlotType.Inventory)
            {
                ContextMenuUI.OnShowContext(eventData.position, this);   
            }
            else
            {
                SlotDraggable.OnBeginDrag(this);
            }
        }
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData eventData)
    {
        SlotDraggable.OnEndDrag();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SlotDraggable.OnSetSlot(this);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SlotDraggable.OnSetSlot(null);
    }

    public void ChangeEquipStatus()
    {
        _equipStatus = !_equipStatus;

        _equipText.SetActive(_equipStatus);
    }

    public bool GetEquipStatus()
    {
        return _equipStatus;
    }

}
