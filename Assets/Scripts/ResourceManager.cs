using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject energyAsteroid, repairAsteroid, objective;

    public float maxDistX = 20f, maxDistY = 20f; // max distance the object can spawn away from the player
    public float minDistX = 5, minDistY = 5; //min distance the object can spawn away from the player
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
            float randomDistX = Random.Range(minDistX, maxDistX) * (1 + 2 * Random.Range(-1, 1));
            float randomDistY = Random.Range(minDistX, maxDistY) * (1 + 2 * Random.Range(-1, 1));
            Vector3 randomPos = new Vector3(randomDistX, randomDistY, 0);

            GameObject newObject = Instantiate(objectToSpawn, randomPos, Quaternion.identity);
        }
    }
}
