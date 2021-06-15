using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject energyAsteroid, repairAsteroid;

    public float maxDistX = 20f, maxDistY = 20f; // max distance the object can spawn away from the player

    void Start()
    {
        RandomlySpawnObjects(energyAsteroid, 10);
        RandomlySpawnObjects(repairAsteroid, 10);
    }

    void RandomlySpawnObjects(GameObject objectToSpawn, int objectCount) 
    {
        for (int i = 0; i < objectCount; i++)
        {
            float randomDistX = Random.Range(maxDistX * -1, maxDistX);
            float randomDistY = Random.Range(maxDistY * -1, maxDistY);

            Vector3 randomPos = new Vector3(randomDistX, randomDistY, 0);

            GameObject newObject = Instantiate(objectToSpawn, randomPos, Quaternion.identity);
        }
    }
}
