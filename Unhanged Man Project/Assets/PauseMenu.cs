using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false; 
    [SerializeField] GameObject pauseMenu;
    void Awake()
    {
        DontDestroyOnLoad(pauseMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.activeSelf == true)
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
        {
            ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        {
            PauseGame();
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        //Debug.Log(paused);
        paused = false;
    }
    public void PauseGame()
    {
        paused = true;
        //Debug.Log(paused);
        pauseMenu.SetActive(true);     
        Time.timeScale = 0f;
    }

   /* public void ForMainMenu()
    {
        ResumeGame();
        Debug.Log(paused);
        SceneManager.LoadScene("Main Menu");        
        //Destroy(pauseMenu);
    }*/
}
