using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform attackSpawn;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon?.Attack(attackSpawn);
        }
    }

    public void newWeapon(Weapon otherWeapon)
    {
        currentWeapon = otherWeapon;
        Debug.Log("equiped new weapon: " + otherWeapon.weaponName);
    }
}
