using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    public GameObject PauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PauseMenu.SetActive(false);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Continue(){
        PauseMenu.SetActive(false);
    }

}
