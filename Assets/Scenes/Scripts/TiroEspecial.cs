using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroEspecial : MonoBehaviour
{
    public float forca;
    

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * forca * Time.deltaTime;
    }
}
