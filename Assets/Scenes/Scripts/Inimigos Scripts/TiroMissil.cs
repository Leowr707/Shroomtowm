using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroMissil : MonoBehaviour
{
    public float forca;


    // Start is called before the first frame update
    void Start()
    {
        //em 5 segundos apos ser gerado, o tiro sera destru�do
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * forca);
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * forca * Time.deltaTime;



    }


}
