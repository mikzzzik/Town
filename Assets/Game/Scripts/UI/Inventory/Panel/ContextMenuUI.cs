using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public enum ActionType { Equip, Drop};
public class ContextMenuUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private InventoryPanelUI _inventoryPanelUI;

    [SerializeField] private RectTransform _contextPanel;
    [SerializeField] private List<ButtonType> _buttonType;

    private Slot _nowSlot;

    [System.Serializable]
    public struct ButtonType
    {
        public GameObject Button;
        public ActionType Type;
    }

    public static Action<Vector2, Slot> OnShowContext;
    public static Action OnHide;

    private void OnEnable()
    {
        OnShowContext += Show;
        OnHide += Hide;
    }

    private void OnDisable()
    {
        OnShowContext -= Show;
        OnHide -= Hide;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ContextMenuUI.OnHide();
    }

    private void Show(Vector2 mousePos, Slot slot)
    {
        for(int i = 0;i < _buttonType.Count;i++)
        {
            _buttonType[i].Button.SetActive(false);
        }

        int count = 0;

        _nowSlot = slot;
        
        ItemScriptableObject itemObject = _nowSlot.GetItem().ItemObject;

        for (int i = 0; i < _buttonType.Count; i++)
        {
            for(int j =0; j < itemObject.ActionList.Count;j++)
            {
                if (_buttonType[i].Type == itemObject.ActionList[j])
                {
                    count++;        

                    _buttonType[i].Button.SetActive(true);
                }
            }
        }
        _contextPanel.sizeDelta = new Vector2(100, count * 25);

        _contextPanel.position = mousePos + new Vector2(_contextPanel.sizeDelta.x / 2, -_contextPanel.sizeDelta.y);

       
        _contextPanel.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _contextPanel.gameObject.SetActive(false);
    }
    
    public void Equip()
    {
        Hide();
    }

    public void Drop()
    {
        CharacterInventory.OnDropItem(_nowSlot.GetItem());

        _nowSlot.GetItem().Clear();
        
        _inventoryPanelUI.UpdateUI();
        
        Hide();
    }
}
