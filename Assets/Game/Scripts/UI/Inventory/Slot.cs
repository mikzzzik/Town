using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] protected TextMeshProUGUI _itemAmountText;
    [SerializeField] private TextMeshProUGUI _itemWeightText;


    [SerializeField] protected SlotType _slotType;
    [SerializeField] protected Item _nowItem;
    
    public void UpdateInfo(Item item)
    {
        _nowItem = item;

        if (_nowItem.ItemObject == null)
        {
            ClearSlot();
            return;
        }

        ChangeStatus(true);

        _itemIcon.sprite = _nowItem.ItemObject.Icon;

        _itemAmountText.text = _nowItem.Amount.ToString();
        _itemWeightText.text = string.Format("{0} kg", _nowItem.Amount * _nowItem.ItemObject.Weight);
    }

    public void ClearSlot()
    {
        ChangeStatus(false);

        if(_nowItem.ItemObject == null)
        {
            _nowItem.Clear();
        }

        _itemAmountText.text = "0";
    }

    public SlotType GetSlotType()
    {
        return _slotType;
    }

    public Item GetItem()
    {
        return _nowItem;
    }

    protected void ChangeStatus(bool status)
    {
        _itemAmountText.enabled = status;
        _itemIcon.enabled = status;
        _itemWeightText.enabled = status;

        if (_nowItem.ItemObject != null && _nowItem.ItemObject.Type == ItemType.Tools)
            _itemAmountText.enabled = false;
    }
}
