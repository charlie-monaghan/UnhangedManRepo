using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioKeeper : MonoBehaviour
{
    public static AudioKeeper instance;

    //allowed scenes
    public string scene1 = "Main Menu", scene2 = "Credits";
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
