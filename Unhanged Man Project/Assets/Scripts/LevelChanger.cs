using System.Collections;
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
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.1f);
        DedicatedSceneChange(RandomSceneName);
    }

    public void DedicatedSceneChange(string levelName)
    {
        SceneManager.LoadScene(levelName);
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
            DedicatedSceneChange(DedicatedSceneName);
        }
    }
}
