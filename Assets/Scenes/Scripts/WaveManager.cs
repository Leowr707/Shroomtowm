using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public int enemiesPerWave = 10;
    public int bossWaveInterval = 10;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public Text waveText;

    private int enemiesRemaining;

    private void Start()
    {
        StartWave();
    }

    private void StartWave()
    {
        enemiesRemaining = enemiesPerWave;
        StartCoroutine(SpawnEnemies());
        waveText.text = "Wave: " + currentWave;
        Debug.Log("Starting wave " + currentWave + " with " + enemiesPerWave + " enemies.");
    }
    

    private IEnumerator SpawnEnemies()
    {
    int enemiesSpawned = 0;

    while (enemiesSpawned < enemiesPerWave)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemiesSpawned++;
        yield return new WaitForSeconds(1f);
    }

    if (currentWave % bossWaveInterval == 0)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

    public void EnemyDefeated()
    {
    enemiesRemaining--;

    if (enemiesRemaining <= 0)
    {
        currentWave++;
        Debug.Log("Starting next wave: " + currentWave);
        StartWave();
    }
}
}



