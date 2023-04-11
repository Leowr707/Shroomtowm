using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [Header("Configurações básicas do player")]
    public float moveSpeed = 5f; // velocidade de movimento da nave
    public float rotateSpeed = 100f; // velocidade de rotação da nave
    public float maxSpeed = 20f; // velocidade máxima da nave
    public float impulseForce = 10f; // força de impulso aplicada à nave
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

    [Header("Configurações de tipo e spawn")]
    public GameObject projetil; //onde coloca o projetil
    public Transform spawnPoint; //onde o tiro vai spawnar
    public GameObject tiroEspecial;

    [Header("Configurações do tiro normal")]
    public float intervaloTiro = 1;
    float tiroInicial, proximoTiro;
    public int tempoDestruicaoTiro = 5;

    [Header("Configurações do tiro Especial")]
    public float intervaloTiroEspecial = 1;
    float tiroInicialEspecial, proximoTiroEspecial;
    public int tempoDestruicaoTiroEspecial = 5;

    private Rigidbody rb; // referência ao Rigidbody da nave

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // obtém a referência ao Rigidbody da nave
        rb.mass = 1f; // define a massa do Rigidbody
        rb.drag = 0.5f; // define o coeficiente de arrasto do Rigidbody
        meshRenderer = GetComponent<MeshRenderer>(); // obtém a referência ao MeshRenderer da nave
    }

    private void FixedUpdate()
    {
        Atirar();
        AtirarEspecial();

        float horizontal = Input.GetAxis("Horizontal"); // recebe a entrada dos botões A e D
        float vertical = Input.GetAxis("Vertical"); // recebe a entrada do botão W

        // gira a nave em torno do eixo Z baseado na entrada horizontal
        transform.Rotate(0f, 0f, -horizontal * rotateSpeed * Time.deltaTime);

        // verifica se a tecla W está sendo pressionada continuamente
        if (Input.GetKey(KeyCode.W))
        {
            rb.useGravity = false; // desativa a gravidade
        }
        else
        {
            rb.useGravity = true; // ativa a gravidade
        }

        // aplica força para mover a nave para cima baseado na entrada vertical
        Vector3 upForce = transform.up * vertical * impulseForce;
        rb.AddForce(upForce);

        // limita a velocidade da nave
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    /* private IEnumerator OnCollisionEnter(Collision objetoColidido)

     {
         if (objetoColidido.transform.tag == "tiroInimigo")
         {
             Destroy(objetoColidido.gameObject);

             vida = vida - 1;
             //muda a textura do inimigo pro material de dano quando tomar dano
             meshRenderer.material = materialDano;
             //vai executar algo depois que o TempoTexturaDanoPassar
             yield return new WaitForSeconds(tempoTexturaDano);
             //depois desse tempo aí de cima passar o material do inimigo vai voltar pro base   
             meshRenderer.material = materialOriginal;


         }

         if (vida <= 0)
         {
             /* ESSE CÓDIGO AQUI É A BASE PRO SISTEMA DE SCORE DESSE INIMIGO :)
               int auxPontos = int.Parse(valorPontos.text)
                   auxPontos = auxPontos + 200;
               valorPontos.text = auxPontos.ToString();
             */

    //destruirá esse gameObject quando a vida dele chegar em 0

    //Destroy(playerClone.gameObject);

    //Destroy(this.gameObject);        
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "tiroInimigo")
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


    void Atirar()
    {
        // Verifica se o jogador apertou a tecla de espaço
        // Se apertou, vai criar o tiro
        tiroInicial = tiroInicial + Time.deltaTime;

        if (Input.GetButton("Jump") && tiroInicial > proximoTiro)
        {
           

            proximoTiro = tiroInicial + intervaloTiro;

            // Vai criar na tela um projetil, em uma determinada posição, com uma rotação
            // spawnPoint.position -> Representa o ponto onde o projetil vai ser criado
            // spawnPoint.rotation -> Rotação que o projetil criado vai possuir
            GameObject tiro = Instantiate(projetil, spawnPoint.position, spawnPoint.transform.rotation);
            //obj.GetComponent<bullet>();

            proximoTiro = proximoTiro - tiroInicial;
            tiroInicial = 0.0f;
            Destroy(tiro, tempoDestruicaoTiro);
        }
    }

    void AtirarEspecial()
    {
        // Verifica se o jogador apertou a tecla de espaço
        // Se apertou, vai criar o tiro
        tiroInicialEspecial = tiroInicialEspecial + Time.deltaTime;

        if (Input.GetButton("Fire1") && tiroInicialEspecial > proximoTiroEspecial)
        {

            proximoTiroEspecial = tiroInicialEspecial + intervaloTiroEspecial;

            // Vai criar na tela um projetil, em uma determinada posição, com uma rotação
            // spawnPoint.position -> Representa o ponto onde o projetil vai ser criado
            // spawnPoint.rotation -> Rotação que o projetil criado vai possuir
            GameObject TiroEspecial = Instantiate(tiroEspecial, spawnPoint.position, spawnPoint.transform.rotation);

            proximoTiroEspecial = proximoTiroEspecial - tiroInicialEspecial;
            tiroInicialEspecial = 0.0f;
            Destroy(TiroEspecial, tempoDestruicaoTiroEspecial);
        }
    }
}