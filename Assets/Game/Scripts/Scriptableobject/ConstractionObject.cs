using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConstractionObject", order = 1)]


public class ConstractionObject : ScriptableObject
{
    public ConstractionType Type;
    public GameObject PrafabObject;
    public List<Item> NeedItem;
}

public enum ConstractionType { Wall, Foundation, Pillar, Ramp, Floor }