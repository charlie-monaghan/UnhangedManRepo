using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
        }
    }
}
