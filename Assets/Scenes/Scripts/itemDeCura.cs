using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDeCura : MonoBehaviour
{

    [Header("Controle de queda")]
   // public float velocidadeDeQueda;

    Vector3 mov;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        queda(); 
    }

    void queda()
    {
        mov = transform.position;

        mov.y += 0.0f;

        transform.position = mov;
    }
}
