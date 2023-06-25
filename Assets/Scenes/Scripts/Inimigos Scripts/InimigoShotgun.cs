using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cinemachine;

public class InimigoShotgun : MonoBehaviour
{

    [SerializeField] public Transform Player; // refer�ncia para o objeto do jogador
    public float moveSpeed = 5f; // velocidade de movimento do inimigo

    public int recompensaPontos; // pontos que o jogador recebe por matar este inimigo
    public AudioSource somMorte; // Som que sera executado
    public WaveManager waveManager;// script que faz parte do WaveManager
    public GameObject MorteFx;
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    public GameObject particlePrefab;
    public CinemachineImpulseSource source;

    [SerializeField] private AudioSource SomMorte;

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

    [Header("Drop do inimigo")]
    public GameObject itemVida;

    [SerializeField]
    [Range(0, 100)]
    private float chanceItemVida;
    [SerializeField] private ItemDeCura itemvidaprefab;

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

        // Calcula a distância atual entre o inimigo e o jogador
        float currentDistance = Vector3.Distance(transform.position, Player.position);

        // Verifica se a distância atual é menor que a distância mínima desejada
        if (currentDistance < distMinimaEntreInimigos)
        {
            // Calcula a direção oposta à direção para o jogador
            Vector3 avoidanceDirection = -playerDirection;

            // Suaviza a direção de desvio usando um algoritmo de interpolação
            Vector3 smoothDirection = Vector3.Lerp(transform.up, avoidanceDirection, Time.deltaTime * 5f);

            // Cálculo do ângulo de rotação em relação à direção suavizada
            float angle = Mathf.Atan2(smoothDirection.y, smoothDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Aplica a rotação e movimento
            transform.rotation = rotation;
            transform.position += smoothDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
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
        if (other.transform.tag == "tiroPlayer" )
        {
            Destroy(other.gameObject);
            vida = vida - 1;

            // Muda a textura do inimigo para o material de dano quando tomar dano
            meshRenderer.material = materialDano;
            // Vai executar algo depois que o TempoTexturaDanoPassar
            StartCoroutine(ResetMaterial());

            if (vida <= 0)
            {
                // Destr�i esse gameObject quando a vida dele chegar em 0
                SoltarItemVida();
                Destroy(this.gameObject);
                Instantiate(particlePrefab, transform.position, transform.rotation);
                GameManager.instancia.adicionarPontos(recompensaPontos);
                AudioManager.instancia.TocarSomMorte();
                AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.explosaoSFX, 0.5f);
                AudioManager.instancia.TocarSomMorte();
                source.GenerateImpulse();


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
                // Destr�i esse gameObject quando a vida dele chegar em 0
                SoltarItemVida();
                Destroy(this.gameObject);
                Instantiate(particlePrefab, transform.position, transform.rotation);
                GameManager.instancia.adicionarPontos(recompensaPontos);
                AudioManager.instancia.TocarSomMorte();
                AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.explosaoSFX, 0.5f);
                AudioManager.instancia.TocarSomMorte();
                source.GenerateImpulse();

            }
        }
    }

    private IEnumerator ResetMaterial()
    {
        // Vai executar depois que o tempo de dura��o do dano passar
        yield return new WaitForSeconds(tempoTexturaDano);
        // Depois desse tempo a� de cima passar o material do inimigo vai voltar pro base   
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

    void OnDestroy()
    {
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }

}