using System;
using UnityEngine; 
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider MusicSlider;
    public static AudioSettings instancia;
    
    void Awake() {
        if (instancia == null) {
            instancia = this;
        }
    }
    void Start()
    {
        
    }
     
    public void SetMusicSFX()
    {
        float volume = SFXSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume)* 20);
        SFXSlider.value = volume;
        
    }

    public void SetMusic()
    {
        float volume = MusicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)* 20);
        MusicSlider.value = volume;
        
    }
    
}
