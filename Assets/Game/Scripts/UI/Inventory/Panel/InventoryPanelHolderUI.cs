using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanelHolderUI : MonoBehaviour
{
    [SerializeField] protected SlotContainer _slotContainer;
    [SerializeField] private TextMeshProUGUI _weightText;

    [SerializeField] protected List<Item> _itemList;

    private float _maxWeight;
    private float _nowWeight;

    private void UpdateWeightText()
    { 
        _nowWeight = _slotContainer.CalcWeight();

        _weightText.text = string.Format("{0}/{1} kg", _nowWeight, _maxWeight);
    }

    public void SetMaxWeight(float maxWeight)
    {
        _maxWeight = maxWeight;
    }
    
    public float GetAvailableWeight()
    {
        return _maxWeight - _nowWeight;
    }

    public bool CheckWeight(Item oldItem, Item newItem)
    {
        float weight = 0f;

        for(int i = 0; i < _itemList.Count;i++)
        {
            if(_itemList[i] != oldItem)
            {
                if (_itemList[i].ItemObject != null)
                    weight += _itemList[i].ItemObject.Weight * _itemList[i].Amount;
            }
            else
            {
                if (newItem.ItemObject != null)
                    weight += newItem.ItemObject.Weight * newItem.Amount;
            }
        }

        if (weight <= _maxWeight)
        {
            return true;
        }
        else return false;
    }

    protected void ShowPanel()
    {
        UIController.OnChangeStatusPanel(gameObject);
        UIController.OnChangeStatusInteractiveText(false, null);

        UpdateUI(); 
    }

    public void UpdateUI()
    {
        _slotContainer.SetItems(_itemList);

        UpdateWeightText();
    }

}
