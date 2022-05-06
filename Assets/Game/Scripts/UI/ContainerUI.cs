using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContainerUI : InventoryPanelHolderUI
{
    [SerializeField] private RectTransform _slotPanelRectTransform;
    [SerializeField] private RectTransform _mainPanelRectTransform;

    [SerializeField] private ContainerType _containerType;

    [SerializeField] private int _slotSize = 75;
    [SerializeField] private int _slotSpace = 2;
    [SerializeField] private int _panelSpace = 5;
    [SerializeField] private int _heightPanelSpace = 25;

    public void InitSlots(Container container)
    {
        _containerType = container.GetContaineType();

        int width = ((_slotSpace + _slotSize) * _containerType.ColumnAmount) + 2;
        int height = ((_slotSpace + _slotSize) * _containerType.RowAmount) + 2;

        _slotPanelRectTransform.localPosition = new Vector3(0, -_heightPanelSpace / 2f);
        _slotPanelRectTransform.sizeDelta = new Vector2(width, height);

        _mainPanelRectTransform.sizeDelta = new Vector2(width + _panelSpace * 2, height + _panelSpace * 2 + _heightPanelSpace);

        _slotContainer.InitSlots(_containerType.RowAmount * _containerType.ColumnAmount);

        _itemList = container.GetItemList();
        _itemAmountList = container.GetAmountItemList();

        ShowPanel();
    }
}
