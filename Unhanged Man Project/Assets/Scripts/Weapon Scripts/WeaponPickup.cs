using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon givenWeapon;
    private bool playerInRange = false;
    private PlayerAttack playerRef;

    void Start()
    {
        
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerRef != null)
            {
                playerRef.NewWeapon(givenWeapon);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerRef = other.GetComponent<PlayerAttack>();

            if (playerRef != null && playerRef.secondWeapon == null)
            {
                playerRef.NewWeapon(givenWeapon);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
        
    }

    private void Pickup()
    {

    }
}
