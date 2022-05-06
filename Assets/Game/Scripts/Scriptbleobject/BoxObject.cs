using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Crate", order = 1)]

public class BoxObject : ScriptableObject
{
    public float MaxWeight;
    public int Slots;
}