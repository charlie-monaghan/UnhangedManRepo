using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public int damage;
    //public AudioClip weaponSound;
    
    public virtual void Attack(Transform attackSpawn)
    {

    }

}
