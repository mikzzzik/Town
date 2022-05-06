using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private List<Transform> _itemTransformList;

    [SerializeField] private List<Item> _itemList;

    [SerializeField] private List<int> _itemAmountList;

    [SerializeField] private ContainerType _containerType;

    private List<Transform> _availableItemTransform;

    private void OnEnable()
    {
        int count = _containerType.RowAmount * _containerType.ColumnAmount;

        _itemList = new List<Item>(count);
        _itemAmountList = new List<int>(count);
    }

    public ContainerType GetContaineType()
    {
        return _containerType;
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }

    public List<int> GetAmountItemList()
    {
        return _itemAmountList;
    }

    public void AddItem(Item item, int amount)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
          //  if(other.TryGetComponent(out CharacterInventory characterInventory))
          //  {
                ButtonController.OnContainerTriggerEnter(this);

              /*  if(characterInventory.GetBox() != null)
                {

                }*/
          //  }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
         
            ButtonController.OnContainerTriggerEnter(null);
    }
}
