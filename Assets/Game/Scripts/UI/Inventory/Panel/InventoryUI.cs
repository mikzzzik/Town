using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : InventoryPanelHolderUI
{
    [SerializeField] private CharacterInventory _characterInventory;

    public void InitSlots(List<Item> itemList,  float maxWeight)
    {
        _itemList = itemList;

        SetMaxWeight(maxWeight);

        ShowPanel();
    }
}
