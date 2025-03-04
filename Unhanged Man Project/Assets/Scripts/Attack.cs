using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health otherHealth = collision.gameObject.GetComponent<Health>(); // Grabbing health component from other object
        if (otherHealth != null) // Checking that its actually there
        {
            otherHealth.DamageHealth(damage); // dealing damage
        }
        else if (otherHealth == null)
        {
            Debug.Log("otherHealth is null");
        }
    }

    public void AssignDamage(int SODamage)
    {
        damage = SODamage; // damage value is equal to the current weapons damage
    }
}
