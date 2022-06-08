using UnityEngine;

public class PickUpItem : InteractiveObject
{

    [SerializeField] private Item _item;

    private void Awake()
    {
        ResourceManager.OnSaveResourceData += SaveData;
    }

    private void OnDisable()
    {
        ResourceManager.OnSaveResourceData -= SaveData;
    }

    public void Drop(int amount)
    {
        _item.Amount = amount;
    }

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

    private void SaveData()
    {
        PickUpItemInfo resourceInfo = new PickUpItemInfo(transform.position, transform.rotation, _item);


        ResourceManager.OnAddToList(resourceInfo);
    }
}
