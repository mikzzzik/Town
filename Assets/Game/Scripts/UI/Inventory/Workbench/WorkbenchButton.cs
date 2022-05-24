using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchButton : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    
    [SerializeField] private WorkbenchUI _workbenchUI;

    public void Click()
    {
        _workbenchUI.ChooseType(_itemType);
    }
}
