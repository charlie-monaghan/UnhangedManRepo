//Created by: Charlie
//Edited by: Carter
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Attack;
    [SerializeField] private float attackDuration = 1.0f;
    public static bool isAttacking = false;
    [SerializeField] public Weapon currentWeapon;
    [SerializeField] public Weapon secondWeapon;
    [SerializeField] public AudioClip attackClip;
    private AudioSource audioSource;
    private Animator anim;

    private Attack attackVolume;

    public event Action onWeaponChange;

    void Start()
    {
        Attack.SetActive(false); // make sure the damage field is off when game loads (redundancy)
        attackVolume = Attack.GetComponent<Attack>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        if(PlayerManager.instance != null )
        {
            currentWeapon = PlayerManager.instance.currentWeapon ?? currentWeapon;
            secondWeapon = PlayerManager.instance.secondWeapon ?? secondWeapon;
        }

        PlayerManager.instance.SavePlayerData(PlayerManager.instance.playerHealth, currentWeapon, secondWeapon);
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

        switch (currentWeapon.weaponName) {
            case "Sword":
                anim.SetInteger("WeaponIndex", 0);
                break;
            case "Axe":
                anim.SetInteger("WeaponIndex", 1);
                break;
        }
    }

    private IEnumerator attackLogic()
    {
        if (isAttacking) yield break; // if attack is already happening, break

        audioSource.PlayOneShot(attackClip);
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
        attackVolume.AssignDamage(currentWeapon.damage); // makes sure attack volume has correct damage at attack time
    }

    public void NewWeapon(Weapon otherWeapon) // if player has only one weapon, adds weapon to second slot, if not, will drop and swap equipped
    {
        if(secondWeapon == null)
        {
            secondWeapon = otherWeapon;
            Debug.Log("picked up new weapon: " + otherWeapon.GetItemName());
        }
        else if(secondWeapon != null)
        {
            currentWeapon = otherWeapon;
            Debug.Log("swapped current weapon to " + currentWeapon.GetItemName());
        }

        PlayerManager.instance.SavePlayerData(PlayerManager.instance.playerHealth, currentWeapon, secondWeapon);
        onWeaponChange?.Invoke();
    }

    public void SwapWeapon()
    {
        if(secondWeapon != null)
        {
            Weapon swap = secondWeapon;
            secondWeapon = currentWeapon;
            currentWeapon = swap;
            Debug.Log("Swapped weapon to: " + currentWeapon.GetItemName());
        }
        else
        {
            Debug.Log("No second weapon");
            return;
        }

        PlayerManager.instance.SavePlayerData(PlayerManager.instance.playerHealth, currentWeapon, secondWeapon);
        onWeaponChange?.Invoke();
    }
}
