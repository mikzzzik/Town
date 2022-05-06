using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Container", order = 1)]

public class ContainerType : ScriptableObject
{
    public float MaxWeight;
    public int RowAmount;
    public int ColumnAmount;
    
}
