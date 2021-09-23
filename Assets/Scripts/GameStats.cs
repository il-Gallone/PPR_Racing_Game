using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    //Player Resources
    public int scrap = 0;
    public int weaponParts = 0;
    public int engineParts = 0;
    public int armourParts = 0;

    //Player Upgrades
    public string currentWeaponID = "Rifle";
    public int currentWeaponIDNumber = 0;
    public List<string> unlockedWeaponIDs = new List<string>();
    public string currentModuleID = "None";
    public int currentModuleIDNumber = -1;
    public List<string> unlockedModuleIDs = new List<string>();
    public int engineLevel = 0;
    public int armourLevel = 0;

    //Player Inventory
    public List<string> inventory = new List<string>();

    //Player Favour
    public int faction1Favour = 50; //Alpha Faction
    public int faction2Favour = 50; //Beta Faction
    public int faction3Favour = 50; //Gamma Faction
    public int faction4Favour = 50; //Omega Faction
    public int faction5Favour = 50; //Epsilon Faction

    public bool firstTime = true;
}
