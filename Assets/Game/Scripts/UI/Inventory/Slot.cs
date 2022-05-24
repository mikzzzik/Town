using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] protected TextMeshProUGUI _itemAmountText;
    [SerializeField] private TextMeshProUGUI _itemWeightText;

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

        _itemAmountText.text = "0";
    }

    public Item GetItem()
    {
        return _nowItem;
    }

    protected void ChangeStatus(bool status)
    {
        _itemIcon.enabled = status;
        _itemWeightText.enabled = status;
        _itemAmountText.enabled = status;
    }
}
