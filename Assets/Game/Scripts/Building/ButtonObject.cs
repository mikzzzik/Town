using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    [SerializeField] private InteractiveObject _interactiveObject;

    private void OnTriggerEnter(Collider other)
    {
        ButtonController.OnInterectivObjectTriggerEnter(_interactiveObject.Interactive);
        Debug.Log(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit: " + other);
        ButtonController.OnInterectivObjectTriggerEnter(null);
    }
}
