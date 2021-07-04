using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    int enemies = 1;
    public int maxEnemies = 10;
    public float minSpawnRadius = 10f, maxSpawnRadius = 20f;

    float timer;
    public float timeUntilSpawn = 30f;

    public GameObject enemyPrefab;

    private void Start()
    {
        RandomlySpawnObjects(enemyPrefab, 1);
        enemies++;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeUntilSpawn)
        {
            timer = 0f;

            RandomlySpawnObjects(enemyPrefab, enemies);
            if (enemies < maxEnemies)
                enemies++;
        }
    }

    void RandomlySpawnObjects(GameObject objectToSpawn, int objectCount)
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            direction /= direction.magnitude;
            Vector3 randomPos = direction * Random.Range(minSpawnRadius, maxSpawnRadius);

            Instantiate(objectToSpawn, randomPos, Quaternion.identity);
        }
    }
}
