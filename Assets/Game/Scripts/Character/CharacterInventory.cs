using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private float _maxWeight;

    [SerializeField] private List<BoxCarrying> _boxCarryingList;
    [SerializeField] private List<Item> _itemList;

    [SerializeField] private InventoryUI _inventoryUI;

    private float _itemWeight;

    private BoxCarrying _nowBox;

    public static Action<BoxType> OnChooseBox;
    public static Action<Container> OnOpenContainer;
    public static Action OnShow;
    public static Action<PickUpItem> OnPickUpItem;

    private void PickUpItem(PickUpItem pickUpItem)
    {
        Item nowItem = pickUpItem.GetItem();
        
        if (nowItem.item.Weight * nowItem.amount+ _itemWeight < _maxWeight)
        {
            for (int i = 0; i < _itemList.Count;i++)
            {
                if (nowItem.item == _itemList[i].item)
                {
                    _itemList[i].amount += nowItem.amount;

                    pickUpItem.PickUp();

                    break;

                }
                else if (_itemList[i].item == null)
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
            if(_itemList[i].item != null)
                _itemWeight += _itemList[i].item.Weight * _itemList[i].amount;
        }
    }



    private void Show()
    {
        UpdateItemList();

        _inventoryUI.InitSlots(_itemList, _maxWeight);
    }

    private void OnEnable()
    {
        OnOpenContainer += OpenContainer;
        OnPickUpItem += PickUpItem;
        OnChooseBox += ChooseBox;
        OnShow += Show;
    }

    private void OnDisable()
    {
        OnOpenContainer -= OpenContainer;
        OnPickUpItem -= PickUpItem;
        OnChooseBox -= ChooseBox;
        OnShow -= Show;
    }

    public BoxCarrying GetBox()
    {
        return _nowBox;
    }

    private void ChooseBox(BoxType boxType)
    {
        if (boxType == BoxType.none)
        {
            _nowBox = null;
            return;
        }

        for (int i = 0; i < _boxCarryingList.Count; i++)
        {
            if (boxType == _boxCarryingList[i].GetBoxType())
            {
                _nowBox = _boxCarryingList[i];

                _nowBox.Show();
            }
        }
    }

    private void OpenContainer(Container container)
    {

    }
}
