using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftItemInfoContainer : MonoBehaviour
{
    [SerializeField] private CharacterInventory _characterInventory;
    [SerializeField] private InventoryPanelUI _inventoryPanelUI;

    [SerializeField] private WorkbenchUI _workbenchUI;

    [SerializeField] private Button _button;

    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private TextMeshProUGUI _itemAmountText;

    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _progressBar;

    [SerializeField] private List<Slot> _slotList;

    private int _slotIndex;

    private float _timer;
    private ItemScriptableObject _itemObject;

    private bool _status = true;

    private void OnDisable()
    {
        Hide();
    }

    public void Init(ItemScriptableObject itemObject)
    {
        StopAllCoroutines();

        _itemObject = itemObject;

        gameObject.SetActive(true);

        _timerText.text = _itemObject.TimeToCraft.ToString();
        _itemNameText.text = _itemObject.Name.ToString();
        _itemDescriptionText.text = _itemObject.Description.ToString();
        _itemAmountText.text = _itemObject.CraftAmount.ToString();
      
        _itemIcon.sprite = _itemObject.Icon;
        _progressBar.fillAmount = 1f;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Hide()
    {
        gameObject.SetActive(false);

        Cancel();
    }

    public void Click()
    {

        if (_status)
        {
            Craft();
        }
        else
        {
            Cancel();
        }
    }

    public void ChangeStatus(bool status)
    {
        _status = status;
    }

    private void Craft()
    {

        _buttonText.text = "Cancel";

        for (int i = 0; i < _slotList.Count; i++)
        {
            Debug.Log(_slotList[i].GetItem().ItemObject);
            if(_slotList[i].GetItem().ItemObject == null)
            {
                ChangeStatus(false);
                
                _slotIndex = i;

                _timer = _itemObject.TimeToCraft;

                StartCoroutine(DoCraft());

                return;
            }
        }
        Debug.Log("You don't have free slots");
       
    }

    private void Cancel()
    {
        StopAllCoroutines();

        _buttonText.text = "Craft";

        ChangeStatus(true);
    }

    IEnumerator DoCraft()
    {
        float nowTimer = _timer;

        for (; nowTimer > 0; nowTimer -= 0.1f)
        {

            _progressBar.fillAmount = nowTimer / _timer;

            _timerText.text = nowTimer.ToString("0.0 sec");

            yield return new WaitForSeconds(.1f);
        }

        if (nowTimer <= 0)
        {
            Crafted();
        }
    }
    
    public void ChangeButtonStatus(bool status)
    {
        _button.interactable = status;
    }

    private void Crafted()
    {
        _buttonText.text = "Craft";

        List<Item> characterItemList = _characterInventory.GetItemList();

        Item tempItem = new Item();

        tempItem.SetItem(_itemObject, _itemObject.CraftAmount);

        for (int i = 0; i < _itemObject.ItemToCraft.Count; i++)
        {
            int needAmount = _itemObject.ItemToCraft[i].Amount;
            for (int j = 0; j < characterItemList.Count; j++)
            {
                if (_itemObject.ItemToCraft[i].ItemObject == characterItemList[j].ItemObject)
                {
                    if(characterItemList[j].Amount > needAmount)
                    {
                        characterItemList[j].Amount -= needAmount;
                    }
                    else
                    {
                        needAmount -= characterItemList[j].Amount;
                        characterItemList[j].Clear();
                    }
                }
            }
        }
        _progressBar.fillAmount = 1f;
        _timerText.text = _itemObject.TimeToCraft.ToString();

        _inventoryPanelUI.UpdateUI();

        _slotList[_slotIndex].UpdateInfo(tempItem);

        _workbenchUI.UpdateViewAfterCraft();
    }
}
