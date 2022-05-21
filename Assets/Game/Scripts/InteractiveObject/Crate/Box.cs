using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private BoxType _boxType;

    private void OnTriggerEnter(Collider other)
    {
        ButtonController.OnBoxTriggerEnter(_boxType);
    }

    private void OnTriggerExit(Collider other)
    {
        ButtonController.OnBoxTriggerEnter(BoxType.none);
    }
}
