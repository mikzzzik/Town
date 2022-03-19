using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _ignoreLayer;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitPoint;

            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            // Debug.Log(Physics.Raycast(ray, out hitPoint,15f, layerMask));
            if(Physics.Raycast(ray, out hitPoint, 15f, layerMask))
            {
    
                if (hitPoint.collider.gameObject.layer == 6) return;

                CharacterMoving.OnSetNewPosition(hitPoint.point);
                CharacterMoving.OnSetHit(hitPoint);

                ContextMenu.OnShowContextMenu(new List<ContextAction>() { ContextAction.Move, ContextAction.Cancel });
            }
        }
    }
}
