using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;
    private GameObject savedGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void LoadGame()
    {
        savedGame = null;
    }

    public bool CheckSavedGame()
    {
        LoadGame();
        if (savedGame != null)
        {
            return true;
        }
        return false;
    }


    public void SaveSettings(SettingsData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.stx";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.stx";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            return data;
        }
        else
        {
            return new SettingsData() 
            { 
                soundOn = true, 
                volume = 10f 
            };
        }
    }

    [System.Serializable]
    public class SettingsData
    {
        public bool soundOn;
        public float volume;
    }
}
