using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private void OnEnable()
    {
        CheckSave();   
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
    
    public void NewWorldClick()
    {
        if(PlayerPrefs.HasKey("SaveName"))
        {
            PlayerPrefs.DeleteKey("SaveName");
        }
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);

    }

    public void CheckSave()
    {
        if (PlayerPrefs.HasKey("SaveName") && new DirectoryInfo(Application.persistentDataPath + "/Save/").GetDirectories().Length > 0)
            _playButton.interactable = true;
        else _playButton.interactable = false;
    }

    public void ExitButton()
    {
        Application.Quit();
    }


}
