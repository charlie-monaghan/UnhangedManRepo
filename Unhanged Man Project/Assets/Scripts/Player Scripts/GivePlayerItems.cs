using System.Collections;
using UnityEngine;

public class GivePlayerItems : MonoBehaviour
{
    private Health playerHealth;
    void Start()
    {
        playerHealth = GetComponent<Health>();
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(Item i in PlayerManager.instance.playerItems)
             {
                i.AssignOwner(gameObject);
                Instantiate(i.itemPrefab, gameObject.transform);
             }
        //if (playerHealth.currentHealth > playerHealth.maxHealth)
        //{
        //    playerHealth.currentHealth = playerHealth.maxHealth;
        //    PlayerManager.instance.playerHealth = playerHealth.currentHealth;
        //}
    }

    void Update()
    {
        
    }
}
