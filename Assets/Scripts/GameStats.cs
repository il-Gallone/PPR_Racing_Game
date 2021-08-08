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
    public List<string> unlockedWeaponIDs = new List<string>{"Rifle"};
    public int engineLevel = 0;
    public int armourLevel = 0;

    //Player Inventory
    public List<string> inventory = new List<string>();
}
