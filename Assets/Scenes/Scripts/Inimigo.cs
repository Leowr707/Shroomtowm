using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public Transform Player; // referência para o objeto do jogador
    public float moveSpeed = 5f; // velocidade de movimento do inimigo

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
}