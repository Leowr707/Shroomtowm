using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager 
{
    private static int pontos;


    public static int Pontos {
        get {
            return pontos;
        }
        set {
            pontos = value;
            if (pontos < 0){
                pontos = 0;
            } 
            Debug.Log("Pontos: " + Pontos);  
        }
    }  
}
