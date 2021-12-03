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
    public int weaponPartChance;
    public int enginePartChance;
    public int armourPartChance;
    public int primaryEnemySpawn; //0 = Swarmer, 1 = Detonator, 2 = Collector, 3 = CollectorPacifist
    public int mapGenerationSeed; //0 = Alpha Homeworld, 1 = Beta Homeworld, 2 = Gamma Homeworld, 3 = Omega Homeworld, 4 = Epsilon Hideout

    public int scrapCollected = 0;
    public int weaponPartsCollected = 0;
    public int enginePartsCollected = 0;
    public int armourPartsCollected = 0;

    public static bool saveInitialized = false;

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
            NewSave();
        }
        GameManager.saveInitialized = true;

    }

    public void ShopStock(int repeats)
    {
        for (int i = 0; i < repeats; i++)
        {
            if (stats.shopBlueprintsT1.Count != 0)
            {
                int randomBlueprint = Random.Range(0, stats.shopBlueprintsT1.Count);
                string selectedBlueprint = stats.shopBlueprintsT1[randomBlueprint];
                stats.availableShopBlueprints.Add(selectedBlueprint);
                stats.shopBlueprintsT1.Remove(selectedBlueprint);
            }
            else if (stats.shopBlueprintsT2.Count != 0)
            {
                int randomBlueprint = Random.Range(0, stats.shopBlueprintsT2.Count);
                string selectedBlueprint = stats.shopBlueprintsT2[randomBlueprint];
                stats.availableShopBlueprints.Add(selectedBlueprint);
                stats.shopBlueprintsT2.Remove(selectedBlueprint);
            }
            else if (stats.shopBlueprintsT3.Count != 0)
            {
                int randomBlueprint = Random.Range(0, stats.shopBlueprintsT3.Count);
                string selectedBlueprint = stats.shopBlueprintsT3[randomBlueprint];
                stats.availableShopBlueprints.Add(selectedBlueprint);
                stats.shopBlueprintsT3.Remove(selectedBlueprint);
            }
            else if (stats.shopBlueprintsT4.Count != 0)
            {
                int randomBlueprint = Random.Range(0, stats.shopBlueprintsT4.Count);
                string selectedBlueprint = stats.shopBlueprintsT4[randomBlueprint];
                stats.availableShopBlueprints.Add(selectedBlueprint);
                stats.shopBlueprintsT4.Remove(selectedBlueprint);
            }
            else if (stats.shopBlueprintsT5.Count != 0)
            {
                int randomBlueprint = Random.Range(0, stats.shopBlueprintsT5.Count);
                string selectedBlueprint = stats.shopBlueprintsT5[randomBlueprint];
                stats.availableShopBlueprints.Add(selectedBlueprint);
                stats.shopBlueprintsT5.Remove(selectedBlueprint);
            }
            else
            {
                break;
            }
        }
    }

    public void NewSave()
    {
        stats = new GameStats
        {
            unlockedWeaponIDs = new List<string> { "Rifle" },
            shopBlueprintsT1 = new List<string> { "Scattergun Blueprint", "Auto-Rifle Blueprint", "Ion Cannon Blueprint", "Emergency Teleport Blueprint", "Tractor Beam Blueprint" },
            shopBlueprintsT2 = new List<string> { "Ore Purifier Blueprint", "Repair Nanobots Blueprint", "Machinegun Blueprint", "Shotgun Blueprint", "Speed Booster Blueprint"/*, "Emergency Battery Blueprint"*/},
            shopBlueprintsT3 = new List<string> { "Shield Generator Blueprint", "Solar Collector Blueprint", "Defective Super Enhancer Blueprint", "Junk Blaster Blueprint", "Scrap Recycler Blueprint"/*, "Ion Pulse Emitter Blueprint"*/},
            shopBlueprintsT4 = new List<string> { "Weapon Overcharger Blueprint", "Laser Blueprint", "Missile Launcher Blueprint", "Ramming Armour Blueprint"/*, "Decoy Buoy Blueprint"*/ },
            shopBlueprintsT5 = new List<string> { "Super Enhancer Blueprint", "Rebounder Weapon Blueprint"/*, "Grenade Launcher Blueprint"*/}
        };
        ShopStock(3);
        MapManager.instance.planets[0] = new PlanetStats(1, 25, 0, 0, 0, 0, 0, 0, 0, "Teritory");
        MapManager.instance.RandomizePlanets();
        SaveData();
        MapManager.instance.SaveData();
        PlayerPrefs.SetInt("SaveExists", 0);
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
