using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    GameObject energyAsteroid, repairAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        RandomlySpawnObjects(energyAsteroid, 10);
        RandomlySpawnObjects(repairAsteroid, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomlySpawnObjects(GameObject objectToSpawn, int objectCount) { 
    }
}
