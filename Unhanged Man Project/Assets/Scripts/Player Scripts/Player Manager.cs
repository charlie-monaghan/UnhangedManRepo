using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }

    public int playerHealth;
    public Weapon currentWeapon;
    public Weapon secondWeapon;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            Health playerHealthComp = player.GetComponent<Health>();
            playerHealth = playerHealthComp.maxHealth;
        }
        currentWeapon = null;
        secondWeapon = null;

        playerItems.Clear();
        RandomLevels.levelsBeat = 0;
    }

    public int GetItemStack(string itemName)
    {
        Item item = playerItems.Find(i => i.itemName == itemName);
        return item != null ? item.stackAmount : 0;
    }

}
