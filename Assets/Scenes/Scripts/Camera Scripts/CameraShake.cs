using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
   {
     [SerializeField] private float shakeDuration = 0.1f; // duração do efeito de shake
    [SerializeField] private float shakeIntensity = 0.5f; // intensidade do efeito de shake

    private Vector3 originalPosition; // posição original da câmera
    private float shakeTimer; // contador de tempo para controlar a duração do shake

    private void Awake()
    {
        originalPosition = transform.position; // salva a posição original da câmera
    }

    private void Update()
    {
        // verifica se o efeito de shake está ativo
        if (shakeTimer > 0)
        {
            // move a câmera aleatoriamente dentro de um intervalo especificado
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            // diminui o contador de tempo
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // restaura a posição original da câmera
            transform.position = originalPosition;
        }
    }

    // método para acionar o efeito de shake
    public void Shake()
    {
        // define o contador de tempo para a duração do shake
        shakeTimer = shakeDuration;
    }
}

