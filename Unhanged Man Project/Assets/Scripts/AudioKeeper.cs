//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioKeeper : MonoBehaviour
{
    static AudioKeeper instance;
    public AudioSource source;
    public AudioClip regularLevelAudios, bossLevelAudios, mainMenusAudios, tutorialAudios; //audio clips

    //allowed scenes/level names
    [SerializeField] string[] levelsRegular = {"Level_1", "Level_2", "Level_3", "Level_4", "Level_5", "Level_6" };
    [SerializeField] string[] levelsMenus = { "Main Menu", "Credits" };
    [SerializeField] string[] levelsBosses =
        { "Boss_Level_1", "Boss_Level_2", "Boss_Level_3", "Boss_Level_4", 
        "Boss_Level_5", "Boss_Level_6", "Boss_Level_7", "Boss_Level_8" };
    [SerializeField] string[] levelsTutorial = {"Tutorial_Level", "Tutorial_Gallows"};

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneAudio; 
        }
        else if (instance != this)
        {
            Destroy(gameObject); // removing duplicates
        }        
    }
    
    private void SceneAudio(Scene scene, LoadSceneMode loadScene)
    {
        if (scene.name == "Settings") //no switiching tracks when opening/closing settings page
        {
            Debug.Log("Settings scene! (keeping current music)");
            return;
        }

        foreach (string regLevel in levelsRegular) //checks each "regular" level name in string array 
        {
            //Debug.Log(regLevel);
            if (scene.name == regLevel)
            {
                Debug.Log("Regular Level Audio!");
                PlayAudioClip(regularLevelAudios);
                break;
            }
        }
        foreach (string menuLevel in levelsMenus) //checks each "menu" level name in string array 
        {
            //Debug.Log(menuLevel);
            if (scene.name == menuLevel)
            {
                Debug.Log("Main Menu Audio!");
                PlayAudioClip(mainMenusAudios);
                break;
            }           
        }
        foreach (string bossLevel in levelsBosses)//checks each "boss" level name in string array
        {
            //Debug.Log(bossLevel);
            if (scene.name == bossLevel)
            {
                Debug.Log("Boss Level Audio!");
                PlayAudioClip(bossLevelAudios);
                break;
            }
        }
        foreach(string tutorialLevel in levelsTutorial) //checks each "tutorial" level name in string array
        {
            //Debug.Log(tutorialLevel);
            if(scene.name == tutorialLevel)
            {
                Debug.Log("Tutorial Level Audio!");
                PlayAudioClip(tutorialAudios);
                break;
            }
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if(source.clip != clip)
        {
            source.clip = clip;
            source.Play();
        }
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            SceneManager.sceneLoaded -= SceneAudio;
        }
    }
}
