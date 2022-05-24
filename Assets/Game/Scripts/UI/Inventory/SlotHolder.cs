using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SlotHolder : Slot, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (_nowItem.ItemObject != null)
            SlotDraggable.OnBeginDrag(this);
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
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

}
