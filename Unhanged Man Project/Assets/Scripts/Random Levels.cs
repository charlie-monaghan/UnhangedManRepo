using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevels : MonoBehaviour
{
    public static RandomLevels instance { get; private set; }

    [SerializeField] string[] regularLevelNames;
    [SerializeField] string[] bossLevelNames;
    private static List<string> recentLevels = new List<string>();
    private static List<string> recentBossLevels = new List<string>();

    public static bool levelChosen = false;
    public static int levelsBeat = 0;
    public static string nextLevelName;

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

    public string ReturnNextLevel()
    {
        levelsBeat++;
        Debug.Log("Levels beat = " + levelsBeat);
        if (levelsBeat >= 4)
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
        } while (recentBossLevels.Contains(nextLevelName) || recentLevels.Contains(nextLevelName) && attempts < 1000);

        if(levelArray == regularLevelNames)
        {
            TrackRecentLevels(nextLevelName);
        }
        else if (levelArray == bossLevelNames)
        {
            TrackRecentBossLevels(nextLevelName);
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
    private void TrackRecentBossLevels(string level)
    {
        recentBossLevels.Add(level);
        if (recentBossLevels.Count > 2)
        {
            recentBossLevels.RemoveAt(0);
        }
    }
}
