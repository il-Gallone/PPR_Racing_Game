using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int hordeCount = 20;

    public static int numOfEnemiesInScene = 0;

    int enemiesToSpawn = 1;
    public int maxEnemiesSpawned = 5;  // max enemies spawned at once
    public int maxEnemiesAllowed = 10; // max enemies in scene
    public float minSpawnRadius = 10f, maxSpawnRadius = 20f;

    float timer;
    public float timeUntilSpawn = 30f;

    public GameObject[] enemyPrefabs; //0 = Swarmer, 1 = Detonator, 2 = Collector, 3 = CollectorPacifist
    public int primaryEnemy = 0;

    Transform player;
    [Header("Boss Settings")]
    public float bossScale = 3f;
    public float bossDamageMultiplier = 3f;
    public float bossHealth = 500f;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "BossLevel")
        {
            float minFavour = Mathf.Min(    GameManager.instance.stats.faction1Favour,
                                            GameManager.instance.stats.faction2Favour,
                                            GameManager.instance.stats.faction3Favour,
                                            GameManager.instance.stats.faction4Favour,
                                            GameManager.instance.stats.faction5Favour);
            if (minFavour == GameManager.instance.stats.faction1Favour)
                primaryEnemy = 5;
            else if (minFavour == GameManager.instance.stats.faction2Favour)
                primaryEnemy = 5;
            else if (minFavour == GameManager.instance.stats.faction3Favour)
                primaryEnemy = 0;
            else if (minFavour == GameManager.instance.stats.faction4Favour)
                primaryEnemy = 6;
            else if (minFavour == GameManager.instance.stats.faction5Favour)
                primaryEnemy = 4;
        }
        else
            primaryEnemy = GameManager.instance.primaryEnemySpawn;
        
        if(primaryEnemy == 2 || primaryEnemy == 3)
        {
            maxEnemiesSpawned = 1;
            maxEnemiesAllowed = 2;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BossLevel")
        {
            //spawn horde
            RandomlySpawnObjects(enemyPrefabs[primaryEnemy], hordeCount);

            //spawn captain
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            direction /= direction.magnitude;
            Vector3 randomPos = direction * Random.Range(minSpawnRadius, maxSpawnRadius);

            GameObject captain = Instantiate(enemyPrefabs[primaryEnemy], player.position + randomPos, Quaternion.identity);

            captain.transform.localScale = new Vector3(bossScale, bossScale, bossScale);
            captain.GetComponentInChildren<WeaponController>().damageMultiplier = 3f;
            captain.GetComponent<EnemyBase>().HP = bossHealth;

            return;
        }
        RandomlySpawnObjects(enemyPrefabs[primaryEnemy], 1);
        enemiesToSpawn++;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "BossLevel")
            return;

        timer += Time.deltaTime;

        if (timer >= timeUntilSpawn)
        {
            timer = 0f;

            RandomlySpawnObjects(enemyPrefabs[primaryEnemy], enemiesToSpawn);
            if (enemiesToSpawn < maxEnemiesSpawned)
                enemiesToSpawn++;

            //print(numOfEnemiesInScene);
        }
    }

    void RandomlySpawnObjects(GameObject objectToSpawn, int objectCount)
    {
        for (int i = 0; i < objectCount; i++)
        {
            if (numOfEnemiesInScene < maxEnemiesAllowed || SceneManager.GetActiveScene().name == "BossLevel")
            {
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                direction /= direction.magnitude;
                Vector3 randomPos = direction * Random.Range(minSpawnRadius, maxSpawnRadius);

                GameObject enemy = Instantiate(objectToSpawn, player.position + randomPos, Quaternion.identity);

                // add player position to randomPos
            }

        }
    }
}
