using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoAtaqueMissil : MonoBehaviour
{
    [Header("Configuracoes do disparo do inimigo")]
    public float cdTiroInimigo;
    public float distanciaMinimaDeDisparo = 60f;
    public GameObject prefabTiroInimigo;
    public GameObject spawnPointDoTiroInimigo;
    public bool ativarTiro;
    public float tempoParaDestruicao;
    



    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
      

        if (ativarTiro)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            // o tempo que levará para ativar o código e de quando em quanto tempo ele vai se repetir
            InvokeRepeating("Atirar", 2, cdTiroInimigo);
        }




    }

    /* void aumentarContagemMisseis(int contagemAtual)
    {
        misseisAtivos = misseisAtivos + contagemAtual;
    }*/
    void Atirar()
    {
        
            if (Vector3.Distance(transform.position, playerTransform.position) <= distanciaMinimaDeDisparo)
            {

                GameObject tiro = Instantiate(prefabTiroInimigo, spawnPointDoTiroInimigo.transform.position, spawnPointDoTiroInimigo.transform.rotation);
                Destroy(tiro, tempoParaDestruicao);
                
            }

        }
    
}



