using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;

public class Inimigo : MonoBehaviour
{
    private WaveManager gerenciadorDeOndas;
    [SerializeField]public Transform Player; // referência para o objeto do jogador
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

    [Header("Drop do inimigo")]
    public GameObject itemVida;

    [SerializeField][Range(0, 100)]
    private float chanceItemVida;
    [SerializeField]private ItemDeCura itemvidaprefab;

    [Header("Configurações de distância entre inimigos")]
    public float distMinimaEntreInimigos = 2f; // Distância mínima desejada entre os inimigos
    public float desvioForca = 1f; // Força do desvio aplicado para evitar colisões

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
        Vector3 playerDirection = Player.position - transform.position;
        playerDirection.Normalize();

        // Aplica o desvio suave para evitar colisões com outros inimigos
        Vector3 avoidanceDirection = AvoidOtherEnemies();
        Vector3 desiredDirection = playerDirection + avoidanceDirection;

        // Suaviza a direção desejada usando um algoritmo de interpolação
        Vector3 smoothDirection = Vector3.Lerp(transform.up, desiredDirection, Time.deltaTime * 5f);

        // Cálculo do ângulo de rotação em relação à direção suavizada
        float angle = Mathf.Atan2(smoothDirection.y, smoothDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Aplica a rotação e movimento
        transform.rotation = rotation;
        transform.position += smoothDirection * moveSpeed * Time.deltaTime;
    }

    private Vector3 AvoidOtherEnemies()
    {
        Vector3 avoidanceDirection = Vector3.zero;
        int enemyCount = 0;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Taginimigo");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy != this.gameObject)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < distMinimaEntreInimigos)
                {
                    Vector3 avoidance = transform.position - enemy.transform.position;
                    avoidanceDirection += avoidance.normalized / distance; // Peso inversamente proporcional à distância
                    enemyCount++;
                }
            }
        }

        if (enemyCount > 0)
        {
            avoidanceDirection /= enemyCount;
            avoidanceDirection.Normalize();
            avoidanceDirection *= desvioForca;
        }

        return avoidanceDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
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
                SoltarItemVida();
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

    private void SoltarItemVida() {
        float chanceAleatoria = Random.Range(0f, 100f);
        if(chanceAleatoria <= this.chanceItemVida) {
            //soltar o Item Vida
            Instantiate(this.itemvidaprefab, this.transform.position, Quaternion.identity);
        }

    }
    /*void OnDestroy()
    {
        waveManager.InimigoDestruido();
    }*/
    




}