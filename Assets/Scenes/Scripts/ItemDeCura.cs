using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeCura : MonoBehaviour
{
   [SerializeField] 
    private int vidas;

    public int VidaS {
        get{
            return this.vidas;
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        // Executar ação de coleta (exemplo: aumentar pontuação do jogador)

        // Destruir objeto coletado
        Destroy(gameObject);
    }
}
}    
