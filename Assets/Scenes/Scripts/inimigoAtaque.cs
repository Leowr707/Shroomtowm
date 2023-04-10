using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoAtaque : MonoBehaviour
{
    [Header("Configura��es b�sicas do inimigo")]
    public int vida = 3;

    [Header("Configura��o de textura")]
    //material da nave quando ele tomar dano
    public Material materialDano;
    //material original da nave
    public Material materialOriginal;
    //isso aqui que aplica o material no inimigo 
    MeshRenderer meshRenderer;
    //quanto tempo a textura de dano ficar� na tela
    public float tempoTexturaDano;

    [Header("Configura��es do disparo do inimigo")]
    public GameObject prefabTiroInimigo;
    public GameObject spawnPointDoTiroInimigo;
    public bool ativarTiro;

    [Header("Drop do inimigo")]
    public GameObject itemVida;
    [Header("pontos Do Inimigo")]
    public int points = 10;
    public ScoreManager scoreManager;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialOriginal = meshRenderer.material;

        if (ativarTiro)
        {
            //o tempo que levar� para ativar o c�digo e de quando em quanto tempo ele vai se repetir
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
            //depois desse tempo a� de cima passar o material do inimigo vai voltar pro base   
            meshRenderer.material = materialOriginal;

            if(vida <= 0)
            {
                /* ESSE C�DIGO AQUI � A BASE PRO SISTEMA DE SCORE DESSE INIMIGO :)
                  int auxPontos = int.Parse(valorPontos.text)
                      auxPontos = auxPontos + 200;
                  valorPontos.text = auxPontos.ToString();
                */

                //destruir� esse gameObject quando a vida dele chegar em 0
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
            //depois desse tempo a� de cima passar o material do inimigo vai voltar pro base   
            meshRenderer.material = materialOriginal;

            if (vida <= 0)
            {
                
                Destroy(this.gameObject);

            }

        }
       

    }
    void Atirar()
    
    {
        Instantiate(prefabTiroInimigo, spawnPointDoTiroInimigo.transform.position, spawnPointDoTiroInimigo.transform.rotation);
        Destroy(prefabTiroInimigo, 5);
    }
    public GameObject healthItem; //referencia ao objeto de cura que será deixado cair

    void OnDestroy() {
    scoreManager.AddScore(points);
    Instantiate(healthItem, transform.position, transform.rotation); //cria uma cópia do objeto de cura na posição atual do inimigo que foi destruído
}



}

