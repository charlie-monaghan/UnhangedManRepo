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
            PlayerAttack weaponHandler = other.GetComponent<PlayerAttack>();
            if (weaponHandler != null)
            {
                weaponHandler.NewWeapon(givenWeapon);
                this.gameObject.SetActive(false);
            }
        }
    }
}
