//Created by: Charlie
//Edited by:
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Attack;
    [SerializeField] private float attackDuration = 1.0f;
    private bool isAttacking = false;

    void Start()
    {
        Attack.SetActive(false); // make sure the damage field is off when game loads (redundancy)
    }

    void Update()
    {
        // when player clicks left mouse, start attack
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            StartCoroutine(attackTime());
        }

    }

    private IEnumerator attackTime()
    {
        if (isAttacking) yield break; // if attack is already happening, break
        isAttacking = true; // player is attacking

        Attack.SetActive(true); // turn damage field on
        yield return new WaitForSeconds(attackDuration); // wait for attack to finish
        Attack.SetActive(false); // turn damage field off

        isAttacking = false; // player no longer attacking
    }
}
