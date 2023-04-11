using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float forca;


    // Start is called before the first frame update
    void Start()
    {
        //em 5 segundos após ser gerado, o tiro será destruído
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * forca);
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * forca * Time.deltaTime;



    }

   
}
