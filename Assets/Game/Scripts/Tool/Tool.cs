using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tool : MonoBehaviour
{
    [SerializeField] private string _animatorStateName;

    [SerializeField] private BoxCollider _boxCollider;

    [SerializeField] private ToolScriptableObject _toolScriptableObject;

    public static Action OnActiveCollision;
    public static Action OnDisableCollision;

        
    public void Equip()
    {
        gameObject.SetActive(true);
        
        DisableCollision();

        OnActiveCollision += ActiveCollision;
        OnDisableCollision += DisableCollision;
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);

        OnActiveCollision -= ActiveCollision;
        OnDisableCollision -= DisableCollision;
    }

    public string GetStateName()
    {
        return _animatorStateName;
    }

    private void ActiveCollision()
    {
        _boxCollider.enabled = true;
    }

    private void DisableCollision()
    {
        _boxCollider.enabled = false;
    }

    public ToolScriptableObject GetToolScriptableObject()
    {
        return _toolScriptableObject;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
