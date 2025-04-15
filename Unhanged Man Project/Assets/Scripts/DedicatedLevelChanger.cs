using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DedicatedLevelChanger : MonoBehaviour

{
    [SerializeField] private string DedicatedSceneName;

    private bool inZone = false;

    private void Update()
    {
        if (inZone && Input.GetKeyDown(KeyCode.E))
        {
            DedicatedSceneChange(DedicatedSceneName);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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

    public void DedicatedSceneChange(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
