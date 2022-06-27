using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelUI : PanelHolderUI
{
    [SerializeField] private List<Slot> _inventorylotSlotList = new List<Slot>();
    [SerializeField] private List<Slot> _hotBarSlotList = new List<Slot>();


    private List<Item> _inventoryItemList;
    private List<Item> _hotBarItemList;

    public void SetItems(List<Item> inventoryItemList, List<Item> hotBarItemList, float maxWeight, bool status = true)
    {
        _maxWeight = maxWeight;

        _inventoryItemList = inventoryItemList;
        _hotBarItemList = hotBarItemList;

        if (status)
            ShowPanel();
        else UpdateUI();
    }

    public override void UpdateUI()
    {
        UpdateInventoryUI();
        UpdateHotBarUI();

        CalcWeight();
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < _inventorylotSlotList.Count; i++)
        {
            _inventorylotSlotList[i].UpdateInfo(_inventoryItemList[i]);
        }
    }

    private void UpdateHotBarUI()
    {
        for (int i = 0; i < _hotBarSlotList.Count; i++)
        {
            _hotBarSlotList[i].UpdateInfo(_hotBarItemList[i]);
        }

        CharacterToolController.OnCheckEquipTool();
    }

    private void CalcWeight()
    {
        _nowWeight = 0;

        List<Item> itemList = new List<Item>(_inventoryItemList);

        itemList.AddRange(_hotBarItemList);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ItemObject != null)
            {
                _nowWeight += itemList[i].ItemObject.Weight * itemList[i].Amount;
            }
        }

        SetWeight(_maxWeight, _nowWeight);
    }
}
