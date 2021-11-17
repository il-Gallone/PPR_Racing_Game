using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] inventoryButtons;
    public GameObject inventoryPopUp;
    public Button popUpButton;
    public Text popUpTitle;
    public Text popUpDescription;
    public int activePopUp = 0;

    public Sprite blueprintIcon;

    //UI fade parameters
    public float fadeSpeed = 10f, popupMoveSpeed = 5f;
    bool fadeIn = false;
    CanvasGroup canvasGroup;

    public HubManager hubManager;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = inventoryPopUp.GetComponent<CanvasGroup>();

        for(int i = 0; i<GameManager.instance.stats.inventory.Count; i++)
        {
            if(i < inventoryButtons.Length)
            {
                inventoryButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }


    private void Update()
    {
        // the following code fades in/out UI
        if (fadeIn)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;

            inventoryPopUp.transform.position = Vector3.Lerp(inventoryPopUp.transform.position, inventoryButtons[activePopUp].transform.position, popupMoveSpeed * Time.deltaTime);
                //Vector3.MoveTowards(inventoryPopUp.transform.position, inventoryButtons[activePopUp].transform.position, popupMoveSpeed * Time.deltaTime);
        }
        else if (!fadeIn)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            if (canvasGroup.alpha==0)
            {
                inventoryPopUp.SetActive(false);
            }
        }
    }

    public void GeneratePopUp(int inventoryID)
    {
        if(!fadeIn || activePopUp != inventoryID)
        {
            fadeIn = true;
            inventoryPopUp.SetActive(true);
            activePopUp = inventoryID;
            //inventoryPopUp.transform.position = inventoryButtons[inventoryID].transform.position;
            popUpTitle.text = GameManager.instance.stats.inventory[inventoryID];
            popUpDescription.text = GetPopUpDescription(GameManager.instance.stats.inventory[inventoryID]);
            popUpButton.gameObject.SetActive(IsItemBlueprint(GameManager.instance.stats.inventory[inventoryID]));
        }
        else if(inventoryPopUp.activeInHierarchy && activePopUp == inventoryID)
        {
            //inventoryPopUp.SetActive(false);
            fadeIn = false;
        }
    }

    string GetPopUpDescription(string itemName)
    {
        string text = "Description not Loaded";
        //for now hard coded, TODO load descriptions from file
        if(itemName == "Shotgun Blueprint")
        {
            text = "A Weapon that Sprays Multiple Bullets \nScrap Cost: 100 \nWeapon Part Cost: 10";
        }
        if (itemName == "Machinegun Blueprint")
        {
            text = "A Weapon that has a high firing rate \nScrap Cost: 100 \nWeapon Part Cost: 10";
        }
        if (itemName == "Scattergun Blueprint")
        {
            text = "A Weapon that fires a few extra bullets \nScrap Cost: 50 \nWeapon Part Cost: 5";
        }
        if (itemName == "Auto-Rifle Blueprint")
        {
            text = "An upgraded Rifle that fires slightly faster \nScrap Cost: 50 \nWeapon Part Cost: 5";
        }
        if (itemName == "Missile Launcher Blueprint")
        {
            text = "Launches homing missiles \nScrap Cost: 150 \nWeapon Part Cost: 15";
        }
        if (itemName == "Laser Blueprint")
        {
            text = "Shoots a continuous beam \nScrap Cost: 150 \nWeapon Part Cost: 15";
        }
        if (itemName == "Junk Blaster Blueprint")
        {
            text = "Shoots random junk \nScrap Cost: 150 \nWeapon Part Cost: 15";
        }
        if (itemName == "Ion Cannon Blueprint")
        {
            text = "Disables enemy ships temporarily \nScrap Cost: 150 \nWeapon Part Cost: 15";
        }
        if (itemName == "Rebounder Weapon Blueprint")
        {
            text = "Deflects enemy projectiles \nScrap Cost: 300 \nWeapon Part Cost: 20";
        }
        if (itemName == "Shield Generator Blueprint")
        {
            text = "A module that allows for an auto regenerating shield that will take the hit from most damage types \nScrap Cost: 75 \nArmour Part Cost: 7";
        }
        if (itemName == "Speed Booster Blueprint")
        {
            text = "A module that can temporarily increase Maximum Speed and Acceleration Rate before going on cooldown \nScrap Cost: 75 \nEngine Part Cost: 7";
        }
        if (itemName == "Repair Nanobots Blueprint")
        {
            text = "A module that repairs your ship between levels. \nScrap Cost: 75 \nArmour Part Cost: 7";
        }
        if (itemName == "Emergency Teleport Blueprint")
        {
            text = "A module that teleports you to a random nearby location. \nScrap Cost: 75 \nEngine Part Cost: 7";
        }
        if (itemName == "Solar Collector Blueprint")
        {
            text = "A module that recharges energy while you are not moving. \nScrap Cost: 75 \nEngine Part Cost: 7";
        }
        if (itemName == "Ramming Armour Blueprint")
        {
            text = "A module that protects your ship upon impacts and deals damage to enemies+objects \nScrap Cost: 75 \nArmour Part Cost: 7";
        }
        if (itemName == "Scrap Recycler Blueprint")
        {
            text = "A module that recycles 50% of scrap collected. For every 100 recycled scrap gain a random part \nScrap Cost: 75 \nEngine Part Cost: 7";
        }
        if (itemName == "Ore Purifier Blueprint")
        {
            text = "Asteroid Drops are 50% more effective. \nScrap Cost: 75 \nEngine Part Cost: 7";
        }
        if (itemName == "Secure Storage Blueprint")
        {
            text = "Retain some of your scrap and parts if defeated. \nScrap Cost: 75 \nArmour Part Cost: 7";
        }
        if (itemName == "Ore Refiner Blueprint")
        {
            text = "Refine 50% of asteroid drops. Gain a small amount of scrap from refined drops. \nScrap Cost: 75 \nEngine Part Cost: 7";
        }
        if (itemName == "Defective Super Enhancer Blueprint")
        {
            text = "All Ship Upgrades are boosted by two levels. Controls are reversed. \nScrap Cost: 75 \nEngine Part Cost: 7 \nArmour Part Cost: 7";
        }
        if (itemName == "Super Enhancer Blueprint")
        {
            text = "All Ship Upgrades are boosted by two levels. \nScrap Cost: 75 \nEngine Part Cost: 7 \nArmour Part Cost: 7";
        }
        if (itemName == "Tractor Beam Blueprint")
        {
            text = "Collectables are pulled in when nearby. \nScrap Cost: 75 \nEngine Part Cost: 7";
        }
        if (itemName == "Weapon Overcharger Blueprint")
        {
            text = "Temporarily increase weapon fire-rate and allow automatic fire. \nScrap Cost: 75 \nWeapon Part Cost: 7";
        }
        return text;
    }

    bool IsItemBlueprint(string itemName)
    {
        //for now hard coded, TODO determine blueprint from file
        if(itemName == "Shotgun Blueprint" || itemName == "Machinegun Blueprint" || itemName == "Scattergun Blueprint" || itemName == "Auto-Rifle Blueprint"
            || itemName == "Missile Launcher Blueprint" || itemName == "Laser Blueprint" || itemName == "Junk Blaster Blueprint" || itemName == "Ion Cannon Blueprint" 
            || itemName == "Shield Generator Blueprint" || itemName == "Speed Booster Blueprint" || itemName == "Rebounder Weapon Blueprint" 
            || itemName == "Repair Nanobots Blueprint" || itemName == "Emergency Teleport Blueprint" || itemName == "Solar Collector Blueprint"
            || itemName == "Ramming Armour Blueprint" || itemName == "Scrap Recycler Blueprint" || itemName == "Ore Purifier Blueprint"
            || itemName == "Secure Storage Blueprint" || itemName == "Ore Refiner Blueprint" || itemName == "Defective Super Enhancer Blueprint"
            || itemName == "Super Enhancer Blueprint" || itemName == "Tractor Beam Blueprint" || itemName == "Weapon Overcharger Blueprint")
        {
            return true;
        }
        return false;
    }

    public void FabriacteBlueprint()
    {
        //for now hard coded, TODO determine cost of blueprint from file
        if (GameManager.instance.stats.inventory[activePopUp] == "Rebounder Weapon Blueprint")
        {
            if (GameManager.instance.stats.scrap >= 300 && GameManager.instance.stats.weaponParts >= 20)
            {
                GameManager.instance.stats.scrap -= 300;
                GameManager.instance.stats.weaponParts -= 20;
                hubManager.UpdateInventory();
                //inventoryPopUp.SetActive(false);
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Rebounder Weapon Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Rebounder");
                    GameManager.instance.stats.inventory.Remove("Rebounder Weapon Blueprint");
                }
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
        else if (GameManager.instance.stats.inventory[activePopUp] == "Missile Launcher Blueprint"
             || GameManager.instance.stats.inventory[activePopUp] == "Laser Blueprint"
              || GameManager.instance.stats.inventory[activePopUp] == "Junk Blaster Blueprint"
               || GameManager.instance.stats.inventory[activePopUp] == "Ion Cannon Blueprint")
        {
            if (GameManager.instance.stats.scrap >= 150 && GameManager.instance.stats.weaponParts >= 15)
            {
                GameManager.instance.stats.scrap -= 150;
                GameManager.instance.stats.weaponParts -= 15;
                hubManager.UpdateInventory();
                //inventoryPopUp.SetActive(false);
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Missile Launcher Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Missile Launcher");
                    GameManager.instance.stats.inventory.Remove("Missile Launcher Blueprint");
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Laser Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Laser");
                    GameManager.instance.stats.inventory.Remove("Laser Blueprint");
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Junk Blaster Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Junk Blaster");
                    GameManager.instance.stats.inventory.Remove("Junk Blaster Blueprint");
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Ion Cannon Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Ion Cannon");
                    GameManager.instance.stats.inventory.Remove("Ion Cannon Blueprint");
                }
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
            
        }

        else if (GameManager.instance.stats.inventory[activePopUp] == "Shotgun Blueprint" || GameManager.instance.stats.inventory[activePopUp] == "Machinegun Blueprint")
        {
            if (GameManager.instance.stats.scrap >= 100 && GameManager.instance.stats.weaponParts >= 10)
            {
                GameManager.instance.stats.scrap -= 100;
                GameManager.instance.stats.weaponParts -= 10;
                hubManager.UpdateInventory();
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Shotgun Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Shotgun");
                    GameManager.instance.stats.inventory.Remove("Shotgun Blueprint");
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Machinegun Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Machinegun");
                    GameManager.instance.stats.inventory.Remove("Machinegun Blueprint");
                }
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
            
        }
        
        else if (GameManager.instance.stats.inventory[activePopUp] == "Weapon Overcharger Blueprint")
        {
            if (GameManager.instance.stats.scrap >= 75 && GameManager.instance.stats.weaponParts >= 7)
            {
                GameManager.instance.stats.scrap -= 75;
                GameManager.instance.stats.weaponParts -= 7;
                hubManager.UpdateInventory();
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Weapon Overcharger Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Weapon Overcharger");
                    GameManager.instance.stats.inventory.Remove("Weapon Overcharger Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Weapon Overcharger";
                    }
                }
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }

        }
        else if (GameManager.instance.stats.inventory[activePopUp] == "Scattergun Blueprint" || GameManager.instance.stats.inventory[activePopUp] == "Auto-Rifle Blueprint")
        {
            if (GameManager.instance.stats.scrap >= 50 && GameManager.instance.stats.weaponParts >= 5)
            {
                GameManager.instance.stats.scrap -= 50;
                GameManager.instance.stats.weaponParts -= 5;
                hubManager.UpdateInventory();
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Scattergun Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Scattergun");
                    GameManager.instance.stats.inventory.Remove("Scattergun Blueprint");
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Auto-Rifle Blueprint")
                {
                    GameManager.instance.stats.unlockedWeaponIDs.Add("Auto-Rifle");
                    GameManager.instance.stats.inventory.Remove("Auto-Rifle Blueprint");
                }
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
            
        }
        else if (GameManager.instance.stats.inventory[activePopUp] == "Shield Generator Blueprint" || GameManager.instance.stats.inventory[activePopUp] == "Repair Nanobots Blueprint"
            || GameManager.instance.stats.inventory[activePopUp] == "Ramming Armour Blueprint" || GameManager.instance.stats.inventory[activePopUp] == "Secure Storage Blueprint"
            )
        {
            if (GameManager.instance.stats.scrap >= 75 && GameManager.instance.stats.armourParts >= 7)
            {
                GameManager.instance.stats.scrap -= 75;
                GameManager.instance.stats.armourParts -= 7;
                hubManager.UpdateInventory();
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Shield Generator Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Shield Generator");
                    GameManager.instance.stats.inventory.Remove("Shield Generator Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Shield Generator";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Repair Nanobots Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Repair Nanobots");
                    GameManager.instance.stats.inventory.Remove("Repair Nanobots Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Repair Nanobots";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Ramming Armour Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Ramming Armour");
                    GameManager.instance.stats.inventory.Remove("Ramming Armour Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Ramming Armour";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Secure Storage Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Secure Storage");
                    GameManager.instance.stats.inventory.Remove("Secure Storage Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Secure Storage";
                    }
                }
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
            
        }
        else if (GameManager.instance.stats.inventory[activePopUp] == "Speed Booster Blueprint" || GameManager.instance.stats.inventory[activePopUp] == "Emergency Teleport Blueprint"
            || GameManager.instance.stats.inventory[activePopUp] == "Solar Collector Blueprint" || GameManager.instance.stats.inventory[activePopUp] == "Scrap Recycler Blueprint"
            || GameManager.instance.stats.inventory[activePopUp] == "Ore Purifier Blueprint" || GameManager.instance.stats.inventory[activePopUp] == "Ore Refiner Blueprint"
            || GameManager.instance.stats.inventory[activePopUp] == "Tractor Beam Blueprint")
        {
            if (GameManager.instance.stats.scrap >= 75 && GameManager.instance.stats.engineParts >= 7)
            {
                GameManager.instance.stats.scrap -= 75;
                GameManager.instance.stats.engineParts -= 7;
                hubManager.UpdateInventory();
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Speed Booster Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Speed Booster");
                    GameManager.instance.stats.inventory.Remove("Speed Booster Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Speed Booster";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Emergency Teleport Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Emergency Teleport");
                    GameManager.instance.stats.inventory.Remove("Emergency Teleport Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Emergency Teleport";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Solar Collector Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Solar Collector");
                    GameManager.instance.stats.inventory.Remove("Solar Collector Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Solar Collector";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Scrap Recycler Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Scrap Recycler");
                    GameManager.instance.stats.inventory.Remove("Scrap Recycler Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Scrap Recycler";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Ore Purifier Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Ore Purifier");
                    GameManager.instance.stats.inventory.Remove("Ore Purifier Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Ore Purifier";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Ore Refiner Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Ore Refiner");
                    GameManager.instance.stats.inventory.Remove("Ore Refiner Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Ore Refiner";
                    }
                }
                else if (GameManager.instance.stats.inventory[activePopUp] == "Tractor Beam Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Tractor Beam");
                    GameManager.instance.stats.inventory.Remove("Tractor Beam Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Tractor Beam";
                    }
                }
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
            
        }
        else if (GameManager.instance.stats.inventory[activePopUp] == "Defective Super Enhancer Blueprint"
                || GameManager.instance.stats.inventory[activePopUp] == "Super Enhancer Blueprint")
        {
            if (GameManager.instance.stats.scrap >= 75 && GameManager.instance.stats.engineParts >= 7 && GameManager.instance.stats.armourParts >= 7)
            {
                GameManager.instance.stats.scrap -= 75;
                GameManager.instance.stats.engineParts -= 7;
                hubManager.UpdateInventory();
                fadeIn = false;
                if (GameManager.instance.stats.inventory[activePopUp] == "Defective Super Enhancer Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Defective Super Enhancer");
                    GameManager.instance.stats.inventory.Remove("Defective Super Enhancer Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Defective Super Enhancer";
                    }
                } 
                else if (GameManager.instance.stats.inventory[activePopUp] == "Super Enhancer Blueprint")
                {
                    GameManager.instance.stats.unlockedModuleIDs.Add("Super Enhancer");
                    GameManager.instance.stats.inventory.Remove("Super Enhancer Blueprint");
                    if (GameManager.instance.stats.currentModuleIDNumber == -1)
                    {
                        GameManager.instance.stats.currentModuleIDNumber = 0;
                        GameManager.instance.stats.currentModuleID = "Super Enhancer";
                    }
                }

                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
                {
                    if (i < inventoryButtons.Length)
                    {
                        inventoryButtons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
    }
}
