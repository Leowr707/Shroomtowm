using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigos : MonoBehaviour
{
    public GameObject inimigoPrefab;
    public float tempoEntreSpawns = 1f;
    public float spawnRadius = 10f;
    public float limiteY = 5f;
    public float limiteX = 5f;

    private float tempoDecorrido = 0f;

    private void Update()
    {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido >= tempoEntreSpawns)
        {
            SpawnInimigo();
            tempoDecorrido = 0f;
        }
    }

    private void SpawnInimigo()
    {
        if (gameObject == null) return;
        Vector3 posicaoSpawn = new Vector3(Random.Range(-limiteX, limiteX), Random.Range(-limiteY, limiteY), 0);
        Instantiate(inimigoPrefab, posicaoSpawn, Quaternion.identity);
    }
}
