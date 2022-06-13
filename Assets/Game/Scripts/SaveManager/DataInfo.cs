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
    public List<PickUpItemInfo> PickUpItemList;

    public PickUpItemInfoList(List<PickUpItemInfo> resourceInfoList)
    {
        PickUpItemList = resourceInfoList;
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

[System.Serializable]
public class ResourceObjectInfoList
{
    public List<ResourceObjectInfo> ResourceObjectList;

    public ResourceObjectInfoList(List<ResourceObjectInfo> resourceObjectInfoList)
    {
        ResourceObjectList = resourceObjectInfoList;
    }
}

[System.Serializable]
public class ResourceObjectInfo
{
    public Vector3 Position;
    public Quaternion Rotation;
    public ResourceObject ResourceObject;

    public ResourceObjectInfo(Vector3 position, Quaternion rotation, ResourceObject resourceObject)
    {

        Position = position;
        Rotation = rotation;
        ResourceObject = resourceObject;
    }
}