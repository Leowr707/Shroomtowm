using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeCura : MonoBehaviour


{
    public int valorDeCura = 1;

    private void OnTriggerEnter(Collider other)
    {
        Nave nave = other.GetComponent<Nave>();
        if (nave != null)
        {
            nave.RecuperarVida(valorDeCura);
            Destroy(gameObject);
        }
    }
}
