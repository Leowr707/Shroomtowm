
using UnityEngine; 
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider MusicSlider;

    public void SetMusicSFX()
    {
        float volume = SFXSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume)* 20);
    }

    public void SetMusic()
    {
        float volume = SFXSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)* 20);
    }

    //public const string MIXER_SFX = "SFX";
    //public const string MIXER_SONS = "MusicSons";


    /*void Awake() 
    {
        MusicaSlider.onValueChanged.AddListener(SetMusicBgd);
        SonsSlider.onValueChanged.AddListener(SetMusicSons);
    }

    void SetMusicBgd(float Value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(Value)* 20);
    }

    void SetMusicSons(float Value)
    {
        mixer.SetFloat(MIXER_SONS, Mathf.Log10(Value)* 20);
    }*/



}
