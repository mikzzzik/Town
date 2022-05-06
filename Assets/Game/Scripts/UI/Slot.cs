using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemAmountText;

    public void UpdateInfo(Sprite itemIcon, int amount)
    {
        _itemIcon.sprite = itemIcon;
        _itemAmountText.text = amount.ToString();

    }

}
