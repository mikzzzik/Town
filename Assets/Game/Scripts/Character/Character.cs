using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;

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

    public void BeginLoad()
    {
        _characterController.enabled = false;
    }

    public void Loaded()
    {
        _characterController.enabled = true;
    }

    
}
