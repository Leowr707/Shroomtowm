using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    [Header("Configura��es de Sons")]
    public AudioClip bgmSound;
    public AudioClip tiroJogadorSFX;
    public AudioClip tiroInimigoSFX;
    public AudioClip explosaoSFX;
    public AudioClip powerUpSFX;
    public AudioClip VidaSFX;
    public AudioSource audioSource;

    private void Awake()
    {
        // Verifica se já existe uma instância do AudioManager
        if (instancia == null)
        {
            // Se não existir, atribui esta instância à variável estática
            instancia = this;
            DontDestroyOnLoad(gameObject); // Mantém o objeto AudioManager ao mudar de cena
        }
        else
        {
            // Se já existir uma instância, destrói este objeto para evitar duplicatas
            Destroy(gameObject);
        }
    }

    public void TocarSomBMG() {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().PlayOneShot(bgmSound, 0.8f);
    }

    public void TocarSomVida() 
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(VidaSFX, 0.8f);
    }

    public void TocarSomMorte()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(explosaoSFX, 0.8f);
    }





}
