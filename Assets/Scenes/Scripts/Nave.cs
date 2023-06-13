using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Nave : MonoBehaviour
{
    public static Nave instancia;
    [Header("Configuracoes basicas do player")]
    public float moveSpeed = 5f; // velocidade de movimento da nave
    public float rotateSpeed = 100f; // velocidade de rota��o da nave
    public float maxSpeed = 20f; // velocidade m�xima da nave
    public float impulseForce = 10f; // for�a de impulso aplicada � nave
    public float yMin;
    public float yMax;
    public float xMin;
    public float xMax;

    public Text VidaText;

    private IEnumerator vidatxt()
    {       
            VidaText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            VidaText.gameObject.SetActive(false);
    }
    // método para recuperar vida
    public void RecuperarVida(int valorRecuperado)
    {
        if (GameManager.instancia.vidaAtual + valorRecuperado >= GameManager.instancia.vidaMaxima)
        {
            GameManager.instancia.vidaAtual = GameManager.instancia.vidaMaxima;
        }else{
            GameManager.instancia.vidaAtual = GameManager.instancia.vidaAtual + valorRecuperado;
        }
        Debug.Log("Vida recuperada! Vida atual: " + GameManager.instancia.vidaAtual);
    }

    [Header("Configura��o de textura")]
    //material da nave quando ele tomar dano
    public Material materialDano;
    //material da nave quando ele toma tiro do inimigo de confusao
    public Material materialDanoConfusao;
    //material original da nave
    public Material materialOriginal;
    //isso aqui que aplica o material no inimigo 
    MeshRenderer meshRenderer;
    //quanto tempo a textura de dano ficar� na tela
    public float tempoTexturaDano;
    //quanto tempo a textura de dano ficar� na tela
    public float tempoTexturaConfusao;
    public bool confuso;

    [Header("Configuracoes de tipo e spawn")]
    public GameObject projetil; //onde coloca o projetil
    public Transform spawnPoint; //onde o tiro vai spawnar
    public Transform spawnPointMeioBaixo; //onde o tiro vai spawnar
    public Transform spawnPointBaixo; //onde o tiro vai spawnar
    public Transform spawnPointMeioCima; //onde o tiro vai spawnar
    public Transform spawnPointCima; //onde o tiro vai spawnar

    public GameObject tiroEspecial;

    [Header("Configuracoes do tiro normal")]
    public float intervaloTiro = 1;
    float tiroInicial, proximoTiro;
    public int tempoDestruicaoTiro = 5;

    [Header("Configuracoes do tiro Especial")]
    public float intervaloTiroEspecial = 1;
    float tiroInicialEspecial, proximoTiroEspecial;
    public int tempoDestruicaoTiroEspecial = 5;

    private Rigidbody rb; // refer�ncia ao Rigidbody da nave

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // obt�m a refer�ncia ao Rigidbody da nave
        rb.mass = 1f; // define a massa do Rigidbody
        rb.drag = 0.5f; // define o coeficiente de arrasto do Rigidbody
        meshRenderer = GetComponent<MeshRenderer>(); // obt�m a refer�ncia ao MeshRenderer da nave

    }

    private void FixedUpdate()
    {
        Atirar();
        AtirarEspecial();

        if (confuso == false) { 
        float horizontal = Input.GetAxis("Horizontal"); // recebe a entrada dos botoes A e D
        float vertical = Input.GetAxis("Vertical"); // recebe a entrada do botao W
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax),
            0
            );
        
        // gira a nave em torno do eixo Z baseado na entrada horizontal
        transform.Rotate(0f, 0f, -horizontal * rotateSpeed * Time.deltaTime);

        // verifica se a tecla W est� sendo pressionada continuamente
        if (Input.GetKey(KeyCode.W))
        {
            rb.useGravity = false; // desativa a gravidade
        }
        else
        {
            rb.useGravity = true; // ativa a gravidade
        }
        
        // aplica for�a para mover a nave para cima baseado na entrada vertical
        Vector3 upForce = transform.up * vertical * impulseForce;
        rb.AddForce(upForce);

        // limita a velocidade da nave
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

       
        {
            if (confuso == true)
            {
                float horizontal = -Input.GetAxis("Horizontal"); // recebe a entrada dos botoes A e D
                float vertical = Input.GetAxis("Vertical"); // recebe a entrada do botao W

                // gira a nave em torno do eixo Z baseado na entrada horizontal
                transform.Rotate(0f, 0f, -horizontal * rotateSpeed * Time.deltaTime);

                // verifica se a tecla W est� sendo pressionada continuamente
                if (Input.GetKey(KeyCode.W))
                {
                    rb.useGravity = false; // desativa a gravidade
                }
                else
                {
                    rb.useGravity = true; // ativa a gravidade
                }

                // aplica for�a para mover a nave para cima baseado na entrada vertical
                Vector3 upForce = transform.up * vertical * impulseForce;
                rb.AddForce(upForce);

                // limita a velocidade da nave
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Boss")
        {

            GameManager.instancia.RemoverVida(10);
        }

        if (other.transform.tag == "tiroInimigo")
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            GameManager.instancia.RemoverVida(1);
            if (confuso == false)
            {
                // Muda a textura do inimigo para o material de dano quando tomar dano
                meshRenderer.material = materialDano;
                // Vai executar algo depois que o TempoTexturaDanoPassar
                StartCoroutine(ResetMaterial());
            }

            
        }


        if (other.transform.tag == "tiroInimigoMissil")
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            GameManager.instancia.RemoverVida(1);

            if (confuso == false)
            {
                // Muda a textura do inimigo para o material de dano quando tomar dano
                meshRenderer.material = materialDano;
                // Vai executar algo depois que o TempoTexturaDanoPassar
                StartCoroutine(ResetMaterial());
            }
        }
        
        if (other.transform.tag == "tiroInimigoConfusao")
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);


            if (confuso == false)
            {
                confuso = true;
                // Muda a textura do inimigo para o material de confusao quando tomar o tiro
                meshRenderer.material = materialDanoConfusao;
                // Vai executar algo depois que o TempoTexturaDanoPassar
                StartCoroutine(ResetMaterialConfusao());
            }

            else { 
                GameManager.instancia.RemoverVida(2); 
            }

        }
        if(other.CompareTag("itemDeCura"))
        {
            StartCoroutine(vidatxt());
        }
    }
    private IEnumerator ResetMaterial()
    {
        // Vai executar depois que o tempo de dura��o do dano passar
        yield return new WaitForSeconds(tempoTexturaDano);
        // Depois desse tempo a� de cima passar o material do inimigo vai voltar pro base   
        meshRenderer.material = materialOriginal;
       
    }

    private IEnumerator ResetMaterialConfusao()
    {
        // Vai executar depois que o tempo de duracao do dano passar
        yield return new WaitForSeconds(tempoTexturaConfusao);
        // Depois desse tempo a� de cima passar o material do inimigo vai voltar pro base   
        meshRenderer.material = materialOriginal;
        confuso = false;
    }


    void Atirar()
    {
        // Verifica se o jogador apertou a tecla de espaco
        // Se apertou, vai criar o tiro
        tiroInicial = tiroInicial + Time.deltaTime;

        if (Input.GetKey(KeyCode.J) && tiroInicial > proximoTiro)
        {
           

            proximoTiro = tiroInicial + intervaloTiro;

            // Vai criar na tela um projetil, em uma determinada posicao, com uma rotacao
            // spawnPoint.position -> Representa o ponto onde o projetil vai ser criado
            // spawnPoint.rotation -> Rotacao que o projetil criado vai possuir
            GameObject tiro = Instantiate(projetil, spawnPoint.position, spawnPoint.transform.rotation);
            //obj.GetComponent<bullet>();

            proximoTiro = proximoTiro - tiroInicial;
            tiroInicial = 0.0f;
            AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.tiroJogadorSFX, 0.1f);
            Destroy(tiro, tempoDestruicaoTiro);
        }
    }

    void AtirarEspecial()
    {
        // Verifica se o jogador apertou a tecla de espaco
        // Se apertou, vai criar o tiro
        tiroInicialEspecial = tiroInicialEspecial + Time.deltaTime;

        if (Input.GetKey(KeyCode.K) && tiroInicialEspecial > proximoTiroEspecial)
        {
            

            proximoTiroEspecial = tiroInicialEspecial + intervaloTiroEspecial;

            // Vai criar na tela um projetil, em uma determinada posicao, com uma rotacao
            // spawnPoint.position -> Representa o ponto onde o projetil vai ser criado
            // spawnPoint.rotation -> Rotacao que o projetil criado vai possuir
            GameObject TiroEspecial = Instantiate(tiroEspecial, spawnPoint.position, spawnPoint.transform.rotation);
            GameObject TiroEspecialMeioCima = Instantiate(tiroEspecial, spawnPointMeioCima.position, spawnPointMeioCima.transform.rotation);
            GameObject TiroEspecialMeioBaixo = Instantiate(tiroEspecial, spawnPointMeioBaixo.position, spawnPointMeioBaixo.transform.rotation);
            GameObject TiroEspecialCima = Instantiate(tiroEspecial, spawnPointCima.position, spawnPointCima.transform.rotation);
            GameObject TiroEspecialBaixo = Instantiate(tiroEspecial, spawnPointBaixo.position, spawnPointBaixo.transform.rotation);

            proximoTiroEspecial = proximoTiroEspecial - tiroInicialEspecial;
            tiroInicialEspecial = 0.0f;
            Destroy(TiroEspecial, tempoDestruicaoTiroEspecial);
        }
    }
        
}
    
    

