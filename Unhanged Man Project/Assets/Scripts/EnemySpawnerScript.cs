using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

    void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        GameObject enemy = GameObject.Instantiate(enemyList[Random.Range(0, enemyList.Count)], gameObject.transform.position, gameObject.transform.rotation);
    }
}
