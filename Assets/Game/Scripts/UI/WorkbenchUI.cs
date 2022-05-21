using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorkbenchUI : MonoBehaviour
{
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private GameObject _itemView;
    [SerializeField] private Slot _slotPrefab;

    [SerializeField] private List<ItemScriptableObject> _itemList;


    void Start()
    {
        
    }

    public void Init(List<ItemScriptableObject> itemList)
    {
        _itemList = itemList;

        ShowPanel();
    }

    private void ShowPanel()
    {
        UIController.OnChangeStatusPanel(gameObject);

        UIController.OnChangeStatusInteractiveText(false, null);

    }
}
