using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerInfo
{
    public Vector3 Position;
    public Quaternion Rotation;

    public List<Item> InventoryList;
    public List<Item> HotBarList;

    public PlayerInfo(Vector3 position, Quaternion rotation, List<Item> inventoryList, List<Item> hotBarList)
    {
        Position = position;
        Rotation = rotation;

        InventoryList = inventoryList; 
        HotBarList = hotBarList;
    }
}

[System.Serializable]
public class CameraInfo
{
    public Vector3 Rotation;

    public CameraInfo(Vector3 rotation)
    {

        Rotation = rotation;
    }
}
[System.Serializable]
public class PickUpItemInfoList
{
    public List<PickUpItemInfo> ResourceInfoList;

    public PickUpItemInfoList(List<PickUpItemInfo> resourceInfoList)
    {
           ResourceInfoList = resourceInfoList;
    }
}

[System.Serializable]
public class PickUpItemInfo
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Item Item;

    public PickUpItemInfo(Vector3 position, Quaternion rotation, Item item)
    {

        Position = position;
        Rotation = rotation;
        Item = item;
    }
}