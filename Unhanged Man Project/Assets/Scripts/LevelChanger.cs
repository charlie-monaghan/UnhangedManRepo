using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private string SceneName;

    public void changeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
