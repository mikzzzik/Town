using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionObject : MonoBehaviour
{
   // [SerializeField] protected List<ContextAction> _contextActionList;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Action();
    }

    protected virtual void Action()
    {
   //     ContextMenu.OnShowContextMenu(_contextActionList);
    }
}
