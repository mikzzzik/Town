using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterToolController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Tool> _toolsList;

    private Tool _eqiupTool;

    public static Action OnAttack;


    public void ActiveTool()
    {
        Tool.OnActiveCollision();
    }

    public void DisableTool()
    {
        Tool.OnDisableCollision();
    }

    private void OnEnable()
    {
        OnAttack += Attack;
    }

    private void OnDisable()
    {
        OnAttack -= Attack;
    }
    private void Attack()
    {
        if (_eqiupTool != null)
            _animator.SetBool("Attack", true);
    }

    public void EndAttack()
    {
        _animator.SetBool("Attack", false);
    }

    public void Init(ToolScriptableObject toolObject)
    {

        if (_eqiupTool != null && toolObject == _eqiupTool.GetToolScriptableObject())
        {
            UnEquip();

            Debug.Log("GGGG");
        }
        else
        {

            Debug.Log("GGGG112233");
            if (_eqiupTool != null)
            {
                UnEquip();
            }

           for(int i = 0; i< _toolsList.Count; i++)
            {
                if(toolObject == _toolsList[i].GetToolScriptableObject())
                {
                    _eqiupTool = _toolsList[i];

                    Equip();

                    break;
                }
            }
        }
    }

    private void Equip()
    {
        _animator.SetBool(_eqiupTool.GetStateName(), true);

        _eqiupTool.Equip();

        Debug.Log("equip");
    }

    public void UnEquip()
    {
        if (_eqiupTool == null) return;

        _animator.SetBool(_eqiupTool.GetStateName(), false);

        _eqiupTool.UnEquip();

        _eqiupTool = null;
    }

    public bool CanSwitch()
    {
        return _animator.GetBool("Attack");
    }
}
