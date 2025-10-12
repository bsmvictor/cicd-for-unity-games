using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{

    public GameObject MainOptions;
    public GameObject Creds;
    public GameObject Controls;

    public void Start(){
        Voltar();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Creditos(){
        MainOptions.SetActive(false);
        Creds.SetActive(true);
    }

    public void Controles()
    {
        MainOptions.SetActive(false);
        Controls.SetActive(true);
    }

    public void Voltar()
    {
        Controls.SetActive(false);
        Creds.SetActive(false);
        MainOptions.SetActive(true);
    }
}