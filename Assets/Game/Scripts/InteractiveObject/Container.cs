using System.Collections.Generic;
using UnityEngine;

public class Container : InteractiveObject
{
    [SerializeField] private List<Item> _itemList;



    [SerializeField] private ContainerType _containerType;
    [SerializeField] private ContainerUI _containerUI;

  // private List<Transform> _availableItemTransform;

    private void OnEnable()
    {
        int count = _containerType.RowAmount * _containerType.ColumnAmount;

        _itemList = new List<Item>();

        for (int i = 0; i < count; i++)
        {
            _itemList.Add(new Item());
        }

    }

    public ContainerType GetContaineType()
    {
        return _containerType;
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }

  

    public void AddItem(Item item, int amount)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           ButtonController.OnContainerTriggerEnter(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
         
            ButtonController.OnContainerTriggerEnter(null);
    }

    protected override void Active()
    {
        base.Active();
        _containerUI.InitSlots(this);
    }
}
