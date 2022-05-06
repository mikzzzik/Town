using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotContainer : MonoBehaviour
{
    [SerializeField] private List<Slot> _slotList = new List<Slot>();
    [SerializeField] private Slot _slotPrefab;

    private List<Item> _itemList;
    private List<int> _itemAmountList;

    private void CalcWeight()
    {
        float weight = 0f;
        for(int i =0; i < _slotList.Count; i++)
        {
            weight += _itemList[i].Weight * _itemAmountList[i];
        }


    }

    public void InitSlots(int amount)
    {
        if (_slotList.Count == 0)
        {
            if (TryGetComponent(out Slot slot))
            {
                _slotList.AddRange(gameObject.GetComponentsInChildren<Slot>());
            }
        }


        if (_slotList.Count == amount) return;
        else
        {
            for (int i = 0; i < _slotList.Count; i++)
            {
                Destroy(_slotList[i].gameObject);
            }
            _slotList.Clear();
        }

        for(int i = 0; i < amount; i++)
        {
            Slot slot = Instantiate(_slotPrefab) as Slot;

            _slotList.Add(slot);

            slot.transform.parent = this.transform;
        }
    }

    public void SetItems(List<Item> itemList, List<int> itemAmountList)
    {
        _itemList = itemList;
        _itemAmountList = itemAmountList;
        
        UpdateAllUI();
    }

    private void UpdateAllUI()
    {
         CalcWeight();

       for(int i = 0; i < _slotList.Count; i++)
        {

            _slotList[i].UpdateInfo(_itemList[i].Icon, _itemAmountList[i]);
        }
    }
}
