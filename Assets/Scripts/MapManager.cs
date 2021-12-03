using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public static string sceneToLoad;
    public static int targetedPlanet = 0;
    public static int targetedFaction;
    public PlanetStats[] planets = new PlanetStats[5];
    public Map_PlanetController[] planetButtons = new Map_PlanetController[5];
    public GameObject travelButton;
    public GameObject raidButton;
    public GameObject repairButtons;
    public GameObject shopButton;
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
    }

    public static void TravelPlanets()
    {
        if (targetedPlanet != 0)
        {
            instance.planets[0] = instance.planets[targetedPlanet];
            instance.planetButtons[0].PlanetUpdate();
            instance.travelButton.SetActive(false);
            GameManager.instance.stats.travelState = false;
            GameManager.instance.stats.planetsTraveled++;
            GameManager.instance.ShopStock(1);
            targetedPlanet = 0;
            instance.RandomizePlanets();
            for (int i = 1; i < instance.planetButtons.Length; i++)
            {
                instance.planetButtons[i].PlanetUpdate();
            }
            if (instance.planets[0].planetFaction == 6)
            {
                instance.shopButton.SetActive(true);
                GameManager.instance.stats.shopState = true;
            }
            if (instance.planets[0].planetFaction <= 5)
            {
                instance.raidButton.SetActive(true);
                GameManager.instance.stats.raidState = true;
            }
            if (instance.planets[0].planetFaction == 1)
            {
                if (GameManager.instance.stats.faction1Favour > 15)
                {
                    instance.repairButtons.SetActive(true);
                    GameManager.instance.stats.repairState = true;
                }
            }
            else if (instance.planets[0].planetFaction == 2)
            {
                if (GameManager.instance.stats.faction2Favour > 15)
                {
                    instance.repairButtons.SetActive(true);
                    GameManager.instance.stats.repairState = true;
                }
            }
            else if (instance.planets[0].planetFaction == 3)
            {
                if (GameManager.instance.stats.faction3Favour > 15)
                {
                    instance.repairButtons.SetActive(true);
                    GameManager.instance.stats.repairState = true;
                }
            }
            else if (instance.planets[0].planetFaction == 4)
            {
                if (GameManager.instance.stats.faction4Favour > 15)
                {
                    instance.repairButtons.SetActive(true);
                    GameManager.instance.stats.repairState = true;
                }
            }
            else if (instance.planets[0].planetFaction == 5)
            {
                if (GameManager.instance.stats.faction5Favour > 15)
                {
                    instance.repairButtons.SetActive(true);
                    GameManager.instance.stats.repairState = true;
                }
            }
            GameManager.instance.SaveData();
            instance.SaveData();
        }
    }

    public void RandomizePlanets()
    {
        for (int i = 1; i < planets.Length; i++)
        {
            int diff = 1;
            int limits = 25;
            int faction = Random.Range(1, 7); //1 = Alpha, 2 = Beta, 3 = Gamma, 4 = Omega, 5 = Epsilon, 6 = Merchant
            int weaponWeight;
            int engineWeight;
            int armourWeight;
            if (faction == 1)
            {
                engineWeight = 20;
                weaponWeight = -10;
                armourWeight = -10;
            }
            else if (faction == 2)
            {
                engineWeight = -10;
                weaponWeight = -10;
                armourWeight = 20;
            }
            else if (faction == 3)
            {
                engineWeight = -15;
                weaponWeight = -15;
                armourWeight = -15;
            }
            else if (faction == 4)
            {
                engineWeight = -10;
                weaponWeight = 20;
                armourWeight = -10;
            }
            else
            {
                engineWeight = 0;
                weaponWeight = 0;
                armourWeight = 0;
            }
            int weaponPartChance = Random.Range(20 + weaponWeight, 31 + weaponWeight);
            int enginePartChance = Random.Range(20 + engineWeight, 31 + engineWeight);
            int armourPartChance = Random.Range(20 + armourWeight, 31 + armourWeight);
            int seed = faction - 1; //0 = Alpha, 1 = Beta, 2 = Gamma, 3 = Omega, 4 = Epsilon
            int repairWeight = 0;
            bool repairFree = false;
            if (faction == 1)
            {
                if (GameManager.instance.stats.faction1Favour <= 35)
                {
                    repairWeight = 2;
                }
                if (GameManager.instance.stats.faction1Favour >= 65)
                {
                    repairWeight = -2;
                }
                if (GameManager.instance.stats.faction1Favour >= 90)
                {
                    repairFree = true;
                }
            }
            else if (faction == 2)
            {
                if (GameManager.instance.stats.faction2Favour <= 35)
                {
                    repairWeight = 2;
                }
                if (GameManager.instance.stats.faction2Favour >= 65)
                {
                    repairWeight = -2;
                }
                if (GameManager.instance.stats.faction2Favour >= 90)
                {
                    repairFree = true;
                }
            }
            else if (faction == 3)
            {
                if (GameManager.instance.stats.faction3Favour <= 35)
                {
                    repairWeight = 2;
                }
                if (GameManager.instance.stats.faction3Favour >= 65)
                {
                    repairWeight = -2;
                }
                if (GameManager.instance.stats.faction3Favour >= 90)
                {
                    repairFree = true;
                }
            }
            else if (faction == 4)
            {
                if (GameManager.instance.stats.faction4Favour <= 35)
                {
                    repairWeight = 2;
                }
                if (GameManager.instance.stats.faction4Favour >= 65)
                {
                    repairWeight = -2;
                }
                if (GameManager.instance.stats.faction4Favour >= 90)
                {
                    repairFree = true;
                }
            }
            else if (faction == 5)
            {
                if (GameManager.instance.stats.faction5Favour <= 35)
                {
                    repairWeight = 2;
                }
                if (GameManager.instance.stats.faction5Favour >= 65)
                {
                    repairWeight = -2;
                }
                if (GameManager.instance.stats.faction5Favour >= 90)
                {
                    repairFree = true;
                }
            }
            int repair = Random.Range(3 + repairWeight, 8 + repairWeight);
            if (repairFree)
            {
                repair = 0;
            }
            int enemy; //0 = Swarmer, 1 = Detonator, 2 = Collector, 3 = CollectorPacifist
            if (seed <= 1)
            {
                enemy = 3;
            }
            else if (seed == 3)
            {
                enemy = 1;
            }
            else if (seed == 4)
            {
                enemy = 2;
            }
            else
            {
                enemy = 0;
            }
            string name = "Outpost"; //TODO randomize name
            PlanetStats stats = new PlanetStats(diff, limits, weaponPartChance, enginePartChance, armourPartChance, faction, seed, enemy, repair, name);
            planets[i] = stats;
        }
    }

    public static void RepairPart(int repairAmount)
    {
        if (GameManager.instance.stats.health <= GameManager.instance.stats.maxHealth * (1 + (GameManager.instance.stats.armourLevel * 0.1f))
            - repairAmount)
        {
            if (GameManager.instance.stats.scrap >= instance.planets[0].planetRepairCost * repairAmount)
            {
                GameManager.instance.stats.scrap -= instance.planets[0].planetRepairCost * repairAmount;
                GameManager.instance.stats.health += repairAmount;
                instance.raidButton.SetActive(false);
                GameManager.instance.stats.raidState = false;
                instance.travelButton.SetActive(true);
                GameManager.instance.stats.travelState = true;
            }
        }
    }

    public static void RepairFull()
    {
        float repairAmount = GameManager.instance.stats.maxHealth * (1 + (GameManager.instance.stats.armourLevel * 0.1f))
            - GameManager.instance.stats.health;
        if (GameManager.instance.stats.scrap >= instance.planets[0].planetRepairCost * (int)repairAmount)
        {
            GameManager.instance.stats.scrap -= instance.planets[0].planetRepairCost * (int)repairAmount;
            GameManager.instance.stats.health += repairAmount;
            instance.raidButton.SetActive(false);
            GameManager.instance.stats.raidState = false;
            instance.travelButton.SetActive(true);
            GameManager.instance.stats.travelState = true;
        }
    }

    public static void LoadSceneByName()
    {
        instance.travelButton.SetActive(true);
        GameManager.instance.stats.travelState = true;
        instance.shopButton.SetActive(false);
        GameManager.instance.stats.shopState = false;
        instance.raidButton.SetActive(false);
        GameManager.instance.stats.raidState = false;
        instance.repairButtons.SetActive(false);
        GameManager.instance.stats.repairState = false;
        instance.planetButtons[0].ChangeScene();
        switch (targetedFaction)
        {
            case 1:
                {
                    GameManager.instance.stats.faction1Favour -= 5;
                    GameManager.instance.stats.faction2Favour -= 2;
                    GameManager.instance.stats.faction3Favour += 3;
                    GameManager.instance.stats.faction4Favour += 1;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 2:
                {
                    GameManager.instance.stats.faction1Favour -= 2;
                    GameManager.instance.stats.faction2Favour -= 5;
                    GameManager.instance.stats.faction3Favour += 1;
                    GameManager.instance.stats.faction4Favour += 3;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 3:
                {
                    GameManager.instance.stats.faction1Favour += 3;
                    GameManager.instance.stats.faction2Favour += 1;
                    GameManager.instance.stats.faction3Favour -= 5;
                    GameManager.instance.stats.faction4Favour -= 2;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 4:
                {
                    GameManager.instance.stats.faction1Favour += 1;
                    GameManager.instance.stats.faction2Favour += 3;
                    GameManager.instance.stats.faction3Favour -= 2;
                    GameManager.instance.stats.faction4Favour -= 5;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 5:
                {
                    GameManager.instance.stats.faction1Favour += 1;
                    GameManager.instance.stats.faction2Favour += 1;
                    GameManager.instance.stats.faction3Favour += 1;
                    GameManager.instance.stats.faction4Favour += 1;
                    GameManager.instance.stats.faction5Favour -= 5;
                    break;
                }
        }
        SceneManager.LoadScene(2);
    }

    public static void OpenShop()
    {
        instance.travelButton.SetActive(true);
        GameManager.instance.stats.travelState = true;
        instance.shopButton.SetActive(false);
        GameManager.instance.stats.shopState = false;
        instance.raidButton.SetActive(false);
        GameManager.instance.stats.raidState = false;
        instance.repairButtons.SetActive(false);
        GameManager.instance.stats.repairState = false;
        LoadSceneByIndex(4);
    }

    public static void LoadSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void SaveData()
    {
        string dataPath = Path.Combine(Application.dataPath, "planetData.txt");
        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            string jsonString;
            jsonString = JsonConvert.SerializeObject(planets);
            streamWriter.Write(jsonString);
        }
    }

    public void LoadData()
    {
        string dataPath = Path.Combine(Application.dataPath, "planetData.txt");
        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            string jsonString;
            jsonString = streamReader.ReadToEnd();
            planets = JsonConvert.DeserializeObject<PlanetStats[]>(jsonString);
        }
    }
}
