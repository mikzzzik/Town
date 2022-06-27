using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedObjectToBuildHolder : MonoBehaviour
{
    [SerializeField] private string _needTag = "Pillar";
    public bool GetCanBuild()
    {
  
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f, ~(1 << 14));

        if (hitColliders.Length > 0 && hitColliders[0].tag == _needTag)
        {

            Debug.Log(hitColliders[0].gameObject);

            return true;


        }
        else return false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

}
