using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{
    public string ID;
    public Text uiText;


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
            uiText.text = "Weapon Parts: " + ShopManager.weaponStock.ToString();
        }
        if (ID == "engine")
        {
            uiText.text = "Engine Parts: " + ShopManager.engineStock.ToString();
        }
        if (ID == "armour")
        {
            uiText.text = "Armour Parts: " + ShopManager.armourStock.ToString();
        }
        if (ID == "weaponBuy")
        {
            uiText.text = "Buy: -" + ShopManager.weaponCost.ToString() + " Scrap";
        }
        if (ID == "engineBuy")
        {
            uiText.text = "Buy: -" + ShopManager.engineCost.ToString() + " Scrap";
        }
        if (ID == "armourBuy")
        {
            uiText.text = "Buy: -" + ShopManager.armourCost.ToString() + " Scrap";
        }
        if (ID == "blueprint1")
        {
            uiText.text = ShopManager.blueprint1;
        }
        if (ID == "blueprint2")
        {
            uiText.text = ShopManager.blueprint2;
        }
        if (ID == "blueprint3")
        {
            uiText.text = ShopManager.blueprint3;
        }
        if (ID == "bp1Buy")
        {
            uiText.text = "Buy: -" + ShopManager.bp1Cost.ToString() + " Scrap";
        }
        if (ID == "bp2Buy")
        {
            uiText.text = "Buy: -" + ShopManager.bp2Cost.ToString() + " Scrap";
        }
        if (ID == "bp3Buy")
        {
            uiText.text = "Buy: -" + ShopManager.bp3Cost.ToString() + " Scrap";
        }
    }
}
