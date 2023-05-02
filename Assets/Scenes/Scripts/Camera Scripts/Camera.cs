using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // Transform do objeto que a camera deve seguir
    public float smoothSpeed = 100f; // Velocidade de movimento da camera
    

    void LateUpdate()
    {
        Vector3 desiredPosition = target && !Object.Equals(target, null) ? target.position + new Vector3(0, 2, -30) : transform.position;
        // Vector3 desiredPosition = target.position + new Vector3(0, 2, -30); // Define a posicao desejada da camera (no exemplo, a camera fica 2 unidades acima e 5 unidades atras do objeto)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suaviza o movimento da camera
        transform.position = smoothedPosition; // Move a camera para a nova posicao

        //if (target == null) return;
    }
}
