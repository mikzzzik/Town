using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;
    void Start()
    {
        
    }

    
    void Update()
    {
      

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitPoint;
            RaycastHit tempHitPoint;

            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            Physics.Raycast(ray, out tempHitPoint, 30f, _layerMask);
            Debug.Log("GG: " + tempHitPoint.collider);


            if (Physics.Raycast(ray, out hitPoint, 30f, _layerMask))
            {
                Debug.Log(hitPoint.collider );
                if (hitPoint.collider.gameObject.layer == 6) return;

                CharacterMoving.OnSetNewPosition(hitPoint.point);
                CharacterMoving.OnSetHit(hitPoint);

                ContextMenu.OnShowContextMenu(new List<ContextAction>() { ContextAction.Move, ContextAction.Cancel });
            }
        }
    }
}
