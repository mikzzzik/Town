using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class SavePanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _searchSaveInputField;
    [SerializeField] private TMP_InputField _nameSaveInputField;

    

    [SerializeField] private SaveHolder _saveHolderPrefab;
    [SerializeField] private Transform _saveContainer;

    private List<SaveHolder> _saveHolderList = new List<SaveHolder>();

    public static Action<string> OnRemoveSave;

    private void OnEnable()
    {
        ClearSaveList();
        LoadSaveList();
    }

    public void ClickSave()
    {
        if (_nameSaveInputField.text != null)
        {
            
            PlayerPrefs.SetString("SaveName", _nameSaveInputField.text);

            SaveManager.OnSaveData();

            _nameSaveInputField.text = string.Empty; 

            ClearSaveList();
            LoadSaveList();
        }
    }

    private void LoadSaveList()
    {
        string path = Application.persistentDataPath + "/Save/";

        var folderArray = new DirectoryInfo(path).GetDirectories();

        for (int i = 0; i < folderArray.Length; i++)
        {
            var saveHolder = Instantiate(_saveHolderPrefab) as SaveHolder;

            saveHolder.transform.SetParent(_saveContainer, false);
            saveHolder.gameObject.name = "Name_" + i;
            saveHolder.Init(folderArray[i].Name);

            _saveHolderList.Add(saveHolder);

        }
    }

    private void ClearSaveList()
    {
        while (_saveHolderList.Count > 0)
        {
            if (_saveHolderList[0] != null)     
            Destroy(_saveHolderList[0].gameObject);

            _saveHolderList.RemoveAt(0);

        }
    }
}
