using UnityEngine;

public class JumpUpScript : ItemInstance
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
        Debug.Log("" + itemSO.stackAmount);
        PlayerMovement playerMovement = itemSO.GetPlayer().GetComponent<PlayerMovement>();
        playerMovement.jumpForce = 12f + (1f * itemSO.stackAmount);
        playerMovement.wallJumpForce = 15f + (1f * itemSO.stackAmount);
    }
}
