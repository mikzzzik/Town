using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NeedItemHolder : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;

    public void Show(Item item)
    {
        _icon.enabled = true;
        _amountText.enabled = true;

        _icon.sprite = item.ItemObject.Icon;
        _amountText.text = item.Amount.ToString();
    }

    public void Hide()
    {
        _icon.enabled = false;
        _amountText.enabled = false;
    }
}
