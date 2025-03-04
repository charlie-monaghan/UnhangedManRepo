using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapons/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
    private Transform attackPoint;
    public override void Attack(Transform attackSpawn)
    {
        
    }
}
