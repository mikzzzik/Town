using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemObject", order = 1)]

public class ItemScriptableObject : ScriptableObject
{

    public ItemType Type;

    public string Name;
    public string Description;

    public float Weight;
    public float LowerPrice;
    public float MaxPrice;

    public Sprite Icon;

    public GameObject ObjectPrefab;

    public List<Item> ItemToCraft;
    public float TimeToCraft;
    public int CraftAmount;
}
[System.Serializable]
public class Item
{
    public ItemScriptableObject ItemObject;
    public int Amount;

    public void SetItem(ItemScriptableObject itemObject, int newAmount)
    {
        ItemObject = itemObject;
        Amount = newAmount;
    }
    public void SetItem(Item newItem)
    {
        ItemObject = newItem.ItemObject;
        Amount = newItem.Amount;
    }

    public void Clear()
    {
        ItemObject = null;
        Amount = 0;
    }
}
public enum ItemType {None, Common, Resource, Tools }