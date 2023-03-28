using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // Transform do objeto que a câmera deve seguir
    public float smoothSpeed = 0.125f; // Velocidade de movimento da câmera

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0, 2, -30); // Define a posição desejada da câmera (no exemplo, a câmera fica 2 unidades acima e 5 unidades atrás do objeto)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suaviza o movimento da câmera
        transform.position = smoothedPosition; // Move a câmera para a nova posição
    }
}
