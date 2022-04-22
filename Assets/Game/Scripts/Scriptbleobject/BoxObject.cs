using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Crate/Box", order = 1)]

public class BoxObject : ScriptableObject
{
    public float MaxWeight;
    public int Slots;
}