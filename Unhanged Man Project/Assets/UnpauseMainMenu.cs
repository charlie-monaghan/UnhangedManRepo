using UnityEditor;
using UnityEngine;

public class UnpauseMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu.paused = false;
        pauseMenu.SetActive(false); 
        Time.timeScale = 1f;  
    }   
}
