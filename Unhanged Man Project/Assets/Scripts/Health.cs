using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    [SerializeField] public int currentHealth;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] public GameObject hitParticles;
    [SerializeField] public GameObject deathParticles;
    public GameObject healthPickupPrefab;
    private AudioSource audioSource;
    private AudioSource persSource;

    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioClip healSFX;

    public event Action onHealthChanged;
    public event Action onPlayerDeath;

    public event Action onBossEnemyDeath;

    private void Awake()
    {
        if (tag == "Player")
        {
            Debug.Log("awake health = " + currentHealth+" / "+maxHealth);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        persSource = GameObject.FindGameObjectWithTag("PersistentAudioSource").GetComponent<AudioSource>();
        if (tag == "Player")
        {
            currentHealth = PlayerManager.instance.playerHealth > 0 ? PlayerManager.instance.playerHealth : maxHealth;
            Debug.Log("start health = " + currentHealth + " / " + maxHealth);

        }
        onHealthChanged?.Invoke();
    }

    void Update()
    {
        // if objects health is 0 and its not the player, kill it
        if (currentHealth <= 0 && tag != "Player")
        {
            Instantiate(deathParticles, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(healthPickupPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            persSource.PlayOneShot(damageSound);
            gameObject.SetActive(false);
            onBossEnemyDeath?.Invoke();
        }
        else if (currentHealth <= 0) // kills player
        {
            onPlayerDeath?.Invoke();
        }

        //if(tag == "Player")
        //{
        //    PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        //    PlayerManager.instance.SavePlayerData(ReturnHealth(), playerAttack.currentWeapon, playerAttack.secondWeapon);
        //}
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

        if (tag == "Player" && currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
        if (audioSource != null && damageSFX != null) {
            audioSource.PlayOneShot(damageSFX);
        }

        if (tag == "Player" && currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
