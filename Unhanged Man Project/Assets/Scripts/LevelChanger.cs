using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private string SceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DedicatedSceneChange();
        }
    }

    public void DedicatedSceneChange()
    {
        SceneManager.LoadScene(SceneName);
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
