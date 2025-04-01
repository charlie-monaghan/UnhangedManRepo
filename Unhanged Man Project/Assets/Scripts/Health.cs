using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioClip healSFX;

    public event Action onHealthChanged;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        if (tag == "Player")
        {
            //currentHealth = PlayerManager.Instance.playerHealth > 0 ? PlayerManager.Instance.playerHealth : maxHealth;
            currentHealth = PlayerManager.instance.playerHealth > 0 ? PlayerManager.instance.playerHealth : maxHealth;
        }
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

        if(tag == "Player")
        {
            PlayerManager.instance.playerHealth = currentHealth;
        }
        if (audioSource != null && healSFX != null)
        {
            audioSource.PlayOneShot(healSFX);
        }

        onHealthChanged?.Invoke();
    }

    public void DamageHealth(int damage)
    {
        currentHealth -= damage; // applies damage
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ditto

        if (tag == "Player")
        {
            PlayerManager.instance.playerHealth = currentHealth;
        }
        if (audioSource != null && damageSFX != null) {
            audioSource.PlayOneShot(damageSFX);
        }

        onHealthChanged?.Invoke();
    }

    public int ReturnHealth()
    {
        return currentHealth;
    }
}
