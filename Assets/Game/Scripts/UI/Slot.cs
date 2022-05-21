using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemAmountText;
    [SerializeField] private TextMeshProUGUI _itemWeightText;

    [SerializeField] private Item _nowItem;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (_nowItem.item != null)
            SlotDraggable.OnBeginDrag(this);
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        SlotDraggable.OnEndDrag();
    }

    public void UpdateInfo(Item item)
    {
        _nowItem = item;

        if (_nowItem.item == null)
        {
            ClearSlot();
            return;
        }

        ChangeStatus(true);

        _itemIcon.sprite = _nowItem.item.Icon;

        _itemAmountText.text = _nowItem.amount.ToString();
        _itemWeightText.text = string.Format("{0} kg", _nowItem.amount * _nowItem.item.Weight);
    }

    public void ClearSlot()
    {
        ChangeStatus(false);

        _itemAmountText.text = "0";
    }

    public Item GetItem()
    {
        return _nowItem;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SlotDraggable.OnSetSlot(this);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SlotDraggable.OnSetSlot(null);

    }

    private void ChangeStatus(bool status)
    {
        _itemIcon.enabled = status;
        _itemWeightText.enabled = status;
        _itemAmountText.enabled = status;
    }
}
