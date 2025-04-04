using System.Collections.Generic;
using UnityEngine;

public class RandomLevels : MonoBehaviour
{
    [SerializeField] string[] regularLevelNames;
    [SerializeField] string[] bossLevelNames;
    private List<string> recentLevels = new List<string>();

    private int levelsBeat = 0;
    public string nextLevelName;

    public string ReturnNextLevel()
    {
        levelsBeat++;
        if(levelsBeat < 3)
        {
            ChooseLevel(regularLevelNames);
        }
        else if(levelsBeat == 3)
        {
            ChooseLevel(bossLevelNames);
            levelsBeat = 0;
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
        } while (recentLevels.Contains(nextLevelName) && attempts < 10);

        TrackRecentLevels(nextLevelName);
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
