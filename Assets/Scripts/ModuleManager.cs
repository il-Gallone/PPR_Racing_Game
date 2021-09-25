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
            modules = new ModuleInfo[3] { module1, module2, module3 };
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
