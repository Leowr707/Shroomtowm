using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barreira : MonoBehaviour
{

    public float firula;

    // Start is called before the first frame update
    void Start()
    {

    }

    private IEnumerator OnCollisionEnter(Collision objetoColidido)
    {

        if (objetoColidido.transform.tag == "tiroInimigo" || objetoColidido.transform.tag == "tiroPlayer" || objetoColidido.transform.tag == "tiroEspecialPlayer" || objetoColidido.transform.tag == "itemDeCura")
        {
            Destroy(objetoColidido.gameObject);
        }

        yield break;
    }
}
