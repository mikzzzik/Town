using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Character _character;

    private CharacterInventory _characterInventory;
    private void Awake()
    {
        _characterInventory = _character.GetCharacterInventory();

        SaveManager.OnSaveData += SaveData;
        SaveManager.OnLoadData += LoadData;

        Debug.Log(_character.transform.position);
    }

    private void OnDisable()
    {
        SaveManager.OnSaveData -= SaveData;
        SaveManager.OnLoadData -= LoadData;
    }

    private void OnEnable()
    {
        Debug.Log(_character.transform.position);
    }
    private void Start()
    {
        Debug.Log(_character.transform.position);
    }

    private void SaveData()
    {        
        PlayerInfo playerInfo = new PlayerInfo(_character.transform.localPosition, _character.transform.localRotation, _characterInventory.GetInventoryItemList(), _characterInventory.GetHotBarItemList());

        SaveManager.SaveToJson(SaveManager.PlayerDataName, playerInfo);
    }

    private void LoadData()
    {
        PlayerInfo playerInfo = SaveManager.LoadFromJson<PlayerInfo>(SaveManager.PlayerDataName);

        if (playerInfo == null) return;

        _character.BeginLoad();

        _character.transform.rotation = playerInfo.Rotation;
        _character.transform.position = playerInfo.Position;

        _character.Loaded();

        _characterInventory.Init(playerInfo.InventoryList, playerInfo.HotBarList);

    }

}
