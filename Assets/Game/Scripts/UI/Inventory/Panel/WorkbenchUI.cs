using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkbenchUI : MonoBehaviour
{
    [SerializeField] private Transform _contentHolder;

    [SerializeField] private GameObject _itemView;

    [SerializeField] private SlotCraft _slotPrefab;

    [SerializeField] private List<CraftebleType> _craftebleTypeList;

    [SerializeField] private ItemType _nowItemType;
    [SerializeField] private CraftItemInfoContainer _craftItemInfoContainer;

    [SerializeField] private CharacterInventory _characterInventory;

    [SerializeField] private List<Item> _inventoryItemList = new List<Item>();
    [SerializeField] private List<NeedItemHolder> _needItemHolder = new List<NeedItemHolder>();

    private CraftebleType _nowCraftebleTypeList;

    public static Action<Item,bool, int,int> OnClickSlotCraft;

    private Item _item;

    [SerializeField] private List<SlotCraft> _slotCraftList;

    [System.Serializable]
    public struct CraftebleType
    {
        public ItemType Type;
        public List<ItemScriptableObject> ItemObjectList;
        public List<bool> CanCraft;
    }

    private void OnEnable()
    {
        OnClickSlotCraft += ClickSlot;
    }

    private void OnDisable()
    {
        OnClickSlotCraft -= ClickSlot;
    }

    private void ClickSlot(Item item, bool status, int typeIndex, int slotIndex)
    {
        _craftItemInfoContainer.Init(item.ItemObject);

        for(int i = 0; i < _needItemHolder.Count; i++)
        {
            if(i < _craftebleTypeList[typeIndex].ItemObjectList[slotIndex].ItemToCraft.Count)
            {
                _needItemHolder[i].Show(_craftebleTypeList[typeIndex].ItemObjectList[slotIndex].ItemToCraft[i]);

                _craftItemInfoContainer.ChangeButtonStatus(status);
            }
            else
            {
                _needItemHolder[i].Hide();
            }
        }
    }

    public void Init(List<ItemScriptableObject> itemList)
    { 

        ClearList();
        CalcItem();

        List<ItemScriptableObject> tempItemList = new List<ItemScriptableObject>(itemList);


        for (int i = 0; i < tempItemList.Count; i++)
        {
            for(int j = 0; j < _craftebleTypeList.Count;j++)
            {
                if(tempItemList[i].Type == _craftebleTypeList[j].Type)
                {
                    ItemScriptableObject tempItem = tempItemList[i];

                    _craftebleTypeList[j].ItemObjectList.Add(tempItem);
                    _craftebleTypeList[j].CanCraft.Add(CanCraft(tempItemList[i]));

                    tempItemList.Remove(tempItemList[i]);

                    i--;
                    break;
                }
            }
        }
         
        if(_nowItemType != ItemType.None)
        {
            UpdateItemListView();
        }

        ShowPanel();
    }
    
    private void ClearList()
    {
        for (int i = 0; i < _craftebleTypeList.Count; i++)
        {
            _craftebleTypeList[i].ItemObjectList.Clear();
            _craftebleTypeList[i].CanCraft.Clear();
        }

        _inventoryItemList.Clear();
    }
    private bool CanCraft(ItemScriptableObject itemObject)
    {
        bool status = false;

        for (int i = 0; i < itemObject.ItemToCraft.Count; i++)
        {
            status = false;

            for (int j = 0; j < _inventoryItemList.Count; j++)
            {
                if (_inventoryItemList[j].ItemObject == itemObject.ItemToCraft[i].ItemObject)
                {
                    if (_inventoryItemList[j].Amount < itemObject.ItemToCraft[i].Amount)
                    {
                        return false;
                    }
                    else status = true;
                }
            }
        }

        return status;
    }

    public void ChooseType(ItemType itemType)
    {
        if (itemType == _nowItemType) return;

        _nowItemType = itemType;

        UpdateItemListView();
    }

    private void UpdateItemListView()
    {
        _craftItemInfoContainer.Hide();
        _slotCraftList.Clear();

        _itemView.SetActive(true);

        int count = _contentHolder.childCount;

        for (int i = 0; i < count; i++)
        {
            Destroy(_contentHolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < _craftebleTypeList.Count; i++)
        {
            if (_nowItemType == _craftebleTypeList[i].Type)
            {
                InstantiateSlots(i, _craftebleTypeList[i]);

                _nowCraftebleTypeList = _craftebleTypeList[i];
                
                break;
            }
        }
    }

    private void InstantiateSlots(int i, CraftebleType craftebleTypeList)
    {
        for (int j = 0; j < craftebleTypeList.ItemObjectList.Count; j++)
        {
            SlotCraft tempSlot = Instantiate(_slotPrefab) as SlotCraft;

            tempSlot.transform.SetParent(_contentHolder, false);

            _item = new Item();

            _item.SetItem(craftebleTypeList.ItemObjectList[j], craftebleTypeList.ItemObjectList[j].CraftAmount);

            _slotCraftList.Add(tempSlot);

            tempSlot.Init(_item, craftebleTypeList.CanCraft[j], i, j);
        }
    }

    public void UpdateViewAfterCraft()
    {

        CalcItem();
        for(int i = 0; i < _nowCraftebleTypeList.ItemObjectList.Count; i++)
        {
            _nowCraftebleTypeList.CanCraft[i] = CanCraft(_nowCraftebleTypeList.ItemObjectList[i]);
        }

        for (int i = 0; i < _craftebleTypeList.Count; i++)
        {
            if (_nowItemType == _craftebleTypeList[i].Type)
            {

                for (int j = 0; j < _craftebleTypeList[i].ItemObjectList.Count; j++)
                {
                    _item = new Item();
                    _item.SetItem(_craftebleTypeList[i].ItemObjectList[j], _craftebleTypeList[i].ItemObjectList[j].CraftAmount);

                    _slotCraftList[j].Init(_item, _craftebleTypeList[i].CanCraft[j], i, j);
                }
                break;
            }
        }
    }

    private void CalcItem()
    {
        List<Item> charactreInventoryList = _characterInventory.GetInventoryItemList();
        _inventoryItemList.Clear();

        for (int i = 0; i < charactreInventoryList.Count;i++)
        {
            bool status = true;
            for(int j = 0; j < _inventoryItemList.Count;j++)
            {
                if(charactreInventoryList[i].ItemObject == _inventoryItemList[j].ItemObject)
                {
                    _inventoryItemList[j].Amount += charactreInventoryList[i].Amount;

                    status = false;

                    break;
                }
            }
            if(status)
            {
                Item tempItem = new Item();

                tempItem.SetItem(charactreInventoryList[i]);

                _inventoryItemList.Add(tempItem);
            }
        }
    }

    private void ShowPanel()
    {
        UIController.OnChangeStatusPanel(gameObject);

        UIController.OnChangeStatusInteractiveText(false, null);

        CharacterInventory.OnShow();

    }
}
