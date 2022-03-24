using System.Collections.Generic;
using UnityEngine;

using System;

public enum ContextAction
{
    Move,
    Cancel
}

public class ContextMenu : MonoBehaviour
{
    [SerializeField] private List<ActionButton> _actionButton;
    [SerializeField] private RectTransform _panelTransform;
    [SerializeField] private float _buttonHeight = 42f;


    private Action _action;

    public static Action<List<ContextAction>> OnShowContextMenu;
    public static Action<Action> OnSetAction;

    private List<ContextAction> _contextActionList;
    private List<ActionButton> _activeActionButton = new List<ActionButton>();

    private void Start()
    {

    }

    private void OnEnable()
    {
        OnShowContextMenu += ShowContextMenu;
        OnSetAction += SetAction;
    }

    private void OnDisable()
    {
        OnShowContextMenu -= ShowContextMenu;
        OnSetAction -= SetAction;
    }

    private void SetAction(Action action)
    {
        _action = action;
    }

    private void ShowContextMenu(List<ContextAction> actionList)
    {
        _contextActionList = actionList;
       
        HideButton();
        ShowButton();
    }

    private void ShowButton()
    {
        _activeActionButton = new List<ActionButton>();

        _panelTransform.gameObject.SetActive(true);

        _panelTransform.sizeDelta = new Vector2(_panelTransform.sizeDelta.x, _buttonHeight * _contextActionList.Count);
        _panelTransform.position = new Vector2(Input.mousePosition.x+_panelTransform.sizeDelta.x/2, Input.mousePosition.y - _panelTransform.sizeDelta.y / 2);

        for (int i = 0; i < _actionButton.Count; i++)
        {
            for (int j = 0; j < _contextActionList.Count; j++)
            {
                if (_contextActionList[j] == _actionButton[i].GetAction())
                {
                    _actionButton[i].gameObject.SetActive(true);

                    _activeActionButton.Add(_actionButton[i]);
                    break;
                }
            }
        }
    }

    private void HideButton()
    {
        if (_activeActionButton.Count <= 0)
        {
            for (int i = 0; i < _actionButton.Count; i++)
            {
                _actionButton[i].gameObject.SetActive(false);

            }
        }
        else
        {
            for(int i = 0; i< _activeActionButton.Count;i++)
            {
                _activeActionButton[i].gameObject.SetActive(false);
            }
            _activeActionButton.Clear();
        }
    }

    public void Move()
    {
        CharacterMoving.OnMove();
        
        Cancel();
    }


    public void Cancel()
    {
        _panelTransform.gameObject.SetActive(false);
    }
}
