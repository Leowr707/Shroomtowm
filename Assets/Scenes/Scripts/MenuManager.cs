using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instanciaMenu;
    public bool isPaused = false;
        public GameObject PauseMenu;
        public void CarregarCena(string nomeDaCena) 
        {
        //Vai carregar a cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void CarregarMenu(string nomeDaCena)
        {
        SceneManager.LoadScene(nomeDaCena);
        }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          if (isPaused)
          {
            ResumeGame();
          }
          else
          {
            PauseGame();
          }
        }
        

    }
    void PauseGame()
    {
        Time.timeScale = 0f; // Pausa o jogo
        isPaused = true; // Define que o jogo está pausado
        PauseMenu.gameObject.SetActive(true); // Exibe o menu de pause
    }
     public void ResumeGame()
    {
        Time.timeScale = 1f; // Retoma o jogo
        isPaused = false; // Define que o jogo não está pausado
        PauseMenu.gameObject.SetActive(false); // Oculta o menu de pause
    }
}
