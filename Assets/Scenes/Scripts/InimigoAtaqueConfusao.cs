using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoAtaqueConfusao : MonoBehaviour
{
    [Header("Configurações do disparo do inimigo")]
    public float cdTiroInimigo;
    public GameObject prefabTiroInimigo;
    public GameObject spawnPointDoTiroInimigo;
    public bool ativarTiro;

    private Transform playerTransform;

    private void Start()
    {
        if (ativarTiro)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            // o tempo que levará para ativar o código e de quando em quanto tempo ele vai se repetir
            InvokeRepeating("Atirar", 2, cdTiroInimigo);
        }
    }

    private void Atirar()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= 50)
        {
            GameObject tiro = Instantiate(prefabTiroInimigo, spawnPointDoTiroInimigo.transform.position, spawnPointDoTiroInimigo.transform.rotation);
        }
    }
}
