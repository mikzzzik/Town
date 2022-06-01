using System.Collections.Generic;
using UnityEngine;

public class Container : InteractiveObject
{
    [SerializeField] private List<Item> _inventoryItemList;



    [SerializeField] private ContainerType _containerType;
    [SerializeField] private ContainerPanelUI _containerUI;

    private void OnEnable()
    {
        int count = _containerType.RowAmount * _containerType.ColumnAmount;

        _inventoryItemList = new List<Item>();

        for (int i = 0; i < count; i++)
        {
            _inventoryItemList.Add(new Item());
        }

    }

    public ContainerType GetContaineType()
    {
        return _containerType;
    }

    public List<Item> GetItemList()
    {
        return _inventoryItemList;
    }

  

    public void AddItem(Item item, int amount)
    {

    }

   
    protected override void Active()
    {
        base.Active();

        _containerUI.InitSlots(this);
    }
}
