using UnityEngine;

public class HealthUpScript : ItemInstance
{
    //private void OnEnable()
    //{
    //    itemSO.onItemPickup += OnItemPickup;
    //}

    //private void OnDisable()
    //{
    //    itemSO.onItemPickup -= OnItemPickup;
    //}

    //private void OnItemPickup(string itemName)
    //{
    //    if (itemSO.GetItemName() == itemName)
    //    {
    //        ItemEffect();
    //    }
    //}

    public override void ItemEffect()
    {
        base.ItemEffect();
        Health health = itemSO.GetPlayer().GetComponent<Health>();
        health.maxHealth = 10 + (1 * itemSO.stackAmount);
    }
}
