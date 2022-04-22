using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<BoxCarrying> _boxCarryingList;

    private BoxCarrying _nowBox;

    public static Action<BoxType> OnChooseBox;

    private void OnEnable()
    {
        OnChooseBox += ChooseBox;
    }

    private void OnDisable()
    {
        OnChooseBox -= ChooseBox;
    }

    public BoxCarrying GetBox()
    {
        return _nowBox;
    }

    private void ChooseBox(BoxType boxType)
    {
        if (boxType == BoxType.none)
        {
            return;
        }

        for (int i = 0; i < _boxCarryingList.Count; i++)
        {
            if (boxType == _boxCarryingList[i].GetBoxType())
            {
                _nowBox = _boxCarryingList[i];

                _nowBox.Show();
            }
        }
    }
}
