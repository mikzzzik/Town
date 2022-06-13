using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterToolController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Tool> _toolsList;

    private Tool _eqiupTool;
    private Item _nowEquipItem;

    public static Action OnCheckEquipTool;
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
        OnCheckEquipTool += CheckEquipItem;
    }

    private void OnDisable()
    {
        OnAttack -= Attack;
        OnCheckEquipTool -= CheckEquipItem;
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

    public void Init(Item item)
    {
        _nowEquipItem = item;

        ToolScriptableObject toolObject = item.ItemObject as ToolScriptableObject;

        Debug.Log(_nowEquipItem);

        if (_eqiupTool != null && toolObject == _eqiupTool.GetToolScriptableObject())
        {
            UnEquip();

        }
        else
        {
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

    private void CheckEquipItem()
    {
        if(_nowEquipItem != null && _nowEquipItem.ItemObject == null)
        {
            UnEquip();
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
        _animator.SetBool("Attack", false);


        _eqiupTool.UnEquip();

        _nowEquipItem = null;
        
        _eqiupTool = null;
    }

    public bool CanSwitch()
    {
        return _animator.GetBool("Attack");
    }
}
