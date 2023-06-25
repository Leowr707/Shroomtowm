using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    [Header("Configura��es de Sons")]
    public AudioClip bgmSound;
    public AudioClip tiroJogadorSFX;
    public AudioClip tiroInimigoSFX;
    public AudioClip explosaoSFX;
    public Slider controleVolume;
    public Slider controleSons;
    
    public AudioClip powerUpSFX;
    public AudioClip VidaSFX;
    


    void Awake() {
        if (instancia == null) {
            instancia = this;
        }
    }

    private void Start()
    {
        //audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.PlayOneShot(bgmSound, 0.7f);
    }


    public void TocarSomVida() 
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(VidaSFX, 0.8f);
    }

    public void TocarSomMorte()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(explosaoSFX, 0.8f);
    }

    public void MudarVolume() {
        AudioListener.volume = controleVolume.value;
    }

    public void MudarSons(Slider slider) {
        AudioListener.volume = controleSons.value;
    }


    //void LoadVolume()
   // {
      //  float MusicBgd = PlayerPrefs.GetFloat(MUSIC_KEY, 1F);
      //  float MusicSons= PlayerPrefs.GetFloat(SONS_KEY, 1F);
        
       // Mixer.SetFloat(AudioSettings.MIXER_MUSIC, Mathf.Log10(MusicBgd)* 20);
      //  Mixer.SetFloat(AudioSettings.MIXER_SONS, Mathf.Log10(MusicSons)* 20);
   // }





}
