using UnityEngine;

public class HealthUpScript : ItemInstance
{
    public override void ItemEffect()
    {
        base.ItemEffect();
        Debug.Log("" + itemSO.stackAmount);
        Health health = itemSO.GetPlayer().GetComponent<Health>();
        health.maxHealth = 10 + (1 * itemSO.stackAmount);
    }
}
