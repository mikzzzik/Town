using UnityEngine;
using System;

public enum Place
{
    InHouse,
    InBuilding,
    InOutside
}

public enum Status
{ 
  Stay,
  Move,
  Action
}

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterInventory _characterInventory;
    [SerializeField] private CharacterMoving _characterMoving;
    [SerializeField] private CharacterToolController _characterToolController;

    public CharacterInventory GetCharacterInventory()
    {
        return _characterInventory;
    }

    public CharacterMoving GetCharacterMoving()
    {
        return _characterMoving;
    }

    public CharacterToolController GetCharacterToolController()
    {
        return _characterToolController;
    }
}
