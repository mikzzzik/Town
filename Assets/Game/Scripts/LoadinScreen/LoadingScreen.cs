using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(LoadAsync());   
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainLocation");

        while (!operation.isDone)
        {

            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
