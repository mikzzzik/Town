using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum SlotType { Inventory, Container, Workbench, Craft, Hotbar };

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveTextGameObject;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private List<GameObject> _panelObjectList = new List<GameObject>();

    public static Action<bool, Action> OnChangeStatusInteractiveText;
    public static Action<GameObject> OnChangeStatusPanel;
    public static Action OnAction;
    public static Action OnPressEsc;
    public static Action<Action> OnCheckPanelToAction;

    private Action _nowAction;

    private void OnEnable()
    {
        OnChangeStatusInteractiveText += ChangeStatus;
        OnChangeStatusPanel += ChangePanelStatus;
        OnAction += Action;
        OnPressEsc += PauseMenu;
        OnCheckPanelToAction += CheckPanelToAction;
    }

    private void OnDisable()
    {
        OnChangeStatusInteractiveText -= ChangeStatus;
        OnChangeStatusPanel -= ChangePanelStatus;
        OnAction -= Action;
        OnPressEsc -= PauseMenu;
        OnCheckPanelToAction -= CheckPanelToAction;
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

    public void ChangePanelStatus(GameObject panel)
    {
        if(panel.activeSelf)
            HidePanel(panel);
        else 
            ShowPanel(panel);
    }

    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);

        _panelObjectList.Add        (panel);

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

    private void CheckPanelToAction(Action action)
    {
        if(_panelObjectList.Count <= 0)
        {
            action();
        }
    }

    private void PauseMenu()
    {
        ContextMenuUI.OnHide();

        if(_panelObjectList.Count <= 0)
        {
            ShowPanel(_pauseMenu);
        }
        else
        {
            HideAllPanel();
            CheckPanel();
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void HideAllPanel()
    {

        while (_panelObjectList.Count > 0)
        {
            _panelObjectList[0].SetActive(false);
            _panelObjectList.RemoveAt(0);
    
        }
    }
}

