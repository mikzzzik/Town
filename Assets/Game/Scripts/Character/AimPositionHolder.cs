using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPositionHolder : MonoBehaviour
{
    [SerializeField] private Transform _transformNewPos;
    [SerializeField] private ObjectToBuild _nowObjectToBuild;

    private void Start()
    {
        //_wallTransform.position = _transformNewPos.position;
        //_wallTransform.rotation = _transformNewPos.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
  
        if (other.tag == _nowObjectToBuild.transform.tag)
        {
          // Debug.Log("Enter: " + other.name);

            _nowObjectToBuild.transform.SetParent(null);
            _nowObjectToBuild.transform.position = other.transform.position;
            _nowObjectToBuild.transform.rotation = other.transform.rotation;

            _nowObjectToBuild.CheckCanBuild();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _nowObjectToBuild.tag)
        {
        //    Debug.Log("Exit: " + other.name);

            _nowObjectToBuild.transform.SetParent(this.transform);
            _nowObjectToBuild.transform.localPosition = Vector3.zero;
            _nowObjectToBuild.transform.localEulerAngles = Vector3.zero;

            CharacterBuilding.OnChangeBuildStatus(false);
        }
    }
  
}
