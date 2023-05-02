using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InimigoConfusao : MonoBehaviour
{

    public Transform Player; // refer�ncia para o objeto do jogador
    public float moveSpeed = 5f; // velocidade de movimento do inimigo
    public int recompensaPontos; // pontos que o jogador recebe por matar este inimigo
    
    [Header("Configura��es b�sicas do inimigo")]
    public int vida = 3;
    public float alcance = 30f;

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

    [SerializeField]
    private ItemDeCura itemvidaprefab;

    // Start is called before the first frame update
    void Start()
    {
        
        meshRenderer = GetComponent<MeshRenderer>();
        materialOriginal = meshRenderer.material;
    }

    void Update()
    {
        // Verifica se o objeto do jogador foi definido
        if (Player == null) return;

        // C�lculo da dist�ncia entre o inimigo e o jogador
        float distance = Vector3.Distance(transform.position, Player.position);

        // Verifica se a dist�ncia � menor ou igual a 30
        if (distance <= alcance)
        {
            // Define a dire��o para o jogador
            Vector3 direction = Player.position - transform.position;
            direction.Normalize();

            // C�lculo do �ngulo de rota��o em rela��o ao jogador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Aplica a rota��o
            transform.rotation = rotation;

            // Desativa o movimento
            return;
        }

        // C�lculo da dire��o para o jogador
        Vector3 moveDirection = Player.position - transform.position;
        moveDirection.Normalize();

        // C�lculo do �ngulo de rota��o em rela��o ao jogador
        float moveAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion moveRotation = Quaternion.AngleAxis(moveAngle, Vector3.forward);

        // Aplica a rota��o e movimento
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
                AudioManager.instancia.TocarSomMorte();
                GameManager.instancia.adicionarPontos(recompensaPontos);


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

}