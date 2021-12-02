using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static int weaponStock;
    public static int engineStock;
    public static int armourStock;
    public static int weaponCost;
    public static int engineCost;
    public static int armourCost;
    public static string blueprint1;
    public static string blueprint2;
    public static string blueprint3;
    public static int bp1Cost;
    public static int bp2Cost;
    public static int bp3Cost;

    // Start is called before the first frame update
    void Start()
    {
        weaponStock = Random.Range(3, 8);
        engineStock = Random.Range(3, 8);
        armourStock = Random.Range(3, 8);
        weaponCost = Random.Range(7, 17 - weaponStock);
        engineCost = Random.Range(7, 17 - engineStock);
        armourCost = Random.Range(7, 17 - armourStock);
        if (GameManager.instance.stats.availableShopBlueprints.Count > 0)
        {
            blueprint1 = GameManager.instance.stats.availableShopBlueprints[Random.Range(0, GameManager.instance.stats.availableShopBlueprints.Count)];
            GameManager.instance.stats.availableShopBlueprints.Remove(blueprint1);
        }
        else
            blueprint1 = "SOLD OUT!";
        if (GameManager.instance.stats.availableShopBlueprints.Count > 0)
        {
            blueprint2 = GameManager.instance.stats.availableShopBlueprints[Random.Range(0, GameManager.instance.stats.availableShopBlueprints.Count)];
            GameManager.instance.stats.availableShopBlueprints.Remove(blueprint2);
        }
        else
            blueprint2 = "SOLD OUT!";
        if (GameManager.instance.stats.availableShopBlueprints.Count > 0)
        {
            blueprint3 = GameManager.instance.stats.availableShopBlueprints[Random.Range(0, GameManager.instance.stats.availableShopBlueprints.Count)];
            GameManager.instance.stats.availableShopBlueprints.Remove(blueprint3);
        }
        else
            blueprint3 = "SOLD OUT!";
        bp1Cost = Random.Range(35, 65) + GameManager.instance.stats.planetsTraveled;
        bp2Cost = Random.Range(35, 65) + GameManager.instance.stats.planetsTraveled;
        bp3Cost = Random.Range(35, 65) + GameManager.instance.stats.planetsTraveled;
    }

    public void PurchaseStock(string stockID)
    {
        if (stockID == "weapon")
        {
            if(weaponStock > 0 && weaponCost <= GameManager.instance.stats.scrap)
            {
                weaponStock--;
                GameManager.instance.stats.scrap -= weaponCost;
            }
        }
        if (stockID == "engine")
        {
            if (engineStock > 0 && engineCost <= GameManager.instance.stats.scrap)
            {
                engineStock--;
                GameManager.instance.stats.scrap -= engineCost;
            }
        }
        if (stockID == "armour")
        {
            if (armourStock > 0 && armourCost <= GameManager.instance.stats.scrap)
            {
                armourStock--;
                GameManager.instance.stats.scrap -= armourCost;
            }
        }
        if (stockID == "blueprint1")
        {
            if (blueprint1 != "SOLD OUT!" && bp1Cost <= GameManager.instance.stats.scrap)
            {
                GameManager.instance.stats.scrap -= bp1Cost;
                GameManager.instance.stats.inventory.Add(blueprint1);
                blueprint1 = "SOLD OUT!";
            }
        }
        if (stockID == "blueprint2")
        {
            if (blueprint2 != "SOLD OUT!" && bp2Cost <= GameManager.instance.stats.scrap)
            {
                GameManager.instance.stats.scrap -= bp2Cost;
                GameManager.instance.stats.inventory.Add(blueprint2);
                blueprint2 = "SOLD OUT!";
            }
        }
        if (stockID == "blueprint3")
        {
            if (blueprint3 != "SOLD OUT!" && bp3Cost <= GameManager.instance.stats.scrap)
            {
                GameManager.instance.stats.scrap -= bp3Cost;
                GameManager.instance.stats.inventory.Add(blueprint3);
                blueprint3 = "SOLD OUT!";
            }
        }
    }

    public void ExitShopScene()
    {
        if (blueprint1 != "SOLD OUT!")
        {
            GameManager.instance.stats.availableShopBlueprints.Add(blueprint1);
        }
        if (blueprint2 != "SOLD OUT!")
        {
            GameManager.instance.stats.availableShopBlueprints.Add(blueprint2);
        }
        if (blueprint3 != "SOLD OUT!")
        {
            GameManager.instance.stats.availableShopBlueprints.Add(blueprint3);
        }
        GameManager.instance.SaveData();
        MapManager.LoadSceneByIndex(0);
    }
}
