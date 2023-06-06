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
    private bool atirarMissilAtivo = true;
    private float tempoAtirarConfusao = 1.0f;
    private float tempoAtirarDiagonal = 1.5f;
    private float tempoAtirarMissil = 2.5f;
    private float timerAtirarConfusao = 0f;
    private float timerAtirarDiagonal = 0f;
    private float timerAtirarMissil = 0f;
    public GameObject prefabTiroInimigo;
    public GameObject prefabTiroInimigoMissil;
    public GameObject prefabTiroInimigoConfusao;
    [Header("Tiros padroes retos")]
    public GameObject spawnPointCima, spawnPointDireita, spawnPointBaixo, spawnPointEsquerda;
    [Header("Tiros padroes diagonais")]
    public GameObject spawnPointNordeste, spawnPointSudeste, spawnPointSudoeste, spawnPointNoroeste;
    public bool ativarTiro;
    public bool ativarTiroConfusao = false;
    public bool ativarTiroDiagonal = true;
    public bool ativarTiroMissil = false;
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

    void Update()
    {
        if (bossScript.vida <= 80 && bossScript.vida > 60)
        {
            Debug.Log("Atirar");
            ativarTiroConfusao = true;
            ativarTiroDiagonal = false;
        }

        if (bossScript.vida <= 60 && bossScript.vida > 40)
        {
            Debug.Log("Fase 2");
            ativarTiroMissil = true;

        }

        if (ativarTiroConfusao)
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

        if (ativarTiroDiagonal)
        {
            if (atirarDiagonalAtivo)
            {
                timerAtirarDiagonal += Time.deltaTime;

                if (timerAtirarDiagonal >= tempoAtirarDiagonal)
                {
                    StartCoroutine(ExecutarAtaquesDiagonais());
                    timerAtirarDiagonal = 0f;
                }
            }
        }

        //quebra

        if (ativarTiroMissil)
        {
            if (atirarMissilAtivo)
            {
                timerAtirarMissil += Time.deltaTime;

                if (timerAtirarMissil >= tempoAtirarMissil)
                {
                    StartCoroutine(atirarMissil());
                    timerAtirarMissil = 0f;
                }
            }
        }
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

    IEnumerator atirarMissil()
    {
        GameObject tiroNordeste = Instantiate(prefabTiroInimigoMissil, spawnPointNordeste.transform.position, spawnPointNordeste.transform.rotation);
        Destroy(tiroNordeste, 5);
        GameObject tiroSudeste = Instantiate(prefabTiroInimigoMissil, spawnPointSudeste.transform.position, spawnPointSudeste.transform.rotation);
        Destroy(tiroSudeste, 5);

        yield return null;
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
     IEnumerator ExecutarAtaquesDiagonais()
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
     }

}
