using UnityEngine;

public class SlotCraft : Slot
{
    private int _typeIndex;
    private int _slotIndex;

    public void Click()
    {
        WorkbenchUI.OnClickSlotCraft(GetItem(),_typeIndex,_slotIndex);
    }

    public void Init(Item item, bool status, int typeIndex, int slotIndex)
    {
        UpdateInfo(item);

        if (status) ChangeColor(Color.white);
            else ChangeColor(Color.red);

        _typeIndex = typeIndex;
        _slotIndex = slotIndex;
    }

    private void ChangeColor(Color color)
    {
        Debug.Log(color);

        _itemAmountText.color = color;
    }
}
