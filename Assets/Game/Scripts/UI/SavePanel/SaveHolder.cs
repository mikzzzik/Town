using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;


public class SaveHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    private string _name;
    public void Init(string name)
    {
        _name = name;

        _nameText.text = _name;
    }

    public void LoadClick()
    {
        PlayerPrefs.SetString("SaveName", _name);

        SceneManager.LoadScene("LoadingScreen");
    }
    
    public void RemoveClick()
    {
        if(PlayerPrefs.GetString("SaveName") == _name)
        {
            PlayerPrefs.DeleteKey("SaveName");
        }

        string path = Application.persistentDataPath + "/Save/" + _name;
        
        Directory.Delete(path, true);

        Destroy(this.gameObject);
    }
}
