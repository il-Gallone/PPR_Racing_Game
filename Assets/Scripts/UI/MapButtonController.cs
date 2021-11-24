using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonController : MonoBehaviour
{
    public string ID;
    public Text buttonText;


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
    }

    public void Update()
    {
        if (ID == "repair1")
        {
            buttonText.text = "Repair 1 HP: -" + MapManager.instance.planets[0].planetRepairCost.ToString() + " Scrap";
        }
        if (ID == "repair10")
        {
            buttonText.text = "Repair 10 HP: -" + (10 * MapManager.instance.planets[0].planetRepairCost).ToString() + " Scrap";
        }
        if (ID == "repairAll")
        {
            buttonText.text = "Repair All HP: -" + ((int)(GameManager.instance.stats.maxHealth * (1 + (GameManager.instance.stats.armourLevel * 0.1f))
            - GameManager.instance.stats.health) * MapManager.instance.planets[0].planetRepairCost).ToString() + " Scrap";
        }
        if (ID == "hp")
        {
            buttonText.text = "HP: " + ((int)GameManager.instance.stats.health).ToString() + "/" + ((int)(GameManager.instance.stats.maxHealth * (1 + (GameManager.instance.stats.armourLevel * 0.1f)))).ToString();
        }
        if (ID == "scrap")
        {
            buttonText.text = "Scrap: " + GameManager.instance.stats.scrap.ToString();
        }
    }
}
