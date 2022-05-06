using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanelHolderUI : MonoBehaviour
{
    [SerializeField] protected SlotContainer _slotContainer;
    [SerializeField] private TextMeshProUGUI _weightText;

    protected List<Item> _itemList;
    protected List<int> _itemAmountList;

    private float _maxWeight;

    public void SetWeight(int weight)
    {
        _weightText.text = string.Format("{0}, {1},kg", weight, _maxWeight);
    }

    protected void ShowPanel()
    {
        gameObject.SetActive(true);

        _slotContainer.SetItems(_itemList, _itemAmountList);
    }

}
