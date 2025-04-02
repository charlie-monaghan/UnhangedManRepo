using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    [SerializeField] protected Item itemSO;
    //public int stackAmount;

    void Start()
    {
        itemSO.onItemPickup += UpdateItem;
        ItemEffect();
    }

    private void OnDestroy()
    {
        itemSO.onItemPickup -= UpdateItem;
    }

    void Update()
    {
        
    }

    public virtual void ItemEffect()
    {

    }

    public void UpdateItem(string _itemName)
    {
        if(_itemName == itemSO.GetItemName())
        {
            ItemEffect();
        }
    }
}
