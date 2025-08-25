using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject gemPrefab;
    //public GameObject heartPrefab;

    private float spawnRange = 12;
    private int enemiesToSpawn = 1;
    private int enemyCount;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        if(player.GetComponent<PlayerController>().isAlive == true)
        {
            SpawnEnemies(enemiesToSpawn);
            spawnGems(enemiesToSpawn);
        }  
    }

    // Update is called once per frame
    void Update()
    {



        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0 && player.GetComponent<PlayerController>().isAlive == true)
        {
            SpawnEnemies(enemiesToSpawn);
            spawnGems(enemiesToSpawn);
            enemiesToSpawn++;
        }
    }

    void SpawnEnemies(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        
    }

    void spawnGems(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(gemPrefab, GenerateSpawnPosition(), gemPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0.6f, spawnPosZ);

        return randomPos;
    }
}
