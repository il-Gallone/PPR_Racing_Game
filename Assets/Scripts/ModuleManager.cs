using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ModuleManager : MonoBehaviour
{
    public ModuleInfo[] modules;

    // Start is called before the first frame update
    void Awake()
    {
        LoadModules();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.N))
        {
            ModuleInfo module1 = new ModuleInfo();
            module1.module = "None";
            module1.moduleDescription = "You have no Module Equipped.";
            ModuleInfo module2 = new ModuleInfo();
            module2.module = "Shield Generator";
            module2.moduleDescription = "Passively Generate a Shield that blocks most forms of Damage.";
            ModuleInfo module3 = new ModuleInfo();
            module3.module = "Speed Booster";
            module3.moduleDescription = "Press SPACE in order to temporarily increase max speed and acceleration.";
            ModuleInfo module4 = new ModuleInfo();
            module4.module = "Repair Nanobots";
            module4.moduleDescription = "Repair your ship between levels.";
            ModuleInfo module5 = new ModuleInfo();
            module5.module = "Emergency Teleport";
            module5.moduleDescription = "Teleport to a random nearby location.";
            ModuleInfo module6 = new ModuleInfo();
            module6.module = "Solar Collector";
            module6.moduleDescription = "Recharges energy while not moving.";
            ModuleInfo module7 = new ModuleInfo();
            module7.module = "Ramming Armour";
            module7.moduleDescription = "Take less damage upon impact and deal damage to enemies+objects.";
            ModuleInfo module8 = new ModuleInfo();
            module8.module = "Scrap Recycler";
            module8.moduleDescription = "Recycle 50% of scrap collected. For every 100 recycled scrap gain a random part.";
            ModuleInfo module9 = new ModuleInfo();
            module9.module = "Ore Purifier";
            module9.moduleDescription = "Asteroid Drops are 50% more effective.";
            ModuleInfo module10 = new ModuleInfo();
            module10.module = "Secure Storage";
            module10.moduleDescription = "Retain some of your scrap and parts if defeated.";
            ModuleInfo module11 = new ModuleInfo();
            module11.module = "Ore Refiner";
            module11.moduleDescription = "Refine 50% of asteroid drops. Gain a small amount of scrap from refined drops.";
            ModuleInfo module12 = new ModuleInfo();
            module12.module = "Defective Super Enhancer";
            module12.moduleDescription = "All Ship Upgrades are boosted by two levels. Controls are reversed.";
            ModuleInfo module13 = new ModuleInfo();
            module13.module = "Super Enhancer";
            module13.moduleDescription = "All Ship Upgrades are boosted by two levels.";
            ModuleInfo module14 = new ModuleInfo();
            module14.module = "Tractor Beam";
            module14.moduleDescription = "Collectibles get pulled in when nearby.";
            ModuleInfo module15 = new ModuleInfo();
            module15.module = "Weapon Overcharger";
            module15.moduleDescription = "Temporarily increase weapon fire-rate and allow automatic fire.";
            modules = new ModuleInfo[15] { module1, module2, module3, module4, module5, module6, module7, module8, module9, module10,
                                           module11, module12, module13, module14, module15 };
            SaveModules();
        }
    }

    void LoadModules()
    {
        string dataPath = Path.Combine(Application.dataPath, "moduleData.txt");
        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            string jsonString;
            jsonString = streamReader.ReadToEnd();
            modules = JsonConvert.DeserializeObject<ModuleInfo[]>(jsonString);
        }
    }

    void SaveModules()
    {
        string dataPath = Path.Combine(Application.dataPath, "moduleData.txt");
        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            string jsonString;
            jsonString = JsonConvert.SerializeObject(modules);
            streamWriter.Write(jsonString);
        }
    }

    public string FindDescription(string moduleName)
    {
        for(int i=0; i < modules.Length; i++)
        {
            if(moduleName == modules[i].module)
            {
                return modules[i].moduleDescription;
            }
        }
        return "Error: No Available Description for this Module";
    }
}
