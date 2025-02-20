using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapons/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
    public override void Attack(Transform attackSpawn)
    {
        Debug.Log(weaponName + " Melee Attack");
    }
}
