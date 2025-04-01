using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int enemiesInScene;
    private int defeatedEnemies;

    [SerializeField] private GameObject levelTransition;
    [SerializeField] private GameObject levelReward;

    private bool finished = false;

    void Start()
    {
        levelTransition.SetActive(false);
        levelReward.SetActive(false);
    }

    void Update()
    {
        if(defeatedEnemies == enemiesInScene && !finished)
        {
            levelTransition.SetActive(true);
            levelReward.SetActive(true);
            finished = true;
        }
    }

    public void DefeatedEnemy()
    {
        defeatedEnemies++;
        Debug.Log("Defeated enemies = " + defeatedEnemies);
    }
}
