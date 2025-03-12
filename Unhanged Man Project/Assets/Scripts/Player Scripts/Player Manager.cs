using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }

    public int playerHealth;
    public Weapon currentWeapon;
    public Weapon secondWeapon;

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
}
