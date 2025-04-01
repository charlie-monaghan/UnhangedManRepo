using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : Item
{
    public int damage;
    public int knockbackForce;
    public float startupLength;
    public float attackLength;
    public float recoveryLength;
    //public AudioClip weaponSound;
    
    public virtual void Attack(Transform attackSpawn)
    {

    }

}
