using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStats
{
    public int planetDifficulty;
    public int planetLevelLimits;
    public int planetWeaponPartChance;
    public int planetEnginePartChance;
    public int planetArmourPartChance;
    public int planetFaction; //1 = Alpha, 2 = Beta, 3 = Gamma, 4 = Omega, 5 = Epsilon, 6 = Merchant
    public int planetMapSeed; //0 = Alpha Homeworld, 1 = Beta Homeworld, 2 = Gamma Homeworld, 3 = Omega Homeworld, 4 = Epsilon Hideout
    public int planetDefaultEnemy; //0 = Swarmer, 1 = Detonator, 2 = Collector, 3 = CollectorPacifist
    public int planetRepairCost;
    public string planetName;
    public PlanetStats(int diff, int limits, int wPart, int ePart, int aPart, int faction, int seed, int enemy, int repair, string name)
    {
        planetDifficulty = diff;
        planetLevelLimits = limits;
        planetWeaponPartChance = wPart;
        planetEnginePartChance = ePart;
        planetArmourPartChance = aPart;
        planetFaction = faction; //1 = Alpha, 2 = Beta, 3 = Gamma, 4 = Omega, 5 = Epsilon, 6 = Merchant
        planetMapSeed = seed; //0 = Alpha Homeworld, 1 = Beta Homeworld, 2 = Gamma Homeworld, 3 = Omega Homeworld, 4 = Epsilon Hideout
        planetDefaultEnemy = enemy; //0 = Swarmer, 1 = Detonator, 2 = Collector, 3 = CollectorPacifist
        planetRepairCost = repair;
        planetName = name;
    }
}
