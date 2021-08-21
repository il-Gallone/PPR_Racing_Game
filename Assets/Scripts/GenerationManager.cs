using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public GameObject objective;
    public GameObject[] mapTerrainPrefabs; //0 = Energy Asteroid, 1 = Repair Asteroid, 2 = Station1, 3 = Station2

    public int objectiveCount = 10;

    void Start()
    {
        GenerateMap(GameManager.instance.mapGenerationSeed);
    }

    void RandomlySpawnObjects(GameObject objectToSpawn, int objectCount, Vector3 centrePos, float minRadius, float maxRadius) 
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            direction /= direction.magnitude;
            Vector3 randomPos = direction * Random.Range(minRadius, maxRadius);

            Instantiate(objectToSpawn, centrePos + randomPos, Quaternion.identity);
        }
    }

    void GenerateMap(int mapGenerationSeed)
    {
        switch (mapGenerationSeed)
        {
            case 0: //Alpha Homeworld
                {
                    RandomlySpawnObjects(mapTerrainPrefabs[2], 1, Vector3.zero, 5, GameManager.instance.levelLimits - 10);
                    RandomlySpawnObjects(objective, 5, GameObject.FindGameObjectWithTag("Station").transform.position, 2, 4);
                    RandomlySpawnObjects(objective, 5, Vector3.zero, 1, GameManager.instance.levelLimits-5);
                    RandomlySpawnObjects(mapTerrainPrefabs[0], 20, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[1], 20, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    break;
                }
            case 1: //Beta Homeworld
                {
                    RandomlySpawnObjects(objective, 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[0], 25, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[1], 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    break;
                }
            case 2: //Gamma Homeworld
                {
                    RandomlySpawnObjects(mapTerrainPrefabs[3], 1, Vector3.zero, 5, GameManager.instance.levelLimits - 10);
                    RandomlySpawnObjects(objective, 7, GameObject.FindGameObjectWithTag("Station").transform.position, 2, 4);
                    RandomlySpawnObjects(objective, 3, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[0], 25, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[1], 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    break;
                }
            case 3: //Omega Homeworld
                {
                    RandomlySpawnObjects(objective, 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[0], 12, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[1], 12, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    break;
                }
            case 4: //Epsilon Hideout
                {
                    objectiveCount = 15;
                    RandomlySpawnObjects(objective, 15, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[0], 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[1], 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    break;
                }
            default:
                {
                    RandomlySpawnObjects(objective, 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[0], 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    RandomlySpawnObjects(mapTerrainPrefabs[1], 10, Vector3.zero, 1, GameManager.instance.levelLimits - 5);
                    break;
                }
        }
    }
}
