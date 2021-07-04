using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int numOfEnemiesInScene = 0;

    int enemiesToSpawn = 1;
    public int maxEnemiesSpawned = 5;  // max enemies spawned at once
    public int maxEnemiesAllowed = 10; // max enemies in scene
    public float minSpawnRadius = 10f, maxSpawnRadius = 20f;

    float timer;
    public float timeUntilSpawn = 30f;

    public GameObject enemyPrefab;

    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        RandomlySpawnObjects(enemyPrefab, 1);
        enemiesToSpawn++;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeUntilSpawn)
        {
            timer = 0f;

            RandomlySpawnObjects(enemyPrefab, enemiesToSpawn);
            if (enemiesToSpawn < maxEnemiesSpawned)
                enemiesToSpawn++;

            //print(numOfEnemiesInScene);
        }
    }

    void RandomlySpawnObjects(GameObject objectToSpawn, int objectCount)
    {
        for (int i = 0; i < objectCount; i++)
        {
            if (numOfEnemiesInScene < maxEnemiesAllowed)
            {
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                direction /= direction.magnitude;
                Vector3 randomPos = direction * Random.Range(minSpawnRadius, maxSpawnRadius);

                Instantiate(objectToSpawn, player.position + randomPos, Quaternion.identity);
                // add player position to randomPos
            }
            
        }
    }
}
