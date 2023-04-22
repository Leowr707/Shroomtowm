using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSom : MonoBehaviour
{
    [SerializeField] private AudioSource FundoMusical;
   public void VolumeMusica(float value)
   {
    FundoMusical.volume = value;
   }
}
