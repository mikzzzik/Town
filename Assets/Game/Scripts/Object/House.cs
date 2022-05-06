using System.Collections.Generic;
using UnityEngine;

public class House : ActionObject
{
    [SerializeField] private List<Renderer> _rendererList;

    [SerializeField] private Transform _enterPosition;

    [SerializeField] private BoxCollider _collider;
    
    protected override void Action()
    {
        base.Action();

        ContextMenu.OnSetAction(Enter);
    }

    private void Enter()
    {

        _collider.enabled = false;




      //  Character.OnSetHouse(this);
      //  Character.OnSetPlace(Place.InHouse);

        for (int i = 0; i < _rendererList.Count; i++)
        {
            _rendererList[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

    }

    public void Leave()
    {
        _collider.enabled = true;

  //      Character.OnSetHouse(this);
  //      Character.OnSetPlace(Place.InOutside);

        for (int i = 0; i < _rendererList.Count; i++)
        {
            _rendererList[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
