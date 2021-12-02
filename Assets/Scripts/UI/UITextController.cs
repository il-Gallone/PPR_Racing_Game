using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{
    public string ID;
    public Text uiText;
    public ShopManager shopManager;


    private void Start()
    {
        if (ID == "allRepair")
        {
            MapManager.instance.repairButtons = gameObject;
            gameObject.SetActive(GameManager.instance.stats.repairState);
        }
        if (ID == "raid")
        {
            MapManager.instance.raidButton = gameObject;
            gameObject.SetActive(GameManager.instance.stats.raidState);
        }
        if (ID == "travel")
        {
            MapManager.instance.travelButton = gameObject;
            gameObject.SetActive(GameManager.instance.stats.travelState);
        }
        if (ID == "shop")
        {
            MapManager.instance.shopButton = gameObject;
            gameObject.SetActive(GameManager.instance.stats.shopState);
        }
    }

    public void OnClick()
    {
        if (ID == "raid")
        {
            MapManager.LoadSceneByName();
        }
        if (ID == "repair1")
        {
            MapManager.RepairPart(1);
        }
        if (ID == "repair10")
        {
            MapManager.RepairPart(10);
        }
        if (ID == "repairAll")
        {
            MapManager.RepairFull();
        }
        if (ID == "travel")
        {
            MapManager.TravelPlanets();
        }
        if(ID == "back")
        {
            MapManager.LoadSceneByIndex(0);
        }
        if (ID == "shop")
        {
            MapManager.OpenShop();
        }
    }

    public void Update()
    {
        if (ID == "repair1")
        {
            uiText.text = "Repair 1 HP: -" + MapManager.instance.planets[0].planetRepairCost.ToString() + " Scrap";
        }
        if (ID == "repair10")
        {
            uiText.text = "Repair 10 HP: -" + (10 * MapManager.instance.planets[0].planetRepairCost).ToString() + " Scrap";
        }
        if (ID == "repairAll")
        {
            uiText.text = "Repair All HP: -" + ((int)(GameManager.instance.stats.maxHealth * (1 + (GameManager.instance.stats.armourLevel * 0.1f))
            - GameManager.instance.stats.health) * MapManager.instance.planets[0].planetRepairCost).ToString() + " Scrap";
        }
        if (ID == "hp")
        {
            uiText.text = "HP: " + ((int)GameManager.instance.stats.health).ToString() + "/" + ((int)(GameManager.instance.stats.maxHealth * (1 + (GameManager.instance.stats.armourLevel * 0.1f)))).ToString();
        }
        if (ID == "scrap")
        {
            uiText.text = "Scrap: " + GameManager.instance.stats.scrap.ToString();
        }
        if (ID == "weapon")
        {
            uiText.text = "Weapon Parts: " + shopManager.weaponStock.ToString();
        }
        if (ID == "engine")
        {
            uiText.text = "Engine Parts: " + shopManager.engineStock.ToString();
        }
        if (ID == "armour")
        {
            uiText.text = "Armour Parts: " + shopManager.armourStock.ToString();
        }
        if (ID == "weaponBuy")
        {
            uiText.text = "Buy: -" + shopManager.weaponCost.ToString() + " Scrap";
        }
        if (ID == "engineBuy")
        {
            uiText.text = "Buy: -" + shopManager.engineCost.ToString() + " Scrap";
        }
        if (ID == "armourBuy")
        {
            uiText.text = "Buy: -" + shopManager.armourCost.ToString() + " Scrap";
        }
        if (ID == "blueprint1")
        {
            uiText.text = shopManager.blueprint1;
        }
        if (ID == "blueprint2")
        {
            uiText.text = shopManager.blueprint2;
        }
        if (ID == "blueprint3")
        {
            uiText.text = shopManager.blueprint3;
        }
        if (ID == "bp1Buy")
        {
            uiText.text = "Buy: -" + shopManager.bp1Cost.ToString() + " Scrap";
        }
        if (ID == "bp2Buy")
        {
            uiText.text = "Buy: -" + shopManager.bp2Cost.ToString() + " Scrap";
        }
        if (ID == "bp3Buy")
        {
            uiText.text = "Buy: -" + shopManager.bp3Cost.ToString() + " Scrap";
        }
    }
}
