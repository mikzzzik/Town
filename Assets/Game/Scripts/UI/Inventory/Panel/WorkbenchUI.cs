using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkbenchUI : MonoBehaviour
{
    [SerializeField] private Transform _contentHolder;

    [SerializeField] private GameObject _itemView;

    [SerializeField] private SlotCraft _slotPrefab;

    [SerializeField] private List<CraftebleType> _typeList;

    [SerializeField] private ItemType _nowItemType;
    [SerializeField] private CraftItemInfoContainer _craftItemInfoContainer;

    [SerializeField] private CharacterInventory _characterInventory;

    [SerializeField] private List<Item> _itemList = new List<Item>();
    [SerializeField] private List<NeedItemHolder> _needItemHolder = new List<NeedItemHolder>();
    public static Action<Item, int,int> OnClickSlotCraft;

    private Item _item;

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

    private void ClickSlot(Item item, int typeIndex, int slotIndex)
    {
        _craftItemInfoContainer.Init(item.ItemObject);

        for(int i = 0; i < _needItemHolder.Count; i++)
        {
            if(i < _typeList[typeIndex].ItemObjectList[slotIndex].ItemToCraft.Count)
            {
                _needItemHolder[i].Show(_typeList[typeIndex].ItemObjectList[slotIndex].ItemToCraft[i]);
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
            for(int j = 0; j < _typeList.Count;j++)
            {
                if(tempItemList[i].Type == _typeList[j].Type)
                {
                    ItemScriptableObject tempItem = tempItemList[i];

                    _typeList[j].ItemObjectList.Add(tempItem);
                    _typeList[j].CanCraft.Add(CanCraft(tempItemList[i]));

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
        for (int i = 0; i < _typeList.Count; i++)
        {
            _typeList[i].ItemObjectList.Clear();
            _typeList[i].CanCraft.Clear();
        }

        _itemList.Clear();
    }

    private void CalcItem()
    {
        List<Item> charactreInventoryList = _characterInventory.GetItemList();
        
        for(int i = 0; i < charactreInventoryList.Count;i++)
        {
            bool status = true;
            for(int j = 0; j < _itemList.Count;j++)
            {
                if(charactreInventoryList[i].ItemObject == _itemList[j].ItemObject)
                {
                    _itemList[j].Amount += charactreInventoryList[i].Amount;

                    status = false;

                    break;
                }
            }
            if(status)
            {
                Item tempItem = new Item();

                tempItem.SetItem(charactreInventoryList[i]);

                _itemList.Add(tempItem);
            }
        }
    }

    private bool CanCraft(ItemScriptableObject itemObject)
    {
        bool status = false;

        for(int i = 0; i < itemObject.ItemToCraft.Count;i++)
        {
            status = false;

            for (int j = 0; j < _itemList.Count;j++)
            {
                if(_itemList[j].ItemObject == itemObject.ItemToCraft[i].ItemObject)
                {
                    if (_itemList[j].Amount < itemObject.ItemToCraft[i].Amount)
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

        _itemView.SetActive(true);

        int count = _contentHolder.childCount;

        for (int i = 0; i < count; i++)
        {

            Destroy(_contentHolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < _typeList.Count; i++)
        {
            if (_nowItemType == _typeList[i].Type)
            {
                for (int j = 0; j < _typeList[i].ItemObjectList.Count; j++)
                {
                    SlotCraft tempSlot = Instantiate(_slotPrefab) as SlotCraft;

                    tempSlot.transform.SetParent(_contentHolder);

                    _item = new Item();

                    _item.SetItem(_typeList[i].ItemObjectList[j], _typeList[i].ItemObjectList[j].CraftAmount);

                    tempSlot.Init(_item, _typeList[i].CanCraft[j], i, j);
                }
                break;
            }
        }
    }
    public Item GetItem()
    {
        return _item;
    }

    private void ShowPanel()
    {
        UIController.OnChangeStatusPanel(gameObject);

        UIController.OnChangeStatusInteractiveText(false, null);

    }

    public void  CraftItem()
    {

    }
}
