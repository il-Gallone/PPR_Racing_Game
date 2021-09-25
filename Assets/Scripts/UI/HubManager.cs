using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public Text scrapTotal, weapon, engine, armour, module, moduleDescription;
    public Text scrapTotal2, weaponParts, engineParts, armourParts;

    public string[] weapons;
    public ModuleManager moduleManager;

    public int upgradeCost = 50, maxArmour = 4, maxEngine = 4;

    public GameObject[] UIarray;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHubStats();
    }

    public void DisableAllUI(GameObject UItoEnable)
    {
        for (int i = 0; i < UIarray.Length; i++)
        {
            if (UIarray[i].activeInHierarchy)
                UIarray[i].SetActive(false);
        }

        // then enable the selected UI
        UItoEnable.SetActive(true);
    }

    public void ToggleUI(GameObject UI)
    {
        if (UI.activeInHierarchy)
        {
            UI.SetActive(false);
        } else
        {
            UI.SetActive(true);
        }
    }

    public void UpgradeEngine()
    {
        if (GameManager.instance.stats.engineLevel < maxEngine)
        {
            // enough scrap and upgrade available?
            if (GameManager.instance.stats.scrap >= upgradeCost && GameManager.instance.stats.engineParts >= 10)
            {
                GameManager.instance.stats.engineLevel++;
                GameManager.instance.stats.scrap -= upgradeCost;
                GameManager.instance.stats.engineParts -= 10;
            }
        }

        // update text
        engine.text = "Engine: " + GameManager.instance.stats.engineLevel;
        scrapTotal.text = "Scrap: " + GameManager.instance.stats.scrap;
    }

    public void UpgradeArmour()
    {
        if (GameManager.instance.stats.armourLevel < maxArmour)
        {
            // enough scrap and upgrade available?
            if (GameManager.instance.stats.scrap >= upgradeCost && GameManager.instance.stats.armourParts >= 10)
            {
                GameManager.instance.stats.armourLevel++;
                GameManager.instance.stats.scrap -= upgradeCost;
                GameManager.instance.stats.armourParts -= 10;
            }
        }

        // update text
        armour.text = "Armour: " + GameManager.instance.stats.armourLevel;
        scrapTotal.text = "Scrap: " + GameManager.instance.stats.scrap;
    }

    public void UpdateHubStats()
    {
        scrapTotal.text = "Scrap: " + GameManager.instance.stats.scrap;
        weapon.text = "Weapon: " + GameManager.instance.stats.currentWeaponID;
        module.text = "Module: " + GameManager.instance.stats.currentModuleID;
        moduleDescription.text = moduleManager.FindDescription(GameManager.instance.stats.currentModuleID);
        engine.text = "Engine: " + GameManager.instance.stats.engineLevel;
        armour.text = "Armour: " + GameManager.instance.stats.armourLevel;
    }

    public void UpdateInventory()
    {
        scrapTotal2.text = "Scrap: " + GameManager.instance.stats.scrap;
        weaponParts.text = "Weapon Parts: " + GameManager.instance.stats.weaponParts;
        engineParts.text = "Engine Parts: " + GameManager.instance.stats.engineParts;
        armourParts.text = "Armour Parts: " + GameManager.instance.stats.armourParts;
    }


    public void WeaponLeft()
    {
        if (GameManager.instance.stats.unlockedWeaponIDs.Count > 1)
        {
            if (GameManager.instance.stats.currentWeaponIDNumber > 0)
            {
                GameManager.instance.stats.currentWeaponIDNumber--;
            }
            else
            {
                GameManager.instance.stats.currentWeaponIDNumber = GameManager.instance.stats.unlockedWeaponIDs.Count - 1;
            }
            GameManager.instance.stats.currentWeaponID = GameManager.instance.stats.unlockedWeaponIDs[GameManager.instance.stats.currentWeaponIDNumber];
        }
        // update text
        weapon.text = "Weapon: " + GameManager.instance.stats.currentWeaponID;
    }

    public void WeaponRight()
    {
        if (GameManager.instance.stats.unlockedWeaponIDs.Count > 1)
        {
            if (GameManager.instance.stats.currentWeaponIDNumber < GameManager.instance.stats.unlockedWeaponIDs.Count - 1)
            {
                GameManager.instance.stats.currentWeaponIDNumber++;
            }
            else
            {
                GameManager.instance.stats.currentWeaponIDNumber = 0;
            }
            GameManager.instance.stats.currentWeaponID = GameManager.instance.stats.unlockedWeaponIDs[GameManager.instance.stats.currentWeaponIDNumber];
        }
        // update text
        weapon.text = "Weapon: " + GameManager.instance.stats.currentWeaponID;
    }
    public void ModuleLeft()
    {
        if (GameManager.instance.stats.unlockedModuleIDs.Count > 1 && GameManager.instance.stats.currentModuleIDNumber != -1)
        {
            if (GameManager.instance.stats.currentModuleIDNumber > 0)
            {
                GameManager.instance.stats.currentModuleIDNumber--;
            }
            else
            {
                GameManager.instance.stats.currentModuleIDNumber = GameManager.instance.stats.unlockedModuleIDs.Count - 1;
            }
            GameManager.instance.stats.currentModuleID = GameManager.instance.stats.unlockedModuleIDs[GameManager.instance.stats.currentModuleIDNumber];
        }
        // update text
        module.text = "Module: " + GameManager.instance.stats.currentModuleID;
    }

    public void ModuleRight()
    {
        if (GameManager.instance.stats.unlockedModuleIDs.Count > 1 && GameManager.instance.stats.currentModuleIDNumber != -1)
        {
            if (GameManager.instance.stats.currentModuleIDNumber < GameManager.instance.stats.unlockedModuleIDs.Count - 1)
            {
                GameManager.instance.stats.currentModuleIDNumber++;
            }
            else
            {
                GameManager.instance.stats.currentModuleIDNumber = 0;
            }
            GameManager.instance.stats.currentModuleID = GameManager.instance.stats.unlockedModuleIDs[GameManager.instance.stats.currentModuleIDNumber];
        }
        // update text
        module.text = "Module: " + GameManager.instance.stats.currentModuleID;
    }
}
