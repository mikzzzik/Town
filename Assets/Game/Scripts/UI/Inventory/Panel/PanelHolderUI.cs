using UnityEngine;
using TMPro;

public class PanelHolderUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private GameObject _panel;
    protected float _maxWeight;
    protected float _nowWeight;

    public void SetWeight(float maxWeight, float nowWeight)
    {
        _weightText.text = string.Format("{0}/{1} kg", nowWeight, maxWeight);
    }

    public virtual bool CheckCanSwitch(Item oldItem, Item newItem) { return false; }
    public bool CanDrag(Item item)
    {
        Debug.Log(item.Amount * item.ItemObject.Weight);
        Debug.Log(_maxWeight - _nowWeight);
        if (item.Amount * item.ItemObject.Weight <= _maxWeight - _nowWeight) return true;
            else return false;
    }

    public float GetAvailableWeight()
    {
        return _maxWeight - _nowWeight;
    }

    protected void ShowPanel()
    {
        UIController.OnChangeStatusPanel(_panel);
        UIController.OnChangeStatusInteractiveText(false, null);

        UpdateUI(); 
    }

    public virtual void UpdateUI()
    {

    }
    protected virtual void CalcWeight()
    {

    }
}
