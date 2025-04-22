using UnityEngine;
using System;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToHeal;
    [SerializeField] private bool guaranteedToSpawn;
    [SerializeField] private float spawnThreshold = 3f;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (!guaranteedToSpawn)
        {
            if (UnityEngine.Random.Range(0f, 10f) * player.GetComponent<Health>().currentHealth / player.GetComponent<Health>().maxHealth >= spawnThreshold)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerRef = other.GetComponent<Health>();
            playerRef.HealHealth(healthToHeal);
            Destroy(this.gameObject);
        }
    }
}
