using UnityEngine;
using TMPro;

public class PanelHolderUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private GameObject _panel;
//    protected float _maxWeight;
//    protected float _nowWeight;

    public void SetWeight(float maxWeight, float nowWeight)
    {
        _weightText.text = string.Format("{0}/{1} kg", nowWeight, maxWeight);
    }

    public virtual bool CheckCanSwitch(Item oldItem, Item newItem) { return false; }


    protected void ShowPanel()
    {
        UIController.OnChangeStatusPanel(_panel);
        UIController.OnChangeStatusInteractiveText(false, null);

        UpdateUI(); 
    }

    public virtual void UpdateUI()
    {

    }
}
