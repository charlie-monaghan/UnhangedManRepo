using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevels : MonoBehaviour
{
    public static RandomLevels instance { get; private set; }

    [SerializeField] string[] regularLevelNames;
    [SerializeField] string[] bossLevelNames;
    private List<string> recentLevels = new List<string>();

    public bool levelChosen = false;
    private int levelsBeat = 0;
    public string nextLevelName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetLevelFlag()
    {
        levelChosen = false;
    }

    public string ReturnNextLevel()
    {
        //if (levelChosen)
        //{
        //    Debug.LogWarning("ReturnNextLevel called again before reset!");
        //    return nextLevelName;
        //}

        //levelChosen = true;

        levelsBeat++;
        Debug.Log("Levels beat = " + levelsBeat);
        if (levelsBeat >= 3)
        {
            ChooseLevel(bossLevelNames);
            levelsBeat = 0;
        }
        else
        {
            ChooseLevel(regularLevelNames);
        }
        return nextLevelName;
    }

    private void ChooseLevel(string[] levelArray)
    {
        int attempts = 0;
        do
        {
            int nextLevelNum = Random.Range(0, levelArray.Length);
            nextLevelName = levelArray[nextLevelNum];
            attempts++;
        } while (recentLevels.Contains(nextLevelName) && attempts < 1000);

        if(levelArray == regularLevelNames)
        {
            TrackRecentLevels(nextLevelName);
        }
        //Debug.Log("recent levels are: " + recentLevels[0] + " and " + recentLevels[1]);
    }

    private void TrackRecentLevels(string level)
    {
        recentLevels.Add(level);
        if(recentLevels.Count > 2)
        {
            recentLevels.RemoveAt(0);
        }
    }
}
