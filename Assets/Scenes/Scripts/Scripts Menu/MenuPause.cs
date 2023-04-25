using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject Menu_Configs;
    [SerializeField] private string nomeDoLevelDeJogo;
    public MenuManager menuManager;
    


    public void ResumeGame()
    {
        menuManager.ResumeGame();
    }
    public void AbrirOpcoes()
    {
        Menu_Configs.SetActive(true);
    }

    public void FecharOpcoes()
    {
        Menu_Configs.SetActive(false);
    }

    public void MenuPrincipal()
    {
        Menu_Configs.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
    
}
