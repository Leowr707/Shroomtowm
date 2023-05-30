using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;

public class Boss : MonoBehaviour
{
    private WaveManager gerenciadorDeOndas;
    [SerializeField] public Transform Player; // referência para o objeto do jogador
    public float moveSpeed = 5f; // velocidade de movimento do inimigo
    public int recompensaPontos; // pontos que o jogador recebe por matar este inimigo
    public CinemachineImpulseSource source;
    [SerializeField] private AudioSource SomMorte;

    [Header("Configurações básicas do inimigo")]
    public int vida = 3;
    public GameObject particlePrefab;

    [Header("Configuração de textura")]
    //material da nave quando ele tomar dano
    public Material materialDano;
    //material original da nave
    public Material materialOriginal;
    //isso aqui que aplica o material no inimigo 
    MeshRenderer meshRenderer;
    //quanto tempo a textura de dano ficará na tela
    public float tempoTexturaDano;

    

  

    // Start is called before the first frame update
    void Start()
    {


        Player = GameObject.FindWithTag("Player").transform;
        SomMorte = GameObject.Find("MorteInimigo").GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        materialOriginal = meshRenderer.material;


    }

    void Update()
    {
        // Verifica se o objeto do jogador foi definido
        if (Player == null) return;

        // C�lculo da dire��o para o jogador
        Vector3 direction = Player.position - transform.position;
        direction.Normalize();
      
        transform.position += direction * moveSpeed * Time.deltaTime;

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            GameManager.instancia.RemoverVida(10);
        }

            if (other.transform.tag == "tiroPlayer" /*|| other.transform.tag == "Player"*/)
        {
            Destroy(other.gameObject);
            vida = vida - 1;

            // Muda a textura do inimigo para o material de dano quando tomar dano
            meshRenderer.material = materialDano;
            // Vai executar algo depois que o TempoTexturaDanoPassar
            StartCoroutine(ResetMaterial());

            if (vida <= 0)
            {
               
                Instantiate(particlePrefab, transform.position, transform.rotation);
                source.GenerateImpulse();
                Destroy(this.gameObject);
                GameManager.instancia.adicionarPontos(recompensaPontos);
                AudioManager.instancia.TocarSomMorte();
                AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.explosaoSFX, 0.5f);

              
            }
            if (other.transform.tag == "Player")
            {
                GameManager.instancia.vidaAtual = 0;


            }







        }
        else if (other.transform.tag == "tiroEspecialPlayer")
        {
            Destroy(other.gameObject);
            vida = vida - 3;

            // Muda a textura do inimigo para o material de dano quando tomar dano
            meshRenderer.material = materialDano;
            // Vai executar algo depois que o TempoTexturaDanoPassar
            StartCoroutine(ResetMaterial());

            if (vida <= 0)
            {
                
                Instantiate(particlePrefab, transform.position, transform.rotation);
                source.GenerateImpulse();
                Destroy(this.gameObject);
                GameManager.instancia.InimigosBaseMortos(1);
                GameManager.instancia.adicionarPontos(recompensaPontos);
                AudioManager.instancia.TocarSomMorte();
                AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.explosaoSFX, 0.5f);

            }
        }
    }

    private IEnumerator ResetMaterial()
    {
        // Vai executar depois que o tempo de duração do dano passar
        yield return new WaitForSeconds(tempoTexturaDano);
        // Depois desse tempo aí de cima passar o material do inimigo vai voltar pro base   
        meshRenderer.material = materialOriginal;
    }

   

    }

