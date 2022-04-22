using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList;

    [SerializeField] private List<int> _itemAmountList;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.TryGetComponent(out CharacterInventory characterInventory))
            {
                if(characterInventory.GetBox() != null)
                {

                }
            }
        }
    }
}
