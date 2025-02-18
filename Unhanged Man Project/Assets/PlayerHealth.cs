using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int extraHearts = 0;
    private int currentHealth;

    public Sprite fullHeart, halfHeart, emptyHeart, extraHeart;
    private List<Image> heartImages = new List<Image>();

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {

    }

    public void AddExtraHeart(int amount)
    {

    }

    private void UpdateHearts()
    {
        foreach(Image heart in heartImages)
        {
            Destroy(heart.gameObject);
        }
        heartImages.Clear();


    }
}
