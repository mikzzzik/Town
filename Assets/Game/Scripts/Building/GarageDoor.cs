using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarageDoor : InteractiveObject
{
    [SerializeField] private Animator _animator;
 


    protected override void Active()
    {
        _actionStatus = true;
        _animator.SetBool("Status", true);
 
        _status = true;
      
    }

    protected override void Disable()
    {
        _actionStatus = true;
        _animator.SetBool("Status", false) ;

        _status = false; 

    }

    public void CanAction()
    {
        _actionStatus = false;
    }
}
