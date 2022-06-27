using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToBuild : MonoBehaviour
{
    [SerializeField] private List<NeedObjectToBuildHolder> _needObjectToBuildList;

    private void OnEnable()
    {
        CharacterBuilding.OnChangeBuildStatus(false);     
    }

    public void CheckCanBuild()
    {
        Debug.Log("Check");
        for(int i = 0; _needObjectToBuildList.Count > i; i++)
        {
            Debug.Log(i + "  " + _needObjectToBuildList.Count);
            if (!_needObjectToBuildList[i].GetCanBuild())
            {
                Debug.Log(_needObjectToBuildList[i].GetCanBuild());

                return;
            }
        }

        Debug.Log("True");
        CharacterBuilding.OnChangeBuildStatus(true);
    }
}
