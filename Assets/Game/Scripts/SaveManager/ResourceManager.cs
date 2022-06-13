using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    private List<PickUpItemInfo> _pickUpInfoList;
   [SerializeField] private List<ResourceObjectInfo> _resourceObjectList;

    public static Action OnSavePickUpItem;
    public static Action OnSaveResourceObject;
    public static Action<PickUpItemInfo> OnAddPickUpItemToList;
    public static Action<ResourceObjectInfo> OnAddResourceObjectToList;

    private void Awake()
    {
        SaveManager.OnSaveData += SaveData;
        SaveManager.OnLoadData += LoadData;
        OnAddPickUpItemToList += AddPickUpItemToList;
        OnAddResourceObjectToList += AddResourceObjectToList;
    }


    private void OnDisable()
    {
        SaveManager.OnSaveData -= SaveData;
        SaveManager.OnLoadData -= LoadData;

        OnAddPickUpItemToList -= AddPickUpItemToList;
        OnAddResourceObjectToList -= AddResourceObjectToList;
    }
    private void AddPickUpItemToList(PickUpItemInfo pickUpInfo)
    {
        _pickUpInfoList.Add(pickUpInfo);
    }

    private void AddResourceObjectToList(ResourceObjectInfo resourceObject)
    {
        _resourceObjectList.Add(resourceObject);
    }

    private void SaveData()
    {
        SavePickUpInfoData();
        SaveResourceObjectInfoData();


    }

    private void SavePickUpInfoData()
    {
        _pickUpInfoList = new List<PickUpItemInfo>();

        OnSavePickUpItem?.Invoke();

        PickUpItemInfoList pickUpItemInfoList = new PickUpItemInfoList(_pickUpInfoList);

        SaveManager.SaveToJson(SaveManager.PickUpItemDataName, pickUpItemInfoList);
    }

    private void SaveResourceObjectInfoData()
    {
        _resourceObjectList = new List<ResourceObjectInfo>();

        OnSaveResourceObject?.Invoke();

        ResourceObjectInfoList resourceObjectInfoList = new ResourceObjectInfoList(_resourceObjectList);

        SaveManager.SaveToJson(SaveManager.ResourceObjectDataName, resourceObjectInfoList);
    }

    private void LoadData()
    {
        LoadPickUpItem();
        LoadResourceObject();
    }

    private void LoadPickUpItem()
    {
        PickUpItemInfoList resourceInfoListHolder = SaveManager.LoadFromJson<PickUpItemInfoList>(SaveManager.PickUpItemDataName);

        if (resourceInfoListHolder == null) return;

        _pickUpInfoList = resourceInfoListHolder.PickUpItemList;

        for (int i = 0; i < _pickUpInfoList.Count; i++)
        {
            var pickUpItem = Instantiate(_pickUpInfoList[i].Item.ItemObject.PickUpObject) as PickUpItem;
            pickUpItem.Drop(_pickUpInfoList[i].Item.Amount);

            pickUpItem.transform.position = _pickUpInfoList[i].Position;
            pickUpItem.transform.rotation = _pickUpInfoList[i].Rotation;
        }
    }
    
    private void LoadResourceObject()
    {
        ResourceObjectInfoList resourceObjectListHolder = SaveManager.LoadFromJson<ResourceObjectInfoList>(SaveManager.ResourceObjectDataName);

        if (resourceObjectListHolder == null) return;

        _resourceObjectList = resourceObjectListHolder.ResourceObjectList;

        for (int i = 0; i < _resourceObjectList.Count; i++)
        {
            var resourceObject = Instantiate(_resourceObjectList[i].ResourceObject.Prefab);

            resourceObject.transform.position = _resourceObjectList[i].Position;
            resourceObject.transform.rotation = _resourceObjectList[i].Rotation;
        }
    }
}
