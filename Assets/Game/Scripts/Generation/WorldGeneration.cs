using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> _treeGameObjectPrefabList;
    [SerializeField] private GameObject _woodResourcePrefab;
    [SerializeField] private GameObject _stoneResourcePrefab;

    [SerializeField] private BoxCollider _treeSpawnZone;
    [SerializeField] private BoxCollider _woodResourceSpawnZone;
    [SerializeField] private BoxCollider _stoneResourceSpawnZone;

    [SerializeField] private int _treeGenerateAmount;
    [SerializeField] private int _stoneResourceGenerateAmount;
    [SerializeField] private int _woodResourceGenerateAmount;

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private TerrainGeneration _terrainGeneration;
    void OnEnable()
    {
        if(!PlayerPrefs.HasKey("SaveName"))
        {
            // _terrainGeneration.Generate();
            BeginGeration();
        }
    }

    private void BeginGeration()
    {
        GenerationTree();
        GenerationWoodResource();
        GenerationStoneResource();
    }

    private void GenerationTree()
    {
        Generation(_treeGenerateAmount, _treeSpawnZone, new Vector3(3f, 3f, 3f), _treeGameObjectPrefabList[0]);
    }

    private void Generation(int amount, BoxCollider boxCollider, Vector3 colliderSize, GameObject prefab)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 point = RandomPointInBounds(boxCollider.bounds);

            RaycastHit hit;
            if (Physics.Raycast(point, Vector3.down, out hit, 100f))
            {
                Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);


                Collider[] collidersInsideBox = new Collider[1];

                int hitColliderCount = Physics.OverlapBoxNonAlloc(hit.point, colliderSize, collidersInsideBox, spawnRotation, _layerMask);

                if (hitColliderCount == 0)
                {
                    Instantiate(prefab, hit.point, spawnRotation);
                }
                else
                {
                    i--;
                }
            }
            else i--;

        }

        boxCollider.enabled = false;
    }

    private void GenerationWoodResource()
    {
        Generation(_woodResourceGenerateAmount, _woodResourceSpawnZone, new Vector3(1f, 1f, 1f), _woodResourcePrefab);
    }

    private void GenerationStoneResource()
    {
        Generation(_stoneResourceGenerateAmount, _stoneResourceSpawnZone, new Vector3(1f, 1f, 1f), _stoneResourcePrefab);
    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
    }
}
