using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
   public Text waveText;
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float waveInterval = 2f;
    public int maxEnemies = 10;
    private int currentWave = 1;
    private bool spawningEnemies = false;
    public GameObject[] BossPrefabs; 

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        spawningEnemies = true;
        waveText.text = "Wave " + currentWave;
        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        waveText.gameObject.SetActive(false);

        int enemiesToSpawn = currentWave * 10;
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemiesToSpawn)
        {
            if (GameObject.FindGameObjectsWithTag("Taginimigo").Length < maxEnemies)
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject enemy = Instantiate(enemyPrefabs[randomIndex], GetRandomSpawnPoint(), Quaternion.identity);
                enemiesSpawned++;
            }

            yield return new WaitForSeconds(waveInterval);
        }
        if (currentWave == 1)
    {
        int randomIndex = Random.Range(0, BossPrefabs.Length);
        GameObject SpawnBoss = Instantiate(BossPrefabs[randomIndex], GetRandomSpawnPoint(), Quaternion.identity); // Spawn do boss na terceira wave
        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        waveText.gameObject.SetActive(false);
    }

        spawningEnemies = false;
        currentWave++;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }

    private void Update()
    {
        if (!spawningEnemies && GameObject.FindGameObjectsWithTag("Taginimigo").Length == 0)
        {
            StartCoroutine(SpawnWave());
        }
    }
}



