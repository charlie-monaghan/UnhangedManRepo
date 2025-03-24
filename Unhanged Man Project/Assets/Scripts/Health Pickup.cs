using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToHeal;

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
