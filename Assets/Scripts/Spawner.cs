using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{

    
    [Header("Attributes")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int baseEnemies = 7; //starting number of enemies
    [SerializeField] private float enemiesPS = 0.5f; //enemis per second
    [SerializeField] private float timeBetweenWaves = 5f; //time between waves
    [SerializeField] private float difficultyScale = 0.75f;  //ratio of difficulty scaling

    [Header("Events")]
    public static UnityEvent onEnemyKill = new UnityEvent();

    private int currentWave = 1;
    private float spawnTimer;
    private int enemiesAlive;
    private int enemiesLeft;        //enemies left to spawn
    private bool isSpawning = false;

    private void Awake(){
        onEnemyKill.AddListener(EnemyKilled);
    }

    private void Start(){
        StartCoroutine(StartWave());
    }

    private void Update(){
        if (!isSpawning) return;
        spawnTimer += Time.deltaTime;

         if((spawnTimer >= 1f / enemiesPS) && enemiesLeft > 0){
            SpawnEnemy();
            enemiesLeft--;
            enemiesAlive++;
            spawnTimer = 0f;
         }

         if (enemiesAlive == 0 && enemiesLeft == 0){
            EndWave();
         }
    }

    private void EndWave(){
        isSpawning = false;
        spawnTimer = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave(){
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeft = EnemiesRampUp();
    }


    private int EnemiesRampUp(){
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScale));
    }

    private void SpawnEnemy(){
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, level_manager.main.StartNode.position, Quaternion.identity);
    }

    private void EnemyKilled(){
        enemiesAlive--;
    }

}
