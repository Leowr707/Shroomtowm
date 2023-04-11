using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoAtaque : MonoBehaviour
{
    [Header("Configura��es do disparo do inimigo")]
    public float cdTiroInimigo;
    public GameObject prefabTiroInimigo;
    public GameObject spawnPointDoTiroInimigo;
    public bool ativarTiro;

    // Start is called before the first frame update
    void Start()
    {
        if (ativarTiro)
        {
            //o tempo que levar� para ativar o c�digo e de quando em quanto tempo ele vai se repetir
            InvokeRepeating("Atirar", 2, cdTiroInimigo);
        }
    }

    
    void Atirar()
    {
        Instantiate(prefabTiroInimigo, spawnPointDoTiroInimigo.transform.position, spawnPointDoTiroInimigo.transform.rotation);
        Destroy(prefabTiroInimigo, 5);  
    }
}
