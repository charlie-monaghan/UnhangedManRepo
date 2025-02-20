using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon givenWeapon;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponHandler weaponHandler = other.GetComponent<WeaponHandler>();
            if (weaponHandler != null)
            {
                weaponHandler.newWeapon(givenWeapon);
                this.gameObject.SetActive(false);
            }
        }
    }
}
