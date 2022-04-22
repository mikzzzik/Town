using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] protected bool _status = false;

    protected bool _actionStatus;

    public   void Interactive()
    {
        if (_actionStatus) return;

        Debug.Log(_status);

        if(!_status)
        {
            Active();
        }
        else
        {
            Disable();
        }
    }

     protected virtual void Active()
     {

     }
    protected virtual void Disable()
    {
      
    }

}
