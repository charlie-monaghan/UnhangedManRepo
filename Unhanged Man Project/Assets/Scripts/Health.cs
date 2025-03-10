using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public event Action onHealthChanged;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        
    }

    void Update()
    {
        // if objects health is 0 and its not the player, kill it
        if (currentHealth <= 0 && tag != "Player")
        {
            gameObject.SetActive(false);
        }
    }

    public void HealHealth(int healing)
    {
        currentHealth += healing; // applies healing
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // clamps health value if it goes over max or under 0

        onHealthChanged?.Invoke();
    }

    public void DamageHealth(int damage)
    {
        currentHealth -= damage; // applies damage
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ditto

        onHealthChanged?.Invoke();
    }

    public int ReturnHealth()
    {
        return currentHealth;
    }
}
