using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotContainer : MonoBehaviour
{
    [SerializeField] private List<SlotHolder> _slotList = new List<SlotHolder>();
    [SerializeField] private SlotHolder _slotPrefab;

    private List<Item> _itemList;

    public float CalcWeight()
    {
        float weight = 0f;

        for(int i = 0; i < _slotList.Count; i++)
        {
            if(_itemList[i].ItemObject != null)
             weight += _itemList[i].ItemObject.Weight * _itemList[i].Amount;
        }
        return weight;

    }

    public void InitSlots(int amount)
    {
        if (_slotList.Count == 0)
        {
            if (TryGetComponent(out SlotHolder slot))
            {
                _slotList.AddRange(gameObject.GetComponentsInChildren<SlotHolder>());
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
            SlotHolder slot = Instantiate(_slotPrefab) as SlotHolder;

            _slotList.Add(slot);

            slot.transform.SetParent(this.transform, false);

            slot.ClearSlot();
        }
    }

    public void SetItems(List<Item> itemList)
    {
        _itemList = itemList;

        UpdateAllUI();
    }

    private void UpdateAllUI()
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            _slotList[i].UpdateInfo(_itemList[i]);
        }
    }
}
