using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InimigoShotgun : MonoBehaviour
{

    [SerializeField] public Transform Player; // referência para o objeto do jogador
    public float moveSpeed = 5f; // velocidade de movimento do inimigo

    public int recompensaPontos; // pontos que o jogador recebe por matar este inimigo
    public AudioSource somMorte; // Som que sera executado
    public WaveManager waveManager;// script que faz parte do WaveManager
    public GameObject MorteFx;

    [SerializeField] private AudioSource SomMorte;

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

    [Header("Drop do inimigo")]
    public GameObject itemVida;

    [SerializeField]
    [Range(0, 100)]
    private float chanceItemVida;
    [SerializeField] private ItemDeCura itemvidaprefab;

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

        // Cálculo da direção para o jogador
        Vector3 direction = Player.position - transform.position;
        direction.Normalize();

        // Cálculo do ângulo de rotação em relação ao jogador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Aplica a rotação e movimento
        transform.rotation = rotation;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "tiroPlayer" || other.transform.tag == "Player")
        {
            Destroy(other.gameObject);
            vida = vida - 1;

            // Muda a textura do inimigo para o material de dano quando tomar dano
            meshRenderer.material = materialDano;
            // Vai executar algo depois que o TempoTexturaDanoPassar
            StartCoroutine(ResetMaterial());

            if (vida <= 0)
            {
                // Destrói esse gameObject quando a vida dele chegar em 0
                SoltarItemVida();
                Destroy(this.gameObject);
                GameManager.instancia.adicionarPontos(recompensaPontos);
                AudioManager.instancia.TocarSomMorte();
                AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.explosaoSFX, 0.5f);

                if (waveManager != null)
                {
                    waveManager.EnemyDefeated();
                }
            }
            if (other.CompareTag("Player"))

                if (other.transform.tag == "Player")
                {
                    GameManager.instancia.vidaAtual = 0;
                }


        }
        else if (other.transform.tag == "tiroEspecialPlayer")
        {
            Destroy(other.gameObject);
            vida = vida - 2;

            // Muda a textura do inimigo para o material de dano quando tomar dano
            meshRenderer.material = materialDano;
            // Vai executar algo depois que o TempoTexturaDanoPassar
            StartCoroutine(ResetMaterial());

            if (vida <= 0)
            {
                // Destrói esse gameObject quando a vida dele chegar em 0
                SoltarItemVida();
                SomMorte.Play();
                Destroy(this.gameObject);

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

    private void SoltarItemVida()
    {
        float chanceAleatoria = Random.Range(0f, 100f);
        if (chanceAleatoria <= this.chanceItemVida)
        {
            //soltar o Item Vida
            Instantiate(this.itemvidaprefab, this.transform.position, Quaternion.identity);
        }

    }

}