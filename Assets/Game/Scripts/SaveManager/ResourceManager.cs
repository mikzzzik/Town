using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]  List<PickUpItemInfo> _pickUpInfoList;

    public static Action OnSaveResourceData;
    public static Action<PickUpItemInfo> OnAddToList;

    private void Awake()
    {
        SaveManager.OnSaveData += SaveData;
        SaveManager.OnLoadData += LoadData;
        OnAddToList += AddResourceToList;
    }


    private void OnDisable()
    {
        SaveManager.OnSaveData -= SaveData;
        SaveManager.OnLoadData -= LoadData;
        OnAddToList -= AddResourceToList;
    }
    private void AddResourceToList(PickUpItemInfo pickUpInfo)
    {
        _pickUpInfoList.Add(pickUpInfo);
    }

    private void SaveData()
    {
        _pickUpInfoList = new List<PickUpItemInfo>();
       
        OnSaveResourceData?.Invoke();

        if (_pickUpInfoList.Count <= 0) return;

        PickUpItemInfoList resourceInfoListHolder = new PickUpItemInfoList(_pickUpInfoList);

        SaveManager.SaveToJson(SaveManager.ResouceDataName, resourceInfoListHolder);
    }

    private void LoadData()
    {
        PickUpItemInfoList resourceInfoListHolder = SaveManager.LoadFromJson<PickUpItemInfoList>(SaveManager.ResouceDataName);
      
        if (resourceInfoListHolder == null) return;

        _pickUpInfoList = resourceInfoListHolder.ResourceInfoList;
        
        for(int i =0; i < _pickUpInfoList.Count; i++)
        {
            var pickUpItem = Instantiate(_pickUpInfoList[i].Item.ItemObject.PickUpObject) as PickUpItem;
            pickUpItem.Drop(_pickUpInfoList[i].Item.Amount);

            pickUpItem.transform.position = _pickUpInfoList[i].Position;
            pickUpItem.transform.rotation = _pickUpInfoList[i].Rotation;
        }
    }
}
