using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;

public class InimigoConfusao : MonoBehaviour
{

    public Transform Player; // refer�ncia para o objeto do jogador
    public float moveSpeed = 5f; // velocidade de movimento do inimigo
    public int recompensaPontos; // pontos que o jogador recebe por matar este inimigo
    [SerializeField] private AudioSource SomMorte;
    public CinemachineImpulseSource source;
    public GameObject particlePrefab;
    
    [Header("Configuracoes basicas do inimigo")]
    public int vida = 3;
    public float alcance = 30f;

    [Header("Configuracao de textura")]
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

    [SerializeField]
    private ItemDeCura itemvidaprefab;

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

        // C�lculo da distancia entre o inimigo e o jogador
        float distance = Vector3.Distance(transform.position, Player.position);

        // Verifica se a distancia eh menor ou igual a 30
        if (distance <= alcance)
        {
            // Define a direcao para o jogador
            Vector3 direction = Player.position - transform.position;
            direction.Normalize();

            // Calculo do angulo de rotacao em relacao ao jogador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Aplica a rotacao
            transform.rotation = rotation;

            // Desativa o movimento
            return;
        }

        // C�lculo da direcao para o jogador
        Vector3 moveDirection = Player.position - transform.position;
        moveDirection.Normalize();

        // C�lculo do �ngulo de rotacao em relacao ao jogador
        float moveAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion moveRotation = Quaternion.AngleAxis(moveAngle, Vector3.forward);

        // Aplica a rotacao e movimento
        transform.rotation = moveRotation;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "tiroPlayer")
        {
            Destroy(other.gameObject);
            vida = vida - 1;

            // Muda a textura do inimigo para o material de dano quando tomar dano
            meshRenderer.material = materialDano;
            // Vai executar algo depois que o TempoTexturaDanoPassar
            StartCoroutine(ResetMaterial());

            if (vida <= 0)
            {
                // Destroi esse gameObject quando a vida dele chegar em 0
                SoltarItemVida();
                Destroy(this.gameObject);
                source.GenerateImpulse();
                Instantiate(particlePrefab, transform.position, transform.rotation);
                //GameManager.instancia.InimigosConfusaoMortos(1);
                AudioManager.instancia.TocarSomMorte();
                GameManager.instancia.adicionarPontos(recompensaPontos);
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
                // Destr�i esse gameObject quando a vida dele chegar em 0
                SoltarItemVida();
                Destroy(this.gameObject);
                //GameManager.instancia.InimigosConfusaoMortos(1);
                Instantiate(particlePrefab, transform.position, transform.rotation);
                AudioManager.instancia.TocarSomMorte();
                GameManager.instancia.adicionarPontos(recompensaPontos);
                AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.explosaoSFX, 0.5f);

            }
        }
    }

    private IEnumerator ResetMaterial()
    {
        // Vai executar depois que o tempo de duracao do dano passar
        yield return new WaitForSeconds(tempoTexturaDano);
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