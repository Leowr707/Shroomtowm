using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Tiro : MonoBehaviour
{
    public float forca;
    public CinemachineImpulseSource source;
    

    // Start is called before the first frame update
    void Start()
    {
        //em 5 segundos ap�s ser gerado, o tiro ser� destru�do
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * forca);
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * forca * Time.deltaTime;
    }

   
}
