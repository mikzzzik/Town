using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{

    public static string PlayerDataName = "Player.save";
    public static string CameraDataName = "Camera.save";
    public static string PickUpItemDataName = "PickUpItem.save";
    public static string ResourceObjectDataName = "ResourceObject.save";


    public static Action OnSaveData;
    public static Action OnLoadData;



    private void Start()
    {

        if (PlayerPrefs.HasKey("SaveName"))
            OnLoadData();

    }

    public static void SaveToJson<T>(string fileName,T data)
    {
        string path = Application.persistentDataPath + "/Save/" + PlayerPrefs.GetString("SaveName") + "/";

        if (File.Exists(path + fileName)) Debug.Log("File found");
            else Debug.Log("File doesn't found");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.WriteAllText(path + fileName, JsonUtility.ToJson(data, true));

        if (File.Exists(path + fileName)) Debug.Log("File found");
            else Debug.Log("File doesn't found");
    }

    public static T LoadFromJson<T>(string fileName) where T : class
    {
        string path = Application.persistentDataPath + "/Save/" + PlayerPrefs.GetString("SaveName") + "/";

        if (File.Exists(path + fileName))
        {

            string jsonText = File.ReadAllText(path + fileName);
            Debug.Log(JsonUtility.FromJson<T>(jsonText));

            return JsonUtility.FromJson<T>(jsonText);
        }
        {
            Debug.Log("Save doesn't found");

            return null;
        }
    }
}
