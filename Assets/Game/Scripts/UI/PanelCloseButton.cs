using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCloseButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hidePanelGameObject;
    [SerializeField] private Button _button;

    private void Start()
    {
        if(_button == null)
        {
            _button = GetComponent<Button>();
        }
        _button.onClick.AddListener(Click);
    }

    private void Click()
    {
        Debug.Log("Close button - Click");

        for(int i = 0; i < _hidePanelGameObject.Count; i++)
        {
            if (_hidePanelGameObject[i].activeSelf)
                UIController.OnChangeStatusPanel(_hidePanelGameObject[i]);
        }
    }
}
