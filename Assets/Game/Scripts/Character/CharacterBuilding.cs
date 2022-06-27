using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterBuilding : MonoBehaviour
{
    [SerializeField] private Material _buildingMaterial;
    [SerializeField] private Color _canBuildColor;
    [SerializeField] private Color _cantBuildColor;

    public static Action<bool> OnChangeBuildStatus;

    private void OnEnable()
    {
        OnChangeBuildStatus += ChangeBuildStatus;
    }

    private void OnDisable()
    {
        OnChangeBuildStatus -= ChangeBuildStatus;
    }

    private void ChangeBuildStatus(bool status)
    {
        Debug.Log(status);

        if (status)
            _buildingMaterial.color = _canBuildColor;
        else
            _buildingMaterial.color = _cantBuildColor;
    }
}
