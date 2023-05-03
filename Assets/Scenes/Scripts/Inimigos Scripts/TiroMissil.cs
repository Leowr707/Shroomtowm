using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroMissil : MonoBehaviour
{
    public float forca;
    public float distanciaMaxima = 30f;
    private float distanciaPercorrida = 0f;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {

        if (!GetComponent<Rigidbody>())
        {
            Destroy(this);
        }
        else { 

        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * forca * Time.deltaTime;
        }
    }

    private void Update()
    {
        float distanciaPercorridaAtual = distanciaPercorrida + GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime;
        if (distanciaPercorridaAtual < distanciaMaxima)
        {
            distanciaPercorrida = distanciaPercorridaAtual;
        }
        else
        {
            Destroy(GetComponent<Rigidbody>());
           
        }
        if (!GetComponent<Rigidbody>())
        {
            
        }
    }
}
