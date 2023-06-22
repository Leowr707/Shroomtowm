using UnityEngine; 
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider MusicaSlider;
    [SerializeField] Slider SonsSlider;

    public const string MIXER_MUSIC = "MusicBgd";
    public const string MIXER_SONS = "MusicSons";


    void Awake() 
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
    }



}
