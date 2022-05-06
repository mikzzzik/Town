using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<BoxCarrying> _boxCarryingList;

    private BoxCarrying _nowBox;

    public static Action<BoxType> OnChooseBox;
    public static Action<Container> OnOpenContainer;

    private void OnEnable()
    {
        OnChooseBox += ChooseBox;
        OnOpenContainer += OpenContainer;
    }

    private void OnDisable()
    {
        OnChooseBox -= ChooseBox;
        OnOpenContainer -= OpenContainer;
    }

    public BoxCarrying GetBox()
    {
        return _nowBox;
    }

    private void ChooseBox(BoxType boxType)
    {
        if (boxType == BoxType.none)
        {
            _nowBox = null;
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

    private void OpenContainer(Container container)
    {

    }
}
