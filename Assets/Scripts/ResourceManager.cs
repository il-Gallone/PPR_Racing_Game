using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject energyAsteroid, repairAsteroid, objective;

    public float minRadius = 1f, maxRadius = 20f; //Minimum and Maximum Radius for spawning objects
    public int energyCount = 20, repairCount = 20, objectiveCount = 10;

    void Start()
    {
        RandomlySpawnObjects(energyAsteroid, energyCount);
        RandomlySpawnObjects(repairAsteroid, repairCount);
        RandomlySpawnObjects(objective, objectiveCount);
    }

    void RandomlySpawnObjects(GameObject objectToSpawn, int objectCount) 
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            direction /= direction.magnitude;
            Vector3 randomPos = direction * Random.Range(minRadius, maxRadius);

            Instantiate(objectToSpawn, randomPos, Quaternion.identity);
        }
    }
}
