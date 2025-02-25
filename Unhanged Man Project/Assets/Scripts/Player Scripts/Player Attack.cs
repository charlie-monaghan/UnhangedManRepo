//Created by: Charlie
//Edited by:
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Attack;
    [SerializeField] public Weapon currentWeapon;
    [SerializeField] public Weapon secondWeapon;

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeapon();
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
    public void PassDamageThrough()
    {
        attackVolume.AssignDamage(currentWeapon.damage);
    }

    public void NewWeapon(Weapon otherWeapon)
    {
        if(secondWeapon == null)
        {
            secondWeapon = otherWeapon;
            Debug.Log("picked up new weapon: " + otherWeapon.weaponName);
        }
        else if(secondWeapon != null)
        {
            currentWeapon = otherWeapon;
            Debug.Log("swapped current weapon to " + currentWeapon.weaponName);
        }
        //currentWeapon = otherWeapon; // pick up weapon
        //Debug.Log("equiped new weapon: " + otherWeapon.weaponName);
    }

    public void SwapWeapon()
    {
        if(secondWeapon != null)
        {
            Weapon swap = secondWeapon;
            secondWeapon = currentWeapon;
            currentWeapon = swap;
            Debug.Log("Swapped weapon to: " + currentWeapon.weaponName);
        }
        else
        {
            Debug.Log("No second weapon");
            return;
        }
    }
}
