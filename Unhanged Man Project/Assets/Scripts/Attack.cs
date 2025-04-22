using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int knockbackForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health otherHealth = collision.gameObject.GetComponent<Health>(); // Grabbing health component from other object
        if (otherHealth != null) // Checking that its actually there
        {
            otherHealth.DamageHealth(damage); // dealing damage

            if (gameObject.CompareTag("Player Attack"))
            {
                Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }

        }
        else if (otherHealth == null)
        {
            
        }
    }

    public void AssignDamage(int SODamage)
    {
        damage = SODamage; // damage value is equal to the current weapons damage
    }

    public void AssignKnockback(int SOKnockback)
    {
        knockbackForce = SOKnockback; // knockback force is equal to the current weapons knockback
    }
}
