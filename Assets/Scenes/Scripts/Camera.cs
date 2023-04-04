using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // Transform do objeto que a c�mera deve seguir
    public float smoothSpeed = 100f; // Velocidade de movimento da c�mera

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0, 2, -30); // Define a posi��o desejada da c�mera (no exemplo, a c�mera fica 2 unidades acima e 5 unidades atr�s do objeto)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suaviza o movimento da c�mera
        transform.position = smoothedPosition; // Move a c�mera para a nova posi��o
    }
}
