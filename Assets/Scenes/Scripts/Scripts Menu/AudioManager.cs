using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    [SerializeField] AudioMixer Mixer;

    [Header("Configura��es de Sons")]
    public AudioClip bgmSound;
    public AudioClip tiroJogadorSFX;
    public AudioClip tiroInimigoSFX;
    public AudioClip explosaoSFX;
    public Slider controleVolume;
    public Slider controleSons;
    
    public AudioClip powerUpSFX;
    public AudioClip VidaSFX;
    public AudioSource audioSource;

    public const string MUSIC_KEY = "MusicBgd";
    public const string SONS_KEY = "MusicSons";


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
        LoadVolume();
    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
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


    void LoadVolume()
    {
        float MusicBgd = PlayerPrefs.GetFloat(MUSIC_KEY, 1F);
        float MusicSons= PlayerPrefs.GetFloat(SONS_KEY, 1F);
        
        Mixer.SetFloat(AudioSettings.MIXER_MUSIC, Mathf.Log10(MusicBgd)* 20);
        Mixer.SetFloat(AudioSettings.MIXER_SONS, Mathf.Log10(MusicSons)* 20);
    }





}
