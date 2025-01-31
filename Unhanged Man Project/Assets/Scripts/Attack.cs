using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int damage = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health otherHealth = collision.gameObject.GetComponent<Health>(); // Grabbing health component from other object
        if (otherHealth != null) // Checking that its actually there
        {
            otherHealth.ChangeHealth(damage); // dealing damage
        }
        else if (otherHealth == null)
        {
            Debug.Log("otherHealth is null");
        }
    }
}
