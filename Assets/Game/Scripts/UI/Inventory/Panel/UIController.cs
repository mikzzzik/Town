using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum SlotType { Inventory, Container, Workbench, Craft, Hotbar };

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveTextGameObject;
    [SerializeField] private GameObject _pauseMenu;
    private List<GameObject> _panelObjectList = new List<GameObject>();

    public static Action<bool, Action> OnChangeStatusInteractiveText;
    public static Action<GameObject> OnChangeStatusPanel;
    public static Action OnAction;
    public static Action OnPressEsc;

    private Action _nowAction;

    private void OnEnable()
    {
        OnChangeStatusInteractiveText += ChangeStatus;
        OnChangeStatusPanel += ChangePanelStatus;
        OnAction += Action;
        OnPressEsc += PauseMenu;
    }

    private void OnDisable()
    {
        OnChangeStatusInteractiveText -= ChangeStatus;
        OnChangeStatusPanel -= ChangePanelStatus;
        OnAction -= Action;
        OnPressEsc -= PauseMenu;
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
            Debug.Log("GG");
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void PauseMenu()
    {
        ContextMenuUI.OnHide();

        if(_panelObjectList.Count <= 0)
        {
            Debug.Log("11");
            ShowPanel(_pauseMenu);
        }
        else
        {
            HideAllPanel();
            CheckPanel();
            Debug.Log("22");
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void HideAllPanel()
    {
        
        for(int i = 0; i < _panelObjectList.Count; i++)
        {
            
            _panelObjectList[0].SetActive(false);
            _panelObjectList.RemoveAt(0);
        }
    }
}

