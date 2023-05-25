using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ItemDeCura : MonoBehaviour


{
    public int valorDeCura = 1;
    
    [SerializeField]public GameObject VidaText;

    private IEnumerator vidatxt()
    {
            VidaText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            VidaText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Nave nave = other.GetComponent<Nave>();
        if (nave != null)
        {
            nave.RecuperarVida(valorDeCura);
            AudioManager.instancia.GetComponent<AudioSource>().PlayOneShot(AudioManager.instancia.VidaSFX, 0.5f);
            AudioManager.instancia.TocarSomVida();
            vidatxt();      
            Destroy(gameObject);
        }else
        {
            Destroy(gameObject, 10);
        }
    }
}
