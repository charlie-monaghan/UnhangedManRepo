using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] public GameObject hitParticles;
    [SerializeField] public GameObject deathParticles;
    private AudioSource audioSource;
    private AudioSource persSource;

    public event Action onHealthChanged;
    public event Action onPlayerDeath;

    public event Action onBossEnemyDeath;

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
        onHealthChanged?.Invoke();
    }

    void Update()
    {
        // if objects health is 0 and its not the player, kill it
        if (currentHealth <= 0 && tag != "Player")
        {
            Instantiate(deathParticles, gameObject.transform.position, gameObject.transform.rotation);
            persSource.PlayOneShot(damageSound);
            gameObject.SetActive(false);
            onBossEnemyDeath?.Invoke();
        }
        else if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke();
        }

        if(tag == "Player" && currentHealth > maxHealth)
        {
            DamageHealth(0);
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

        Instantiate(hitParticles, gameObject.transform.position, gameObject.transform.rotation);

        onHealthChanged?.Invoke();
    }

    public int ReturnHealth()
    {
        return currentHealth;
    }
}
