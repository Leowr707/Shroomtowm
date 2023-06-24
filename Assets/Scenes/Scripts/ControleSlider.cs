using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleSlider : MonoBehaviour
{
    public GameObject AudioSetting;
    public void Start() {
        // Recupera o valor atual do volume do grupo de áudio Music
        if (AudioSettings.instancia.mixer.GetFloat("SFX", out float volume)) {
            gameObject.GetComponent<Slider>().value = volume;
            Debug.Log(volume);
        }
        if (AudioSettings.instancia.mixer.GetFloat("Music", out float volume2)) {
           // MusicSlider.value = volume2;
        }
    }


    
}
