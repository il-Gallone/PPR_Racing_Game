using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStats stats = new GameStats();
    public float levelLimits;

    public int scrapCollected = 0;
    public int weaponPartsCollected = 0;
    public int enginePartsCollected = 0;
    public int armourPartsCollected = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (PlayerPrefs.HasKey("SaveExists"))
        {
            LoadData();
        }
        else
        {
            stats.inventory = new List<string> { "Shotgun Blueprint", "Machinegun Blueprint" };
            SaveData();
            PlayerPrefs.SetInt("SaveExists", 0);
        }
    }

    public void SaveData()
    {
        string dataPath = Path.Combine(Application.dataPath, "saveData.txt");
        using(StreamWriter streamWriter = File.CreateText(dataPath))
        {
            string jsonString;
            jsonString = JsonConvert.SerializeObject(stats);
            streamWriter.Write(jsonString);
        }
    }

    public void LoadData()
    {
        string dataPath = Path.Combine(Application.dataPath, "saveData.txt");
        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            string jsonString;
            jsonString = streamReader.ReadToEnd();
            stats = JsonConvert.DeserializeObject<GameStats>(jsonString);
        }
    }

    
}
