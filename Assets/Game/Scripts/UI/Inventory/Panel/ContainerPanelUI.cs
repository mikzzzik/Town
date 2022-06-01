using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContainerPanelUI : PanelHolderUI
{
    [SerializeField] private RectTransform _slotPanelRectTransform;
    [SerializeField] private RectTransform _mainPanelRectTransform;

    [SerializeField] private int _slotSize = 75;
    [SerializeField] private int _slotSpace = 2;
    [SerializeField] private int _panelSpace = 5;
    [SerializeField] private int _heightPanelSpace = 25;

    [SerializeField] private Slot _slotPrefab;

    private List<Slot> _slotList = new List<Slot>();
    private List<Item> _itemList;

    private ContainerType _containerType;
    private Container _container;

    public void InitSlots(Container container)
    {
        _container = container;

        _containerType = _container.GetContaineType();

        int width = ((_slotSpace + _slotSize) * _containerType.ColumnAmount) + 2;
        int height = ((_slotSpace + _slotSize) * _containerType.RowAmount) + 2;

        _slotPanelRectTransform.localPosition = new Vector3(0, -_heightPanelSpace / 2f);
        _slotPanelRectTransform.sizeDelta = new Vector2(width, height);

        _mainPanelRectTransform.sizeDelta = new Vector2(width + _panelSpace * 2, height + _panelSpace * 2 + _heightPanelSpace);

        _itemList = _container.GetItemList();
        _maxWeight = _containerType.MaxWeight;

        InstantiateSlot();
        CalcWeight();
        ShowPanel();

        CharacterInventory.OnShow();
    }

    protected override void CalcWeight()
    {
        _nowWeight = 0;

        List<Item> itemList = new List<Item>(_itemList);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ItemObject != null)
            {
                _nowWeight += itemList[i].ItemObject.Weight * itemList[i].Amount;
            }
        }

        SetWeight(_maxWeight, _nowWeight);
    }

    private void InstantiateSlot()
    {
        if (_slotList.Count == 0)
        {
            if (TryGetComponent(out Slot slot))
            {
                _slotList.AddRange(gameObject.GetComponentsInChildren<Slot>());
            }
        }

        int slotCount = _containerType.ColumnAmount * _containerType.RowAmount;

        if (_slotList.Count == slotCount) return;
        else
        {
            for (int i = 0; i < _slotList.Count; i++)
            {
                Destroy(_slotList[i].gameObject);
            }
            _slotList.Clear();
        }

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(_slotPrefab) as Slot;

            _slotList.Add(slot);

            slot.transform.SetParent(_slotPanelRectTransform, false);

            slot.ClearSlot();
        }

        UpdateUI(); 
    }

    public override void UpdateUI()
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            _slotList[i].UpdateInfo(_itemList[i]);
        }
        CalcWeight(); 
    }
}
