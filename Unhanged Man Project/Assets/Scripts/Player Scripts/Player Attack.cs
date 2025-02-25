//Created by: Charlie
//Edited by:
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Attack;
    [SerializeField] private Weapon currentWeapon;
    private Attack attackVolume;

    private bool isAttacking = false;

    void Start()
    {
        Attack.SetActive(false); // make sure the damage field is off when game loads (redundancy)
        attackVolume = Attack.GetComponent<Attack>();
    }

    void Update()
    {
        // when player clicks left mouse, start attack
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            StartCoroutine(attackLogic());
        }

    }

    private IEnumerator attackLogic()
    {
        if (isAttacking) yield break; // if attack is already happening, break

        isAttacking = true; // player is attacking
        yield return new WaitForSeconds(currentWeapon.startupLength); // wait for the start up before attack can hit
        PassDamageThrough(); // make sure attack volume deals correct damage


        Attack.SetActive(true); // turn damage field on
        yield return new WaitForSeconds(currentWeapon.attackLength); // wait for attack to finish
        Attack.SetActive(false); // turn damage field off

        yield return new WaitForSeconds(currentWeapon.recoveryLength); // wait recovery time
        isAttacking = false; // player no longer attacking
    }

    public void NewWeapon(Weapon otherWeapon)
    {
        currentWeapon = otherWeapon; // pick up weapon
        Debug.Log("equiped new weapon: " + otherWeapon.weaponName);
    }

    public void PassDamageThrough()
    {
        attackVolume.AssignDamage(currentWeapon.damage);
    }
}
