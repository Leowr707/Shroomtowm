using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider objetoColidido)
    {

        if (objetoColidido.transform.tag == "tiroInimigo" || objetoColidido.transform.tag == "tiroPlayer" || objetoColidido.transform.tag == "tiroEspecialPlayer" || objetoColidido.transform.tag == "itemDeCura")
        {
            Destroy(objetoColidido.gameObject);
        }

    }
}