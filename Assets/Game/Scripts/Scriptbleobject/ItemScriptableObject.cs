using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemObject", order = 1)]

public class ItemScriptableObject : ScriptableObject
{
    public enum ItemType {Common, Resource, Tools}
    public ItemType Type;

    public string Name;
    public string Description;

    public float Weight;
    public float LowerPrice;
    public float MaxPrice;

    public Sprite Icon;

    public GameObject ObjectPrefab;

    public List<Item> ItemToCraft;

    public int craftAmount;
}
[System.Serializable]
public class Item
{
    public ItemScriptableObject item;
    public int amount;

    public void SetItem(ItemScriptableObject itemObject, int newAmount)
    {
        item = itemObject;
        amount = newAmount;
    }
    public void SetItem(Item newItem)
    {
        item = newItem.item;
        amount = newItem.amount;
    }

    public void Clear()
    {
        item = null;
        amount = 0;
    }
}
