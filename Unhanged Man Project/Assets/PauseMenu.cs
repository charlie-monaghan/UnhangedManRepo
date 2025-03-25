using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    void Start()
    {

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
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
