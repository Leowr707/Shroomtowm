using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{

    public Text textoPontos;

    // Update is called once per frame
    void Update()
    {
        this.textoPontos.text = ScoreManager.Pontos.ToString();
    }
}
    