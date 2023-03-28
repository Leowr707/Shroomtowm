using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    public float moveSpeed = 5f; // velocidade de movimento da nave
    public float rotateSpeed = 100f; // velocidade de rota��o da nave
    public float maxSpeed = 20f; // velocidade m�xima da nave
    public float impulseForce = 10f; // for�a de impulso aplicada � nave

    private Rigidbody rb; // refer�ncia ao Rigidbody da nave

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // obt�m a refer�ncia ao Rigidbody da nave
        rb.mass = 1f; // define a massa do Rigidbody
        rb.drag = 0.5f; // define o coeficiente de arrasto do Rigidbody
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // recebe a entrada dos bot�es A e D
        float vertical = Input.GetAxis("Vertical"); // recebe a entrada do bot�o W

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