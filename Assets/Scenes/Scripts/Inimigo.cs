using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionDistance = 1f;
    public float collisionSpeed = 10f;

    private Transform playerTransform;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player nao encontrado!");
        }
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        // Movimentacao suave nos eixos X e Y
        Vector2 movement = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(movement.x, movement.y, 0f);

        // Colisor com o jogador
        if (Vector3.Distance(transform.position, playerTransform.position) < collisionDistance)
        {
            Vector3 collisionDirection = playerTransform.position - transform.position;
            collisionDirection.Normalize();
            transform.position -= collisionDirection * collisionSpeed * Time.deltaTime;
        }
    }
}