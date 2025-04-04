using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private string DedicatedSceneName;
    private string RandomSceneName;
    [SerializeField] private RandomLevels randomLevels;

    private void Start()
    {
        randomLevels = FindAnyObjectByType<RandomLevels>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RandomSceneName = randomLevels.ReturnNextLevel();
            DedicatedSceneChange();
        }
    }

    public void DedicatedSceneChange()
    {
        if(RandomSceneName != null)
        {
            SceneManager.LoadScene(RandomSceneName);
        }
        else
        {
            SceneManager.LoadScene(DedicatedSceneName);
        }
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.ResetPlayerData();
            DedicatedSceneChange();
        }
    }
}
