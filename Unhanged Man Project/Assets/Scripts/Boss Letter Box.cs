using UnityEngine;

public class BossLetterBox : MonoBehaviour
{
    [SerializeField] GameObject bossLetter;

    Health bossEnemyHealth;
    SpriteRenderer spriteRenderer;
    Sprite bossLetterSprite;

    void Start()
    {
        bossEnemyHealth = bossLetter.GetComponent<Health>();
        spriteRenderer = bossLetter.GetComponent<SpriteRenderer>();
        Sprite bossLetterSprite = spriteRenderer.sprite;

        if(bossEnemyHealth != null)
        {
            bossEnemyHealth.onBossEnemyDeath += DrawDeadEnemy;
            DrawDeadEnemy();
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
        
    }
}
