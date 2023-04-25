using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelOpcoes;

    public void jogar()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
    }

    public void AbrirOpcoes()
    {
        painelMenuPrincipal.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuPrincipal.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("sair Do Jogo");
        Application.Quit();
    }
}