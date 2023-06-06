using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtaque : MonoBehaviour
{
    [Header("Configuracoes do disparo do inimigo")]
    public float cdTiroInimigo;
    public float distanciaMinimaDeDisparo = 60f;
    public float tempoAtrasoAtaquesDiagonais = 0;
    private bool atirarConfusaoAtivo = true;
    private bool atirarDiagonalAtivo = true;
    private float tempoAtirarConfusao = 1.5f;
    private float tempoAtirarDiagonal = 1.5f;
    private float timerAtirarConfusao = 0f;
    private float timerAtirarDiagonal = 0f;
    public GameObject prefabTiroInimigo;
    public GameObject prefabTiroInimigoConfusao;
    [Header("Tiros padroes retos")]
    public GameObject spawnPointCima, spawnPointDireita, spawnPointBaixo, spawnPointEsquerda;
    [Header("Tiros padroes diagonais")]
    public GameObject spawnPointNordeste, spawnPointSudeste, spawnPointSudoeste, spawnPointNoroeste;
    public bool ativarTiro;
    public bool ativarTiroConfusao = false;
    public bool ativarTiroDiagonal = true;
    public Boss bossScript;

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

    void Update() {

        if (bossScript.vida <= 80) { 
            Debug.Log("Atirar");
            ativarTiroConfusao =true;
            ativarTiroDiagonal=false;
            
        }
        if(ativarTiroConfusao == true)
        {
            if (atirarConfusaoAtivo)
        {
            timerAtirarConfusao += Time.deltaTime;

            if (timerAtirarConfusao >= tempoAtirarConfusao)
            {
                atirarConfusao();
                timerAtirarConfusao = 0f;
        }
            }
        }

        /* if (ativarTiroDiagonal == true)
         {
             if (atirarDiagonalAtivo)
             {
                 timerAtirarDiagonal += Time.deltaTime;

                 if (timerAtirarDiagonal >= tempoAtirarDiagonal)
                 {
                     atirarConfusao();
                     timerAtirarDiagonal = 0f;
                 }
             }
         }*/
    }



    void atirarConfusao() {

        GameObject tiroNordeste = Instantiate(prefabTiroInimigoConfusao, spawnPointNordeste.transform.position, spawnPointNordeste.transform.rotation);
        Destroy(tiroNordeste, 5);
        GameObject tiroSudeste = Instantiate(prefabTiroInimigoConfusao, spawnPointSudeste.transform.position, spawnPointSudeste.transform.rotation);
        Destroy(tiroSudeste, 5);
        GameObject tiroSudoeste = Instantiate(prefabTiroInimigoConfusao, spawnPointSudoeste.transform.position, spawnPointSudoeste.transform.rotation);
        Destroy(tiroSudoeste, 5);
        GameObject tiroNoroeste = Instantiate(prefabTiroInimigoConfusao, spawnPointNoroeste.transform.position, spawnPointNoroeste.transform.rotation);
        Destroy(tiroNoroeste, 5);


    }

    void Atirar()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= distanciaMinimaDeDisparo)
        {

            GameObject tiroCima = Instantiate(prefabTiroInimigo, spawnPointCima.transform.position, spawnPointCima.transform.rotation);
            Destroy(tiroCima, 5);
            GameObject tiroDireita = Instantiate(prefabTiroInimigo, spawnPointDireita.transform.position, spawnPointDireita.transform.rotation);
            Destroy(tiroDireita, 5);
            GameObject tiroBaixo = Instantiate(prefabTiroInimigo, spawnPointBaixo.transform.position, spawnPointBaixo.transform.rotation);
            Destroy(tiroBaixo, 5);
            GameObject tiroEsquerda = Instantiate(prefabTiroInimigo, spawnPointEsquerda.transform.position, spawnPointEsquerda.transform.rotation);
            Destroy(tiroEsquerda, 5);

           

    }
    }
    /* IEnumerator ExecutarAtaquesDiagonais()
     {
         yield return new WaitForSeconds(tempoAtrasoAtaquesDiagonais);
         ataquesDiagonais();
     }

     void ataquesDiagonais()
     {
         GameObject tiroNordeste = Instantiate(prefabTiroInimigo, spawnPointNordeste.transform.position, spawnPointNordeste.transform.rotation);
         Destroy(tiroNordeste, 5);
         GameObject tiroSudeste = Instantiate(prefabTiroInimigo, spawnPointSudeste.transform.position, spawnPointSudeste.transform.rotation);
         Destroy(tiroSudeste, 5);
         GameObject tiroSudoeste = Instantiate(prefabTiroInimigo, spawnPointSudoeste.transform.position, spawnPointSudoeste.transform.rotation);
         Destroy(tiroSudoeste, 5);
         GameObject tiroNoroeste = Instantiate(prefabTiroInimigo, spawnPointNoroeste.transform.position, spawnPointNoroeste.transform.rotation);
         Destroy(tiroNoroeste, 5);
     }*/

}
