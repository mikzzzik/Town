using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private float _offSet;

    private void Start()
    {
        if(_offSet == 0)
        {
            _offSet = transform.position.z - _target.position.z ;
        }
    }

    private void Update()
    {


        float smoothPositionHorizontal = Mathf.Lerp(transform.position.x, _target.position.x, _smoothSpeed);
        float smoothPositionVertical = Mathf.Lerp(transform.position.z, _target.position.z + _offSet, _smoothSpeed);

        transform.position = new Vector3(smoothPositionHorizontal, transform.position.y, smoothPositionVertical);
    }
}
