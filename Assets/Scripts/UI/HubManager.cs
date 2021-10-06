using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class HubManager : MonoBehaviour
{
    public Text scrapTotal, weapon, engine, armour, module, moduleDescription;
    public Text scrapTotal2, weaponParts, engineParts, armourParts;

    public string[] weapons;
    public ModuleManager moduleManager;

    public int upgradeCost = 50, maxArmour = 4, maxEngine = 4;

    public GameObject[] UIarray;

    public AudioMixer masterAudioMixer;

    public Slider master, music, sfx;
    public Toggle camRotationToggle;


    // Start is called before the first frame update
    void Start()
    {
        // set default settings
        if (!PlayerPrefs.HasKey("CamRotation"))
            PlayerPrefs.SetInt("CamRotation", 1);

        if (!PlayerPrefs.HasKey("MasterVolume"))
            PlayerPrefs.SetFloat("MasterVolume", .75f);
        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", .75f);
        if (!PlayerPrefs.HasKey("SFXVolume"))
            PlayerPrefs.SetFloat("SFXVolume", .75f);

        UpdateHubStats();

        // set volume sliders
        master.value = PlayerPrefs.GetFloat("MasterVolume");

        music.value = PlayerPrefs.GetFloat("MusicVolume");

        sfx.value = PlayerPrefs.GetFloat("SFXVolume");

        //set actual volumes
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));

        //set cam toggle
        if (PlayerPrefs.GetInt("CamRotation") == 1)
            camRotationToggle.isOn = true;
        else
            camRotationToggle.isOn = false;

        print(PlayerPrefs.GetInt("CamRotation"));

    }

    public void ToggleCamRotation()
    {
        if (PlayerPrefs.GetInt("CamRotation") == 1)
        {
            PlayerPrefs.SetInt("CamRotation", 0);
        } else
        {
            PlayerPrefs.SetInt("CamRotation", 1);
        }

        print(gameObject.name +": " + PlayerPrefs.GetInt("CamRotation"));
    }

    public void SetMasterVolume(float volume)
    {
        masterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        masterAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        masterAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
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
        if (GameManager.instance.stats.unlockedModuleIDs.Count > 0 && GameManager.instance.stats.currentModuleIDNumber != -1)
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
        moduleDescription.text = moduleManager.FindDescription(GameManager.instance.stats.currentModuleID);
    }

    public void ModuleRight()
    {
        if (GameManager.instance.stats.unlockedModuleIDs.Count > 0 && GameManager.instance.stats.currentModuleIDNumber != -1)
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
        moduleDescription.text = moduleManager.FindDescription(GameManager.instance.stats.currentModuleID);
    }
}
