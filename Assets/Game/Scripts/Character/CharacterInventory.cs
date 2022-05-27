using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private float _maxWeight;


    [SerializeField] private List<Item> _itemList;

    [SerializeField] private InventoryUI _inventoryUI;

    private float _itemWeight;

    public static Action<Container> OnOpenContainer;
    public static Action OnShow;
    public static Action<PickUpItem> OnPickUpItem;

    private void PickUpItem(PickUpItem pickUpItem)
    {
        Item nowItem = pickUpItem.GetItem();
        
        if (nowItem.ItemObject.Weight * nowItem.Amount+ _itemWeight < _maxWeight)
        {
            for (int i = 0; i < _itemList.Count;i++)
            {
                if (nowItem.ItemObject == _itemList[i].ItemObject)
                {
                    _itemList[i].Amount += nowItem.Amount;

                    pickUpItem.PickUp();

                    break;

                }
                else if (_itemList[i].ItemObject == null)
                {
                    _itemList[i] = nowItem;

                    pickUpItem.PickUp();

                    break;
                }
            }
        }

        UpdateItemList();
    }
    
    public void Init(List<Item> itemList)
    {
        _itemList = itemList;

        UpdateItemList();
    }

    private void UpdateItemList()
    { 
        _itemWeight = 0f;

        for (int i = 0; i < _itemList.Count; i++)
        {
            if(_itemList[i].ItemObject != null)
                _itemWeight += _itemList[i].ItemObject.Weight * _itemList[i].Amount;
        }
    }



    private void Show()
    {
        UpdateItemList();

        _inventoryUI.InitSlots(_itemList, _maxWeight);
    }

    private void OnEnable()
    {

        OnPickUpItem += PickUpItem;

        OnShow += Show;
    }

    private void OnDisable()
    {

        OnPickUpItem -= PickUpItem;

        OnShow -= Show;
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }

}
