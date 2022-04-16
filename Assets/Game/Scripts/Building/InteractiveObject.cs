using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    protected bool _status = false;

    public void Interactive()
    {
        if(_status)
        {
            Active();
        }
        else
        {
            Disable();
        }
    }

     public virtual void Active()
     {

     }
    public virtual void Disable()
    {

    }

}
