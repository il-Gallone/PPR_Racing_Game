using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public Text scrapTotal, weapon, engine, armour;
    public Text scrapTotal2, weaponParts, engineParts, armourParts;

    public string[] weapons;

    public int upgradeCost = 50, maxArmour = 4, maxEngine = 4;

    public GameObject[] UIarray;

    // Start is called before the first frame update
    void Start()
    {
        //for testing
        //PlayerPrefs.SetInt("PlayerScrapTotal", 500);
        //PlayerPrefs.SetInt("PlayerWeaponUpgrades", 500);
        //PlayerPrefs.SetInt("PlayerEngineUpgrades", 500);
        //PlayerPrefs.SetInt("PlayerArmourUpgrades", 500);

        //PlayerPrefs.SetInt("PlayerTotalWeapons", 0);
        //PlayerPrefs.SetInt("PlayerEngineLevel", 0);
        //PlayerPrefs.SetInt("PlayerArmourLevel", 0);


        if (!PlayerPrefs.HasKey("PlayerScrapTotal"))
        {
            PlayerPrefs.SetInt("PlayerScrapTotal", 0);
            PlayerPrefs.SetInt("PlayerWeaponUpgrades", 0);
            PlayerPrefs.SetInt("PlayerEngineUpgrades", 0);
            PlayerPrefs.SetInt("PlayerArmourUpgrades", 0);
            PlayerPrefs.SetInt("PlayerTotalWeapons", 0);
            PlayerPrefs.SetInt("PlayerEngineLevel", 0);
            PlayerPrefs.SetInt("PlayerArmourLevel", 0);
        }

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
        if (PlayerPrefs.GetInt("PlayerEngineLevel") < maxEngine)
        {
            // enough scrap and upgrade available?
            if (PlayerPrefs.GetInt("PlayerScrapTotal") >= upgradeCost && PlayerPrefs.GetInt("PlayerEngineUpgrades") >= 10)
            {
                PlayerPrefs.SetInt("PlayerEngineLevel", PlayerPrefs.GetInt("PlayerEngineLevel") + 1);
                PlayerPrefs.SetInt("PlayerScrapTotal", PlayerPrefs.GetInt("PlayerScrapTotal") - upgradeCost);
                PlayerPrefs.SetInt("PlayerEngineUpgrades", PlayerPrefs.GetInt("PlayerEngineUpgrades") - 10);
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
            if (PlayerPrefs.GetInt("PlayerScrapTotal") >= upgradeCost && PlayerPrefs.GetInt("PlayerArmourUpgrades") >= 10)
            {
                PlayerPrefs.SetInt("PlayerArmourLevel", PlayerPrefs.GetInt("PlayerArmourLevel") + 1);
                PlayerPrefs.SetInt("PlayerScrapTotal", PlayerPrefs.GetInt("PlayerScrapTotal") - upgradeCost);
                PlayerPrefs.SetInt("PlayerArmourUpgrades", PlayerPrefs.GetInt("PlayerArmourUpgrades") - 10);
            }
        }

        // update text
        armour.text = "Armour: " + PlayerPrefs.GetInt("PlayerArmourLevel");
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
    }

    public void UpdateHubStats()
    {
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
        engine.text = "Engine: " + PlayerPrefs.GetInt("PlayerEngineLevel");
        armour.text = "Armour: " + PlayerPrefs.GetInt("PlayerArmourLevel");
    }

    public void UpdateInventory()
    {
        scrapTotal2.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
        weaponParts.text = "Weapon Parts: " + PlayerPrefs.GetInt("PlayerWeaponUpgrades");
        engineParts.text = "Engine Parts: " + PlayerPrefs.GetInt("PlayerEngineUpgrades");
        armourParts.text = "Armour Parts: " + PlayerPrefs.GetInt("PlayerArmourUpgrades");
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
        else if (PlayerPrefs.GetInt("PlayerScrapTotal") >= upgradeCost && PlayerPrefs.GetInt("PlayerWeaponUpgrades") >= 10)
        {
            PlayerPrefs.SetInt("PlayerCurrentWeapon", PlayerPrefs.GetInt("PlayerCurrentWeapon") + 1);
            PlayerPrefs.SetInt("PlayerScrapTotal", PlayerPrefs.GetInt("PlayerScrapTotal") - upgradeCost);
            PlayerPrefs.SetInt("PlayerWeaponUpgrades", PlayerPrefs.GetInt("PlayerWeaponUpgrades") - 10);
            PlayerPrefs.SetInt("PlayerTotalWeapons", PlayerPrefs.GetInt("PlayerTotalWeapons") + 1);
        }

        // update text
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
    }
}
