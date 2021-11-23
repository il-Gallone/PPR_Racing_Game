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
        }
        if (ID == "raid")
        {
            MapManager.instance.raidButton = gameObject;
        }
        if (ID == "travel")
        {
            MapManager.instance.travelButton = gameObject;
        }
        if (ID == "shop")
        {
            MapManager.instance.shopButton = gameObject;
        }
        if (ID == "repair1")
        {
            buttonText.text = "Repair 1: " + MapManager.instance.planets[0].planetRepairCost.ToString();
        }
        if (ID == "repair10")
        {
            buttonText.text = "Repair 10: " + (10*MapManager.instance.planets[0].planetRepairCost).ToString();
        }
        if (ID == "repairAll")
        {
            buttonText.text = "Repair All: " + ((int)(GameManager.instance.stats.maxHealth * (1 + (GameManager.instance.stats.armourLevel * 0.1f))
            - GameManager.instance.stats.health) * MapManager.instance.planets[0].planetRepairCost).ToString();
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
    }
}
