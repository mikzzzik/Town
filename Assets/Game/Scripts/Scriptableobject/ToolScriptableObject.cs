using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ToolObject", order = 2)]

public class ToolScriptableObject : ItemScriptableObject
{
    public int Tier = 1;
    public int MaxDuration = 100;
}
