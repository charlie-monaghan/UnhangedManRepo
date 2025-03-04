using UnityEngine;
using TMPro;

public class DrawPlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI heartNumber;

    private Health playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();

        if(playerHealth != null)
        {
            playerHealth.onHealthChanged += updateHealth;
            updateHealth();
        }
    }

    private void OnDestroy()
    {
        if(playerHealth != null)
        {
            playerHealth.onHealthChanged -= updateHealth;
        }
    }

    void Update()
    {
        
    }

    private void updateHealth()
    {
        if (playerHealth != null)
        {
            heartNumber.text = playerHealth.ReturnHealth().ToString();
        }
    }
}
