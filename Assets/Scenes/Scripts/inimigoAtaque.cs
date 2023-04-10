using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoAtaque : MonoBehaviour
{
    [Header("Configurações básicas do inimigo")]
    public int vida = 3;

    [Header("Configuração de textura")]
    //material da nave quando ele tomar dano
    public Material materialDano;
    //material original da nave
    public Material materialOriginal;
    //isso aqui que aplica o material no inimigo 
    MeshRenderer meshRenderer;
    //quanto tempo a textura de dano ficará na tela
    public float tempoTexturaDano;

    [Header("Configurações do disparo do inimigo")]
    public GameObject prefabTiroInimigo;
    public GameObject spawnPointDoTiroInimigo;
    public bool ativarTiro;

    [Header("Drop do inimigo")]
    public GameObject itemVida;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialOriginal = meshRenderer.material;

        if (ativarTiro)
        {
            //o tempo que levará para ativar o código e de quando em quanto tempo ele vai se repetir
            InvokeRepeating("Atirar", 2, 0.5f);
        }

    }

    private IEnumerator OnCollisionEnter(Collision objetoColidido)
    {
        if(objetoColidido.transform.tag == "tiroPlayer" || objetoColidido.transform.tag == "Player")
        {
            Destroy(objetoColidido.gameObject);

            vida = vida - 1;
            //muda a textura do inimigo pro material de dano quando tomar dano
            meshRenderer.material = materialDano;
            //vai executar algo depois que o TempoTexturaDanoPassar
            yield return new WaitForSeconds(tempoTexturaDano);
            //depois desse tempo aí de cima passar o material do inimigo vai voltar pro base   
            meshRenderer.material = materialOriginal;

            if(vida <= 0)
            {
                /* ESSE CÓDIGO AQUI É A BASE PRO SISTEMA DE SCORE DESSE INIMIGO :)
                  int auxPontos = int.Parse(valorPontos.text)
                      auxPontos = auxPontos + 200;
                  valorPontos.text = auxPontos.ToString();
                */

                //destruirá esse gameObject quando a vida dele chegar em 0
                Destroy(this.gameObject);
            
            }
        }

        if (objetoColidido.transform.tag == "tiroEspecialPlayer")
        {
            Destroy(objetoColidido.gameObject);

            vida = vida - 2;
            //muda a textura do inimigo pro material de dano quando tomar dano
            meshRenderer.material = materialDano;
            //vai executar algo depois que o TempoTexturaDanoPassar
            yield return new WaitForSeconds(tempoTexturaDano);
            //depois desse tempo aí de cima passar o material do inimigo vai voltar pro base   
            meshRenderer.material = materialOriginal;

            if (vida <= 0)
            {
                
                Destroy(this.gameObject);
                Instantiate(itemVida);

            }

        }
       

    }
    void Atirar()
    {
        Instantiate(prefabTiroInimigo, spawnPointDoTiroInimigo.transform.position, spawnPointDoTiroInimigo.transform.rotation);
        Destroy(prefabTiroInimigo, 5);
    }
}

