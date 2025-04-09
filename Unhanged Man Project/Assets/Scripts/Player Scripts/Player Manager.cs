using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }

    public int playerHealth;
    public Weapon currentWeapon;
    public Weapon secondWeapon;

    private DrawPlayerItems drawItems;

    public List<Item> playerItems = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData(int health, Weapon current, Weapon second)
    {
        playerHealth = health;
        currentWeapon = current;
        secondWeapon = second;
    }

    public void ResetPlayerData()
    {
        //health and weapons
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            Health playerHealthComp = player.GetComponent<Health>();
            playerHealth = playerHealthComp.maxHealth;
        }
        currentWeapon = null;
        secondWeapon = null;

        //items
        foreach(Item item in playerItems)
        {
            item.stackAmount = 0;
        }
        playerItems.Clear();

        //update ui
        drawItems = FindFirstObjectByType<DrawPlayerItems>();
        if(drawItems != null)
        {
            drawItems.UpdateItems();
        }
        
    }

    public int GetItemStack(string itemName)
    {
        Item item = playerItems.Find(i => i.itemName == itemName);
        return item != null ? item.stackAmount : 0;
    }

}
