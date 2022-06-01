using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : InteractiveObject
{
    [SerializeField] private WorkbenchUI _workbenchUI;
    [SerializeField] private List<ItemScriptableObject> _inventoryItemList;
    protected override void Active() 
    {
        base.Active();

        _workbenchUI.Init(_inventoryItemList);
     
    }
}
