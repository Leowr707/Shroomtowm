using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ItemDeCura : MonoBehaviour


{
    public int valorDeCura = 1;

    private void OnTriggerEnter(Collider other)
    {
        Nave nave = other.GetComponent<Nave>();
        if (nave != null)
        {
            nave.RecuperarVida(valorDeCura);
            AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.VidaSFX, 0.5f);
            AudioManager.instancia.TocarSomVida();          
            Destroy(gameObject);
        }
    }
}
