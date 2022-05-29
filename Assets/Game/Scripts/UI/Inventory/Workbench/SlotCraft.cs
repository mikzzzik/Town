using UnityEngine;

public class SlotCraft : Slot
{
    private int _typeIndex;
    private int _slotIndex;

    private bool _status; 
    
    public void Click()
    {
        WorkbenchUI.OnClickSlotCraft(GetItem(),_status,_typeIndex,_slotIndex);
    }

    public void Init(Item item, bool status, int typeIndex, int slotIndex)
    {
        UpdateInfo(item);

        _status = status;

        if (status) ChangeColor(Color.white);
            else ChangeColor(Color.red);

        _typeIndex = typeIndex;
        _slotIndex = slotIndex;
    }

    private void ChangeColor(Color color)
    {
        _itemAmountText.color = color;
    }
}
