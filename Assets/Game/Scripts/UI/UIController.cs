using UnityEngine;
using System;
using System.Collections.Generic;
public enum SlotType { Inventory, Container };

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveTextGameObject;

    private List<GameObject> _panelObjectList = new List<GameObject>();

    public static Action<bool, Action> OnChangeStatusInteractiveText;
    public static Action<GameObject> OnChangeStatusPanel;
    public static Action OnAction;
    
    private Action _nowAction;

    private void OnEnable()
    {
        OnChangeStatusInteractiveText += ChangeStatus;
        OnChangeStatusPanel += ChangePanelStatus;
        OnAction += Action;
    }

    private void OnDisable()
    {
        OnChangeStatusInteractiveText -= ChangeStatus;
        OnChangeStatusPanel -= ChangePanelStatus;
        OnAction -= Action;
    }

    private void ChangeStatus(bool status, Action action)
    {
        if (_interactiveTextGameObject.activeSelf && status) return;
        
        _nowAction = action;

        _interactiveTextGameObject.SetActive(status);
    }

    private void Action()
    {

        if (_nowAction != null)
        {
            _nowAction();

            _nowAction = null;
        }
    }

    private void ChangePanelStatus(GameObject panel)
    {
        if(panel.activeSelf)
            HidePanel(panel);
        else 
            ShowPanel(panel);
    }

    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);

        _panelObjectList.Add(panel);

        MouseInput.OnChangeStatus(false);
        
        Cursor.lockState = CursorLockMode.Confined;
        
        UIController.OnChangeStatusInteractiveText(false, null);
    }

    private void HidePanel(GameObject panel)
    {
        panel.SetActive(false);

        _panelObjectList.Remove(panel);

        CheckPanel();
    }

    private void CheckPanel()
    {
        if(_panelObjectList.Count <= 0)
        {
            MouseInput.OnChangeStatus(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

