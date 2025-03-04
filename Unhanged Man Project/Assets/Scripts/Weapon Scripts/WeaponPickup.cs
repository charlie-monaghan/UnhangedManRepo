using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon givenWeapon;
    [SerializeField] private GameObject pickupPrefab;
    private PlayerAttack playerRef;

    private bool playerInRange = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerRef != null)
            {
                DropCurrentWeapon();
                playerRef.NewWeapon(givenWeapon);
                Destroy(this.gameObject);
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

    private void DropCurrentWeapon()
    {
        GameObject droppedWeapon = Instantiate(pickupPrefab, playerRef.transform.position, Quaternion.identity); // instantiate the players dropped weapon

        WeaponPickup droppedWeaponPickupScript = droppedWeapon.GetComponent<WeaponPickup>(); // get the players dropped weapon pickup script
        droppedWeaponPickupScript.SetWeapon(playerRef.currentWeapon); // set the pickup's givenWeapon to the one they dropped

        SpriteRenderer droppedWeaponSpriteRenderer = droppedWeapon.GetComponent<SpriteRenderer>(); // get the players dropped weapon pickup's sprite renderer
        droppedWeaponSpriteRenderer.sprite = playerRef.currentWeapon.weaponSprite; // set the pickup's sprite to the dropped weapons sprite
    }

    private void SetWeapon(Weapon newWeapon) // only really used in DropCurrentWeapon
    {
        givenWeapon = newWeapon;
    }
}
