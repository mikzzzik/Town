using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private ResourceObject _resourceObject;
    [SerializeField] private GameObject _woodPrefab;
    [SerializeField] private List<Transform> _instantiateTransformList;

    [SerializeField] private List<GameObject> _pieceGameObjectList;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private CapsuleCollider _treeCollider;

    [SerializeField] private CapsuleCollider _mainCollider;

    private void Awake()
    {
        ResourceManager.OnSaveResourceObject += SaveData;
    }

    private void OnDisable()
    {
        ResourceManager.OnSaveResourceObject -= SaveData;
    }
    private void SaveData()
    {
        ResourceObjectInfo resourceObjectInfo = new ResourceObjectInfo(transform.position, transform.rotation, _resourceObject);

        ResourceManager.OnAddResourceObjectToList(resourceObjectInfo);
    }

    private void Start()
    {
        _mainCollider.enabled = true;
        _treeCollider.enabled = false;

        _rigidBody.useGravity = false;
    }

    private void Hit()
    {
        if(_pieceGameObjectList.Count > 0)
        {
            Destroy(_pieceGameObjectList[0]);

            _pieceGameObjectList.RemoveAt(0);

            if(_pieceGameObjectList.Count <= 0)
            {
                StartCoroutine(Fall());
            }
        }
    }

    IEnumerator Fall()
    {
        _mainCollider.enabled = false;
        _treeCollider.enabled = true;

        _rigidBody.useGravity = true;

        yield return new WaitForSeconds(4);

        for (int i = 0; i < _instantiateTransformList.Count; i++)
        {
            GameObject wood = Instantiate(_woodPrefab);

            wood.transform.position = _instantiateTransformList[i].position + Vector3.up * 0.5f;
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Tool")
        {
            Hit();

            Debug.Log("Tool hit");

            Tool.OnDisableCollision();
        }
    }
}
