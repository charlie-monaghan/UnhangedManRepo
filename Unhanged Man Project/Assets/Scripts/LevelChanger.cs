using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private string DedicatedSceneName;
    private string RandomSceneName;
    [SerializeField] private RandomLevels randomLevels;

    private bool inZone = false;

    private void Start()
    {
        randomLevels = FindAnyObjectByType<RandomLevels>();
    }

    private void Update()
    {
        if (inZone && Input.GetKeyDown(KeyCode.E))
        {
            GetNextLevel();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //GetNextLevel();
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inZone = false;
        }
    }

    private void GetNextLevel()
    {
        RandomSceneName = randomLevels.ReturnNextLevel();
        StartCoroutine(LoadNextLevel());
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
            GetNextLevel();
        }
    }
}
