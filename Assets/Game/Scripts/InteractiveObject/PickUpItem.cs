using UnityEngine;

public class PickUpItem : InteractiveObject
{
    [SerializeField] private Item _item;
    
    protected override void Active()
    {
        base.Active();
        
        CharacterInventory.OnPickUpItem(this);
    }

    public Item GetItem()
    {
        return _item;
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
