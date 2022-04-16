using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarageDoor : InteractiveObject
{
    [SerializeField] private Transform _garageDoor;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Transform _beginPosition;

    public override void Active()
    {
        _garageDoor.DOMove(_endPosition.position, 2f).SetEase(Ease.Linear).OnComplete(() => 
        {
            _status = true; 
        });        
    }

    public override void Disable()
    {
        _garageDoor.DOMove(_beginPosition.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _status = true;
        });
    }
}
