using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroMissil : MonoBehaviour
{
    public float forca;
    public float forcaInicial;
    public float distanciaMaxima = 30f;
    public Transform Player;

    public float moveSpeed = 5f; // velocidade de movimento do inimigo

    // Start is called before the first frame update

    void Start()
    {

        Player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        // Verifica se o objeto do jogador foi definido
        if (Player == null) return;

        // Calculo da direcao para o jogador
        Vector3 direction = Player.position - transform.position;
        direction.Normalize();

        // Calculo do angulo de rotacao em relacao ao jogador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Aplica a rotacao e movimento
        transform.rotation = rotation;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
