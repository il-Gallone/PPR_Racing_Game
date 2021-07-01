using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public Text scrapTotal, weapon;

    public string[] weapons;

    public int upgradeCost = 50, maxArmour = 4, maxEngine = 4;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PlayerScrapTotal", 500);
        PlayerPrefs.SetInt("PlayerWeaponUpgrades", 500);
        PlayerPrefs.SetInt("PlayerTotalWeapons", 0);

        UpdateHubStats();
    }

    void UpdateHubStats()
    {
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];

    }


    public void WeaponLeft()
    {
        if (PlayerPrefs.GetInt("PlayerCurrentWeapon") >= 1)
        {
            PlayerPrefs.SetInt("PlayerCurrentWeapon", PlayerPrefs.GetInt("PlayerCurrentWeapon") - 1);
        }
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
    }

    public void WeaponRight()
    {
        if (PlayerPrefs.GetInt("PlayerCurrentWeapon") == weapons.Length-1) // player already has all weapons
        {
            print("Player already has all weapons");
            return;
        }

        //if (PlayerPrefs.GetInt("PlayerCurrentWeapon") < PlayerPrefs.GetInt("PlayerTotalWeapons"))
        //{
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
            
        //}
        // update text
        weapon.text = "Weapon: " + weapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");
    }
}
