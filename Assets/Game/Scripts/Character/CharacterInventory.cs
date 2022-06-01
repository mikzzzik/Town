using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private float _maxWeight;


    [SerializeField] private List<Item> _inventoryItemList;
    [SerializeField] private List<Item> _hotBarItemList;

    [SerializeField] private InventoryPanelUI _inventoryPanelUI;

    private float _itemWeight;

    public static Action<Container> OnOpenContainer;
    public static Action OnShow;
    public static Action<PickUpItem> OnPickUpItem;
    public static Action<Item> OnDropItem;
    public static Action<Item> OnEquipItem;
    public static Action<int> OnHotBar;

    private void HotBar(int index)
    {
        if(_hotBarItemList[index].ItemObject != null && _hotBarItemList[index].ItemObject.Type == ItemType.Tools)
        {

        }
    }

    private void PickUpItem(PickUpItem pickUpItem)
    {
        Item nowItem = pickUpItem.GetItem();
        
        if (nowItem.ItemObject.Weight * nowItem.Amount+ _itemWeight < _maxWeight)
        {
            for (int i = 0; i < _inventoryItemList.Count;i++)
            {
                if (nowItem.ItemObject == _inventoryItemList[i].ItemObject)
                {
                    _inventoryItemList[i].Amount += nowItem.Amount;

                    pickUpItem.PickUp();

                    break;

                }
                else if (_inventoryItemList[i].ItemObject == null)
                {
                    _inventoryItemList[i] = nowItem;

                    pickUpItem.PickUp();

                    break;
                }
            }
        }

        UpdateItemList();
    }
    
    public void Init(List<Item> itemList)
    {
        _inventoryItemList = itemList;

        UpdateItemList();
    }

    private void UpdateItemList()
    { 
        _itemWeight = 0f;

        for (int i = 0; i < _inventoryItemList.Count; i++)
        {
            if(_inventoryItemList[i].ItemObject != null)
                _itemWeight += _inventoryItemList[i].ItemObject.Weight * _inventoryItemList[i].Amount;
        }
    }

    private void Show()
    {
        UpdateItemList();

        _inventoryPanelUI.SetItems(_inventoryItemList, _hotBarItemList, _maxWeight);
    }

    private void Drop(Item dropItem)
    {
        PickUpItem pickUpItem = Instantiate(dropItem.ItemObject.PickUpObject) as PickUpItem;

        pickUpItem.Drop(dropItem.Amount);
            
        pickUpItem.transform.position = transform.position;

    }

    private void Equip(Item item)
    {
        for(int i = 0; i < _hotBarItemList.Count;i++)
        {
            if(_hotBarItemList[i].ItemObject == null)
                _hotBarItemList[i].SetItem(item);
        }
    }

    private void OnEnable()
    {

        OnPickUpItem += PickUpItem;
        OnDropItem += Drop;
        OnShow += Show;
        OnEquipItem += Equip;
    }

    private void OnDisable()
    {

        OnPickUpItem -= PickUpItem;
        OnDropItem -= Drop;
        OnShow -= Show;
        OnEquipItem -= Equip;
    }

    public List<Item> GetItemList()
    {
        return _inventoryItemList;
    }

}
