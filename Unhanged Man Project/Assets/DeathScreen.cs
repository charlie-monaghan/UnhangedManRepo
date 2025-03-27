using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] GameObject deathScreen;
    private Health playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathScreen.SetActive(false);
        playerHealth = player.GetComponent<Health>();
        if (playerHealth != null) 
        {
            playerHealth.onDeath += showDeathScreen;
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.onDeath -= showDeathScreen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
