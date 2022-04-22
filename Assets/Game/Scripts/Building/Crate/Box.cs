using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private BoxType _boxType;

    private void OnTriggerEnter(Collider other)
    {
        CharacterInteractive.OnBoxTriggerEnter(_boxType);
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterInteractive.OnBoxTriggerEnter(BoxType.none);
    }
}
