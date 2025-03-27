using UnityEngine;

public class BossLetterBox : MonoBehaviour
{
    [SerializeField] GameObject bossLetter;
    [SerializeField] GameObject defeatedBossPlaceholder;

    Health bossEnemyHealth;
    SpriteRenderer spriteRenderer;
    Sprite bossLetterSprite;

    void Start()
    {
        // get necessary pieces
        bossEnemyHealth = bossLetter.GetComponent<Health>();
        spriteRenderer = bossLetter.GetComponentInChildren<SpriteRenderer>();
        bossLetterSprite = spriteRenderer.sprite;

        if(bossEnemyHealth != null)
        {
            bossEnemyHealth.onBossEnemyDeath += DrawDeadEnemy; // subscribe to Health.cs event that checks for when they die
        }
    }

    private void OnDestroy()
    {
        if (bossEnemyHealth != null)
        {
            bossEnemyHealth.onBossEnemyDeath -= DrawDeadEnemy;
            
        }
    }

    void Update()
    {
        
    }

    private void DrawDeadEnemy()
    {
        GameObject letterInBox = Instantiate(defeatedBossPlaceholder, transform.position, Quaternion.identity); // instantiates an empty gameobject with a sprite renderer in the box
        letterInBox.transform.SetParent(transform, false);
        letterInBox.transform.localPosition = Vector3.zero;

        SpriteRenderer letterInBoxRenderer = letterInBox.GetComponent<SpriteRenderer>(); // gets the sprite renderer for the instantiated object
        letterInBoxRenderer.sprite = bossLetterSprite; // sets the sprite to the enemy's that just died
    }
}
