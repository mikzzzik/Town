using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [SerializeField] ContextAction _actionType;

    public ContextAction GetAction()
    {
        return _actionType;
    }
}
