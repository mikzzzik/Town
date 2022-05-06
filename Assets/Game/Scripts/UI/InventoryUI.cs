using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : InventoryPanelHolderUI
{
    [SerializeField] private TextMeshProUGUI _weightText;

    public void InitSlots(List<Item> itemList, List<int> itemAmountList)
    {
        _itemList = itemList;
        _itemAmountList = itemAmountList;

        ShowPanel();
    }


}
