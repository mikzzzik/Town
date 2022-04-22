using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Items/item", order = 1)]

public class Item : ScriptableObject
{
    public string Name;
    public string Description;

    public float Weight;
    public float LowerPrice;
    public float MaxPrice;

    public Sprite Icon;

    public GameObject ObjectPrefab;

}
