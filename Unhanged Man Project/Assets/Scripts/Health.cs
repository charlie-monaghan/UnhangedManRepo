using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioClip damageSound;
    private AudioSource audioSource;
    private AudioSource persSource;

    public event Action onHealthChanged;
    public event Action onDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        persSource = GameObject.FindGameObjectWithTag("PersistentAudioSource").GetComponent<AudioSource>();
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
            persSource.PlayOneShot(damageSound);
            gameObject.SetActive(false);
            onDeath?.Invoke();
        }
        else if (currentHealth <= 0)
        {
            //Play death animation
            onDeath?.Invoke();
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

        onHealthChanged?.Invoke();
    }

    public void DamageHealth(int damage)
    {
        audioSource.PlayOneShot(damageSound);
        currentHealth -= damage; // applies damage
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ditto

        if (tag == "Player")
        {
            PlayerManager.instance.playerHealth = currentHealth;
        }

        onHealthChanged?.Invoke();
    }

    public int ReturnHealth()
    {
        return currentHealth;
    }
}
