using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCarrying : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private BoxType _boxType;

    [SerializeField] private BoxObject _boxObject;

    public BoxType GetBoxType()
    {
        return _boxType;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}

