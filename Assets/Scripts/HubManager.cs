using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public Text scrapTotal, weapon, engine, armour;

    public string[] weapons;

    public int upgradeCost = 50, maxArmour = 4, maxEngine = 4;

    //public GameObject shipStatusUI;

    // Start is called before the first frame update
    void Start()
    {
        //for testing
        PlayerPrefs.SetInt("PlayerScrapTotal", 500);
        PlayerPrefs.SetInt("PlayerWeaponUpgrades", 500);
        PlayerPrefs.SetInt("PlayerEngineUpgrades", 500);
        PlayerPrefs.SetInt("PlayerArmourUpgrades", 500);

        PlayerPrefs.SetInt("PlayerTotalWeapons", 0);
        PlayerPrefs.SetInt("PlayerEngineLevel", 0);
        PlayerPrefs.SetInt("PlayerArmourLevel", 0);

        UpdateHubStats();
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
        if (PlayerPrefs.GetInt("PlayerEngineLevel") < maxEngine)
        {
            // enough scrap and upgrade available?
            if (PlayerPrefs.GetInt("PlayerScrapTotal") >= upgradeCost && PlayerPrefs.GetInt("PlayerEngineUpgrades") >= 1)
            {
                PlayerPrefs.SetInt("PlayerEngineLevel", PlayerPrefs.GetInt("PlayerEngineLevel") + 1);
                PlayerPrefs.SetInt("PlayerScrapTotal", PlayerPrefs.GetInt("PlayerScrapTotal") - upgradeCost);
            }
        }

        // update text
        engine.text = "Engine: " + PlayerPrefs.GetInt("PlayerEngineLevel");
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
    }

    public void UpgradeArmour()
    {
        if (PlayerPrefs.GetInt("PlayerArmourLevel") < maxEngine)
        {
            // enough scrap and upgrade available?
            if (PlayerPrefs.GetInt("PlayerScrapTotal") >= upgradeCost && PlayerPrefs.GetInt("PlayerArmourUpgrades") >= 1)
            {
                PlayerPrefs.SetInt("PlayerArmourLevel", PlayerPrefs.GetInt("PlayerArmourLevel") + 1);
                PlayerPrefs.SetInt("PlayerScrapTotal", PlayerPrefs.GetInt("PlayerScrapTotal") - upgradeCost);
            }
        }

        // update text
        armour.text = "Armour: " + PlayerPrefs.GetInt("PlayerArmourLevel");
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
    }

    void UpdateHubStats()
    {
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
        engine.text = "Engine: " + PlayerPrefs.GetInt("PlayerEngineLevel");
        armour.text = "Armour: " + PlayerPrefs.GetInt("PlayerArmourLevel");
    }


    public void WeaponLeft()
    {
        if (PlayerPrefs.GetInt("PlayerCurrentWeapon") >= 1)
        {
            PlayerPrefs.SetInt("PlayerCurrentWeapon", PlayerPrefs.GetInt("PlayerCurrentWeapon") - 1);
        }
        // update text
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
    }

    public void WeaponRight()
    {
        if (PlayerPrefs.GetInt("PlayerCurrentWeapon") == weapons.Length-1) // player already has all weapons
        {
            print("Player already has all weapons");
            return;
        }

        // does player already own the next weapon?
        if (PlayerPrefs.GetInt("PlayerCurrentWeapon")+1 <= PlayerPrefs.GetInt("PlayerTotalWeapons"))
        {
            PlayerPrefs.SetInt("PlayerCurrentWeapon", PlayerPrefs.GetInt("PlayerCurrentWeapon") + 1);
        }
        // enough scrap & cache available?
        else if (PlayerPrefs.GetInt("PlayerScrapTotal") >= upgradeCost && PlayerPrefs.GetInt("PlayerWeaponUpgrades") >= 1)
        {
            PlayerPrefs.SetInt("PlayerCurrentWeapon", PlayerPrefs.GetInt("PlayerCurrentWeapon") + 1);
            PlayerPrefs.SetInt("PlayerScrapTotal", PlayerPrefs.GetInt("PlayerScrapTotal") - upgradeCost);
            PlayerPrefs.SetInt("PlayerTotalWeapons", PlayerPrefs.GetInt("PlayerTotalWeapons") + 1);
        }

        // update text
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
    }
}