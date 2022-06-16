using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ResourceObject", order = 1)]

public class ResourceObject : ScriptableObject {

    public GameObject Prefab;
    public ResourceType ResourceType;
}

public enum ResourceType { Wood, Stone, Iron }