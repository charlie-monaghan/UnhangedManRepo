using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int enemiesInScene;
    private int defeatedEnemies;

    [SerializeField] private GameObject levelTransition;

    void Start()
    {
        levelTransition.SetActive(false);
    }

    void Update()
    {
        if(defeatedEnemies == enemiesInScene)
        {
            levelTransition.SetActive(true);
        }
    }

    public void defeatedEnemy()
    {
        defeatedEnemies++;
        Debug.Log("Defeated enemies = " + defeatedEnemies);
    }
}
